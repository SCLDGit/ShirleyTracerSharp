using System;
using System.Collections.Generic;
using System.Text;

using InOneWeekend.DataTypes;
using InOneWeekend.DataTypes.Materials;
using InOneWeekend.DataTypes.Shapes;
using InOneWeekend.DataTypes.Utility;

using Math = System.Math;

namespace InOneWeekend
{
    static class SceneGenerator
    {
        internal static List<IHitTarget> GenerateThreeSphereScene()
        {
            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, 0, -1), 0.5, new Lambertian(new Color(0.8, 0.3, 0.3))));
            returnList.Add(new Sphere(new Vec3(0, -100.5, -1), 100, new Lambertian(new Color(0.8, 0.8, 0))));
            returnList.Add(new Sphere(new Vec3(1, 0, -1), 0.5, new Glossy(new Color(0.8, 0.6, 0.2), 1.0)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), 0.5, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));
            returnList.Add(new Sphere(new Vec3(-1, 0, -1), -0.45, new Dielectric(new Color(1.0, 1.0, 1.0), 1.5)));

            return returnList;
        }

        internal static List<IHitTarget> GenerateTwoSphereScene()
        {
            var returnList = new List<IHitTarget>();

            var radius = Math.Cos(Math.PI / 4);

            returnList.Add(new Sphere(new Vec3(-radius, 0, -1), radius, new Lambertian(new Color(0, 0, 1))));
            returnList.Add(new Sphere(new Vec3(radius, 0, -1), radius, new Lambertian(new Color(1, 0, 0))));

            return returnList;
        }

        internal static List<IHitTarget> GenerateRandomScene(int p_numberOfObjects)
        {
            var rng = new Random();

            var returnList = new List<IHitTarget>();

            returnList.Add(new Sphere(new Vec3(0, -1000, 0), 1000, new Lambertian(new Color(0.5, 0.5, 0.5)) ));
            for ( var i = -11; i < 11; ++i )
            {
                for ( var j = -11; j < 11; ++j )
                {
                    var materialPicker = rng.NextDouble();
                    var center = new Vec3(i + 0.9 * rng.NextDouble(), 0.2, j + 0.9 * rng.NextDouble());
                    if ( (center - new Vec3(4, 0.2, 0)).GetLength() > 0.9 )
                    {
                        if ( materialPicker < 0.8 )
                        {
                            returnList.Add(new Sphere(center, 0.2, new Lambertian(new Color(rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble())) ));
                        }
                        else if ( materialPicker < 0.95 )
                        {
                            returnList.Add(new Sphere(center, 0.2, new Glossy(new Color(0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble()), 0.5 * (1 + rng.NextDouble())), 0.5 * rng.NextDouble())));
                        }
                        else
                        {
                            returnList.Add(new Sphere(center, 0.2, new Dielectric(new Color(rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble(), rng.NextDouble() * rng.NextDouble()), 1.5) ));
                        }
                    }
                }
            }

            returnList.Add(new Sphere(new Vec3(0, 1, 0), 1.0, new Dielectric(new Color(1, 1, 1), 1.5) ));
            returnList.Add(new Sphere(new Vec3(-4, 1, 0), 1.0, new Lambertian(new Color(0.4, 0.2, 0.1)) ));
            returnList.Add(new Sphere(new Vec3(4, 1, 0), 1.0, new Glossy(new Color(0.7, 0.6, 0.5), 0.05)) );

            return returnList;
        }
    }
}
