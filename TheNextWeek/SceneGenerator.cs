using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using StbSharp;

using TheNextWeek.DataTypes.Materials;
using TheNextWeek.DataTypes.Materials.Utility;
using TheNextWeek.DataTypes.Shapes;
using TheNextWeek.DataTypes.Utility;

using Math = System.Math;

namespace TheNextWeek
{
    internal static class SceneGenerator
    {
        internal static IHitTarget GenerateThreeSphereScene()
        {
            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, 0, -1), 0.48, new Lambertian(new ConstantTexture(new Color(0.8, 0.3, 0.3)))));
            returnList.Add(new Sphere(new Vec3(0, -100.5, -1), 100, new Lambertian(new ConstantTexture(new Color(0.8, 0.8, 0)))));
            returnList.Add(new Sphere(new Vec3(1, 0, -1), 0.5, new Glossy(new Color(0.8, 0.6, 0.2), 1.0)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), 0.5, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));
            //returnList.Add(new Sphere(new Vec3(-1, 0, -1), -0.45, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));

            return new World(returnList);
        }

        internal static IHitTarget GenerateThreeSphereBvhScene()
        {
            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, 0, -1), 0.48, new Lambertian(new ConstantTexture(new Color(0.8, 0.3, 0.3)))));
            returnList.Add(new Sphere(new Vec3(0, -100.5, -1), 100, new Lambertian(new ConstantTexture(new Color(0.8, 0.8, 0)))));
            returnList.Add(new Sphere(new Vec3(1, 0, -1), 0.5, new Glossy(new Color(0.8, 0.6, 0.2), 1.0)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), 0.5, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));
            //returnList.Add(new Sphere(new Vec3(-1, 0, -1), -0.45, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateThreeSphereMotionBlurScene()
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            returnList.Add(new MovingSphere(new Vec3(0, 0, -1), new Vec3(0, 0, -1) + new Vec3(0, 0.5 * rng.NextDouble(), 0), 0.0, 1.0, 0.35, new Lambertian(new ConstantTexture(new Color(0.8, 0.3, 0.3)))));
            returnList.Add(new Sphere(new Vec3(0, -100.5, -1), 100, new Lambertian(new ConstantTexture(new Color(0.8, 0.8, 0)))));
            returnList.Add(new Sphere(new Vec3(1, 0, -1), 0.5, new Glossy(new Color(0.8, 0.6, 0.2), 0.0)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), 0.5, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), -0.45, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));

            return new World(returnList);
        }

        internal static IHitTarget GenerateThreeSphereMotionBlurBvhScene()
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            returnList.Add(new MovingSphere(new Vec3(0, 0, -1), new Vec3(0, 0, -1) + new Vec3(0, 0.5 * rng.NextDouble(), 0), 0.0, 1.0, 0.35, new Lambertian(new ConstantTexture(new Color(0.8, 0.3, 0.3)))));
            returnList.Add(new Sphere(new Vec3(0, -100.5, -1), 100, new Lambertian(new ConstantTexture(new Color(0.8, 0.8, 0)))));
            returnList.Add(new Sphere(new Vec3(1, 0, -1), 0.5, new Glossy(new Color(0.8, 0.6, 0.2), 0.0)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), 0.5, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), -0.45, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateTwoSphereScene()
        {
            var returnList = new List<IHitTarget>();

            var radius = Math.Cos(Math.PI / 4);

            returnList.Add(new Sphere(new Vec3(-radius, 0, -1), radius, new Lambertian(new ConstantTexture(new Color(0, 0, 1)))));
            returnList.Add(new Sphere(new Vec3(radius, 0, -1), radius, new Lambertian(new ConstantTexture(new Color(1, 0, 0)))));

            return new World(returnList);
        }

        internal static IHitTarget GenerateTwoSphereBvhScene()
        {
            var returnList = new List<IHitTarget>();

            var radius = Math.Cos(Math.PI / 4);

            returnList.Add(new Sphere(new Vec3(-radius, 0, -1), radius, new Lambertian(new ConstantTexture(new Color(0, 0, 1)))));
            returnList.Add(new Sphere(new Vec3(radius, 0, -1), radius, new Lambertian(new ConstantTexture(new Color(1, 0, 0)))));

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateRandomScene()
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            var checkerTexture = new CheckerTexture(new ConstantTexture(new Color(0.2, 0.3, 0.1)), new ConstantTexture(new Color(0.9, 0.9, 0.9)) );

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(checkerTexture)));
            for ( var i = -11; i < 11; ++i )
            {
                for ( var j = -11; j < 11; ++j )
                {
                    var materialPicker = rng.NextDouble();
                    var center = new Vec3(i + 0.9 * rng.NextDouble(), 0.2, j + 0.9 * rng.NextDouble());
                    if ( !((center - new Vec3(4, 0.2, 0)).GetLength() > 0.9) ) continue;
                    if ( materialPicker < 0.7 )
                    {
                        returnList.Add(new Sphere(center, 0.2, new Lambertian(new ConstantTexture(new Color(rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble())))));
                    }
                    else if ( materialPicker < 0.8 )
                    {
                        returnList.Add(new Sphere(center, 0.2, new Glossy(new Color(0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble())), 0.5 * rng.NextDouble())));
                    }
                    else
                    {
                        returnList.Add(new Sphere(center, 0.2, new Dielectric(new Color(1, 1, 1), 1.5) ));
                    }
                }
            }

            returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, new Dielectric(new Color(1, 1, 1), 1.5) ));
            returnList.Add(new Sphere(new Vec3(-4, 1, 0), 1.0, new Lambertian(new ConstantTexture(new Color(0.4, 0.2, 0.1)))));
            returnList.Add(new Sphere(new Vec3(4, 1, 0), 1.0, new Glossy(new Color(0.7, 0.6, 0.5), 0.05)) );

            return new World(returnList);
        }

        internal static IHitTarget GenerateRandomBvhScene()
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            var checkerTexture = new CheckerTexture(new ConstantTexture(new Color(0.2, 0.3, 0.1)), new ConstantTexture(new Color(0.9, 0.9, 0.9)));
            var perlinTexture = new NoiseTexture( new Color(1, 1, 1), 10.0, NoiseTypes.TURBULENCE);

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(perlinTexture)));
            for (var i = -11; i < 11; ++i)
            {
                for (var j = -11; j < 11; ++j)
                {
                    var materialPicker = rng.NextDouble();
                    var center         = new Vec3(i + 0.9 * rng.NextDouble(), 0.2, j + 0.9 * rng.NextDouble());
                    if (!((center - new Vec3(4, 0.2, 0)).GetLength() > 0.9)) continue;
                    if (materialPicker < 0.7)
                    {
                        returnList.Add(new Sphere(center, 0.2, new Lambertian(new ConstantTexture(new Color(rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble())))));
                    }
                    else if (materialPicker < 0.8)
                    {
                        returnList.Add(new Sphere(center, 0.2, new Glossy(new Color(0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble())), 0.5 * rng.NextDouble())));
                    }
                    else
                    {
                        returnList.Add(new Sphere(center, 0.2, new Dielectric(new Color(1, 1, 1), 1.5)));
                    }
                }
            }

            returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, new Dielectric(new Color(1, 1, 1), 1.5)));
            returnList.Add(new Sphere(new Vec3(-4, 1, 0), 1.0, new Lambertian(new ConstantTexture(new Color(0.4, 0.2, 0.1)))));
            returnList.Add(new Sphere(new Vec3(4, 1, 0), 1.0, new Glossy(new Color(0.7, 0.6, 0.5), 0.05)));

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateTwoLargeSphereBvhScene()
        {
            var returnList = new List<IHitTarget>();

            var perlinTexture = new NoiseTexture(new Color(0.8, 0.8, 0.8), 4.0, NoiseTypes.MARBLE, 0.5, 10.0, 15);

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(perlinTexture)));

            returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, new Lambertian(perlinTexture)));

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateRandomMotionBlurScene()
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(new ConstantTexture(new Color(0.5, 0.5, 0.5)))));
            for (var i = -11; i < 11; ++i)
            {
                for (var j = -11; j < 11; ++j)
                {
                    var materialPicker = rng.NextDouble();
                    var center         = new Vec3(i + 0.9 * rng.NextDouble(), 0.2, j + 0.9 * rng.NextDouble());
                    if (!((center - new Vec3(4, 0.2, 0)).GetLength() > 0.9)) continue;
                    if (materialPicker < 0.7)
                    {
                        returnList.Add(new MovingSphere(center, center + new Vec3(0, 0.5 * rng.NextDouble(), 0), 0.0, 1.0, 0.2, new Lambertian(new ConstantTexture(new Color(rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble())))));
                    }
                    else if (materialPicker < 0.8)
                    {
                        returnList.Add(new Sphere(center, 0.2, new Glossy(new Color(0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble())), 0.5 * rng.NextDouble())));
                    }
                    else
                    {
                        returnList.Add(new Sphere(center, 0.2, new Dielectric(new Color(1, 1, 1), 1.5)));
                    }
                }
            }

            returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, new Dielectric(new Color(1, 1, 1), 1.5)));
            returnList.Add(new Sphere(new Vec3(-4, 1, 0), 1.0, new Lambertian(new ConstantTexture(new Color(0.4, 0.2, 0.1)))));
            returnList.Add(new Sphere(new Vec3(4, 1, 0), 1.0, new Glossy(new Color(0.7, 0.6, 0.5), 0.05)));

            return new World(returnList);
        }

        internal static IHitTarget GenerateRandomMotionBlurBvhScene()
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(new ConstantTexture(new Color(0.5, 0.5, 0.5)))));
            for (var i = -11; i < 11; ++i)
            {
                for (var j = -11; j < 11; ++j)
                {
                    var materialPicker = rng.NextDouble();
                    var center         = new Vec3(i + 0.9 * rng.NextDouble(), 0.2, j + 0.9 * rng.NextDouble());
                    if (!((center - new Vec3(4, 0.2, 0)).GetLength() > 0.9)) continue;
                    if (materialPicker < 0.7)
                    {
                        returnList.Add(new MovingSphere(center, center + new Vec3(0, 0.5 * rng.NextDouble(), 0), 0.0, 1.0, 0.2, new Lambertian(new ConstantTexture(new Color(rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble())))));
                    }
                    else if (materialPicker < 0.8)
                    {
                        returnList.Add(new Sphere(center, 0.2, new Glossy(new Color(0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble())), 0.5 * rng.NextDouble())));
                    }
                    else
                    {
                        returnList.Add(new Sphere(center, 0.2, new Dielectric(new Color(1, 1, 1), 1.5)));
                    }
                }
            }

            returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, new Dielectric(new Color(1, 1, 1), 1.5)));
            returnList.Add(new Sphere(new Vec3(-4, 1, 0), 1.0, new Lambertian(new ConstantTexture(new Color(0.4, 0.2, 0.1)))));
            returnList.Add(new Sphere(new Vec3(4, 1, 0), 1.0, new Glossy(new Color(0.7, 0.6, 0.5), 0.05)));

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateEarthBvhScene()
        {

            var returnList = new List<IHitTarget>();

            var perlinTexture = new NoiseTexture(new Color(0.8, 0.8, 0.8), 4.0, NoiseTypes.MARBLE, 0.5, 10.0, 15);

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(perlinTexture)));

            var loader = new ImageReader();

            using ( var stream = File.Open("earthMap.jpg", FileMode.Open) )
            {
                var image = loader.Read(stream, StbImage.STBI_rgb);

                var material = new Lambertian(new ImageTexture(image.Data, image.Width, image.Height));

                returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, material));

            }

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateEarthLampBvhScene()
        {

            var returnList = new List<IHitTarget>();

            var perlinTexture = new NoiseTexture(new Color(0.8, 0.8, 0.8), 4.0, NoiseTypes.MARBLE, 0.5, 10.0, 15);

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(perlinTexture)));

            var loader = new ImageReader();

            using (var stream = File.Open("earthMap.jpg", FileMode.Open))
            {
                var image = loader.Read(stream, StbImage.STBI_rgb);

                var material = new Emission(new ImageTexture(image.Data, image.Width, image.Height), 10);

                returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, material));

            }

            return new BvhNode(returnList, 0.0, 1.0);
        }

        internal static IHitTarget GenerateSimpleAreaLightBvhScene()
        {
            var perlinTexture = new NoiseTexture(new Color(0.8, 0.8, 0.8), 4, NoiseTypes.MARBLE, 0.5, 10, 0 );

            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(perlinTexture)));
            returnList.Add(new Sphere(new Vec3(0, 2, 0), 2, new Lambertian(perlinTexture)));
            returnList.Add(new Sphere(new Vec3(0, 7, 0), 2, new Emission(new ConstantTexture(new Color(1, 1, 1)), 4 )));
            returnList.Add(new XYAlignedRectangle(3, 5, 1, 3, -2, new Emission(new ConstantTexture(new Color(1, 1, 1)), 4)));
            
            return new BvhNode(returnList, 0.0, 1.0);
        }
    }
}
