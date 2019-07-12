﻿using System;

using TheNextWeek.DataTypes.Utility;

using Math = TheNextWeek.DataTypes.Utility.Math;

namespace TheNextWeek.DataTypes.Materials
{
    internal sealed class Dielectric : IMaterial
    {
        private double IndexOfRefraction { get; }
        private Color Color { get; }

        internal Dielectric(Color p_color, double p_indexOfRefraction)
        {
            Color = p_color;
            IndexOfRefraction = p_indexOfRefraction;
        }

        public bool Scatter(Ray p_rayIn, HitRecord p_hitRecord, ref Color p_attenuation, ref Ray p_scatteredRay)
        {
            if ( p_attenuation == null ) throw new ArgumentNullException(nameof(p_attenuation));
            var rng = new Random();
            Vec3 outwardNormal;
            var reflected = Vec3.GetReflection(p_rayIn.Direction, p_hitRecord.Normal);
            var refracted = new Vec3(0);
            double niOverNt;

            p_attenuation = Color;

            double cosine;

            if ( Vec3.GetDotProduct(p_rayIn.Direction, p_hitRecord.Normal) > 0 )
            {
                outwardNormal = -p_hitRecord.Normal;
                niOverNt = IndexOfRefraction;
                cosine = IndexOfRefraction * Vec3.GetDotProduct(p_rayIn.Direction, p_hitRecord.Normal) /
                         p_rayIn.Direction.GetLength();
            }
            else
            {
                outwardNormal = p_hitRecord.Normal;
                niOverNt = 1.0 / IndexOfRefraction;
                cosine = -Vec3.GetDotProduct(p_rayIn.Direction, p_hitRecord.Normal) / p_rayIn.Direction.GetLength();
            }

            var reflectionProbability = Vec3.ShouldRefract(p_rayIn.Direction, outwardNormal, niOverNt, ref refracted) ? Math.SchlickApproximation(cosine, IndexOfRefraction) : 1.0d;

            p_scatteredRay = rng.NextDouble() < reflectionProbability ? new Ray(p_hitRecord.Point, reflected, p_rayIn.Time) : new Ray(p_hitRecord.Point, refracted, p_rayIn.Time);

            return true;
        }

        public Color GetEmitted(double p_u, double p_v, Vec3 p_point)
        {
            return new Color(0, 0, 0);
        }
    }
}
