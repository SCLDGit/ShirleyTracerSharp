using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;

using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using TheNextWeek.Constants;
using TheNextWeek.DataTypes.Camera;
using TheNextWeek.DataTypes.Shapes;
using TheNextWeek.DataTypes.Utility;
using TheNextWeek.ImageUtilities;

namespace TheNextWeek
{
    internal static class Program
    {
        private static Color GetColor(Ray p_ray, IHitTarget p_world, int p_depth)
        {
            var maxDepth = 50;
            var hitRecord = new HitRecord();
            if ( p_world.WasHit(p_ray, 0.001, double.MaxValue, ref hitRecord) )
            {
                var scatteredRay = new Ray(new Vec3(0), new Vec3(0));
                var attenuation = new Color(0, 0, 0);
                if ( p_depth < maxDepth &&
                     hitRecord.Material.Scatter(p_ray, hitRecord, ref attenuation, ref scatteredRay) )
                {
                    return attenuation * GetColor(scatteredRay, p_world, p_depth + 1);
                }
                return new Color(0, 0, 0);
            }
            var unitDirection = Vec3.GetUnitVector(p_ray.Direction);
            var t = 0.5 * (unitDirection.Y + 1.0);
            return (1.0 - t) * new Color(1.0, 1.0, 1.0) + t * new Color(0.5, 0.7, 1.0);
        }

        // ReSharper disable once ArrangeTypeMemberModifiers
        // ReSharper disable once UnusedParameter.Local
        static void Main(string[] p_args)
        {
            #region Set Up Render Save Location
            if (!Directory.Exists(Path.GetDirectoryName(RenderConstants.SaveLocation)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(RenderConstants.SaveLocation));
            }

            #endregion

            // Instantiate World. - Comment by Matt Heimlich on 06/23/2019 @ 12:34:43
            var newWorld = SceneGenerator.GenerateRandomMotionBlurBvhScene();

            #region PPM writer (Disabled)
            //using ( var writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Peter Shirley", "Renders", $@"render_{DateTime.Now:MM-dd-yyyy-HH-mm-ss}.ppm")) )
            //{
            //    writer.WriteLine("P3");
            //    writer.WriteLine($@"{RenderConstants.ImageSizeX} {RenderConstants.ImageSizeY}");
            //    writer.WriteLine("255");
            //    for ( var j = RenderConstants.ImageSizeY - 1; j >= 0; --j )
            //    {
            //        for ( var i = 0; i < RenderConstants.ImageSizeX; ++i )
            //        {
            //            var r = (float) i / (float) RenderConstants.ImageSizeX;
            //            var g = (float) j / (float) RenderConstants.ImageSizeY;
            //            var b = 0.2d;

            //            writer.WriteLine($@"{(int)(255.99 * r)} {(int)(255.99 * g)} {(int)(255.99 * b)}");
            //        }
            //    }
            //}
            #endregion

            #region Camera Settings

            // Three Sphere Scene Settings
            //var          lookFrom      = new Vec3(0, 0, 0);
            //var          lookAt        = new Vec3(0, 0, -1);
            //const double focalDistance = 2;
            //const double aperture      = 0.0;

            // Random Scene Settings
            var lookFrom = new Vec3(13, 2, 3);
            var lookAt = new Vec3(0, 0, 0);
            const double focalDistance = 10.0;
            const double aperture = 0.1;
            #endregion

            var camera = new Camera(lookFrom, lookAt, new Vec3(0, 1, 0), 20, RenderConstants.ImageSizeX / (float) RenderConstants.ImageSizeY, aperture, focalDistance, 0.0, 1.0);

            using ( var image = new Image<Rgba32>(RenderConstants.ImageSizeX, RenderConstants.ImageSizeY) )
            {
                var stopWatch = Stopwatch.StartNew();

                if ( RenderConstants.UseParallelProcessing )
                {
                    Parallel.For(0, RenderConstants.ImageSizeY, p_index =>
                                                                {
                                                                    var rng = new Random();
                                                                    for (var i = 0; i < RenderConstants.ImageSizeX; ++i)
                                                                    {
                                                                        var color = new Color(0, 0, 0);
                                                                        for (var s = 0; s < RenderConstants.NumberOfSamples; ++s)
                                                                        {
                                                                            var u = (float)(i + rng.NextDouble()) / RenderConstants.ImageSizeX;
                                                                            var v = (float)(p_index + rng.NextDouble()) / RenderConstants.ImageSizeY;

                                                                            var ray   = camera.GetRay(u, v);

                                                                            color += GetColor(ray, newWorld, 0);
                                                                        }

                                                                        color /= (float)RenderConstants.NumberOfSamples;

                                                                        color.GammaCorrect(2);

                                                                        // Flip image writing here for Y axis. - Comment by Matt Heimlich on 06/21/2019 @ 19:24:07
                                                                        // ReSharper disable once AccessToDisposedClosure
                                                                        image[i, RenderConstants.ImageSizeY - (p_index + 1)] = new Rgba32(new Vector3((float)color.R, (float)color.G, (float)color.B));
                                                                    }
                                                                });
                }
                else
                {
                    var rng = new Random();

                    for (var j = 0; j < RenderConstants.ImageSizeY; ++j)
                    {
                        for (var i = 0; i < RenderConstants.ImageSizeX; ++i)
                        {
                            var color = new Color(0, 0, 0);
                            for (var s = 0; s < RenderConstants.NumberOfSamples; ++s)
                            {
                                var u = (float)(i + rng.NextDouble()) / RenderConstants.ImageSizeX;
                                var v = (float)(j + rng.NextDouble()) / RenderConstants.ImageSizeY;

                                var ray   = camera.GetRay(u, v);

                                color += GetColor(ray, newWorld, 0);
                            }

                            color /= (float)RenderConstants.NumberOfSamples;

                            color.GammaCorrect(2);

                            // Flip image writing here for Y axis. - Comment by Matt Heimlich on 06/21/2019 @ 19:24:07
                            image[i, RenderConstants.ImageSizeY - (j + 1)] = new Rgba32(new Vector3((float)color.R, (float)color.G, (float)color.B));
                        }
                    }
                }

                stopWatch.Stop();

                // Optionally set encoder values by hand. - Comment by Matt Heimlich on 06/21/2019 @ 18:05:42
                //Configuration.Default.ImageFormatsManager.SetEncoder(PngFormat.Instance, new PngEncoder()
                //{
                //    BitDepth = PngBitDepth.Bit8,
                //    ColorType = PngColorType.Rgb,
                //    CompressionLevel = 6,
                //    FilterMethod = PngFilterMethod.Adaptive,
                //    Gamma = 2.2f
                //});

                var font = SystemFonts.CreateFont("Oswald Medium", 12);

                if ( RenderConstants.PrintRenderInfo )
                {
                    var runtimeString = GetRuntimeString(stopWatch.ElapsedMilliseconds);

                    var parallelString = RenderConstants.UseParallelProcessing ? "Parallel" : "Not Parallel";

                    using (var imageWithRunData = image.Clone(p_ctx => p_ctx.ApplyScalingWaterMark(font, $@"{RenderConstants.ImageSizeX}x{RenderConstants.ImageSizeY} | SPP: {RenderConstants.NumberOfSamples} | {parallelString} | {runtimeString}", Rgba32.GhostWhite, Rgba32.DarkSlateGray, 5, false, 30)))
                    {

                        imageWithRunData.Save(RenderConstants.SaveLocation);

                    }
                }
                else
                {
                    image.Save(RenderConstants.SaveLocation);
                }

                new Process { StartInfo = new ProcessStartInfo(RenderConstants.SaveLocation) { UseShellExecute = true } }.Start();
            }
        }

        private static string GetRuntimeString(long p_stopWatchElapsedMilliseconds)
        {
            if ( p_stopWatchElapsedMilliseconds < 1000 )
            {
                return $@"{p_stopWatchElapsedMilliseconds}ms";
            }
            if ( p_stopWatchElapsedMilliseconds >= 1000 && p_stopWatchElapsedMilliseconds < 60000 )
            {
                var printSeconds = p_stopWatchElapsedMilliseconds / 1000;
                var printMilliseconds = p_stopWatchElapsedMilliseconds % 1000;
                return $@"{printSeconds}s {printMilliseconds}ms";
            }
            if ( p_stopWatchElapsedMilliseconds >= 60000 && p_stopWatchElapsedMilliseconds < 3600000 )
            {
                var printMinutes = p_stopWatchElapsedMilliseconds / 60000;
                var printSeconds = p_stopWatchElapsedMilliseconds % 60000 / 1000;
                var printMilliseconds = p_stopWatchElapsedMilliseconds % 60000 % 1000;
                return $@"{printMinutes}m {printSeconds}s {printMilliseconds}ms";
            }

            if ( p_stopWatchElapsedMilliseconds >= 3600000 )
            {
                var printHours        = p_stopWatchElapsedMilliseconds / 3600000;
                var printMinutes      = p_stopWatchElapsedMilliseconds % 3600000 / 60000;
                var printSeconds      = p_stopWatchElapsedMilliseconds % 60000 / 1000;
                var printMilliseconds = p_stopWatchElapsedMilliseconds % 60000 % 1000;
                return $@"{printHours}h {printMinutes}m {printSeconds}s {printMilliseconds}ms";
            }

            throw new Exception("Some unknown stopwatch value encountered.");
        }
    }
}