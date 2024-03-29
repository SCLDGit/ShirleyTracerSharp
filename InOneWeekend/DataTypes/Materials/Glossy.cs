﻿using System;

using InOneWeekend.DataTypes.Utility;

using Math = InOneWeekend.DataTypes.Utility.Math;

namespace InOneWeekend.DataTypes.Materials
{
    internal class Glossy : IMaterial
    {
        private Color Albedo { get; }
        private double Roughness { get; }

        internal Glossy(Color p_albedo, double p_roughness)
        {
            Albedo = p_albedo;
            Roughness = p_roughness < 1 && p_roughness >= 0 ? p_roughness : 1;
        }

        public virtual bool Scatter(Ray p_rayIn, HitRecord p_hitRecord, ref Color p_attenuation, ref Ray p_scatteredRay)
        {
            if ( p_attenuation == null ) throw new ArgumentNullException(nameof(p_attenuation));
            var reflected = Vec3.GetReflection(Vec3.GetUnitVector(p_rayIn.Direction), p_hitRecord.Normal);
            p_scatteredRay = new Ray(p_hitRecord.Point, reflected + Roughness * Math.GetRandomPositionInUnitSphere());
            p_attenuation = Albedo;
            return Vec3.GetDotProduct(p_scatteredRay.Direction, p_hitRecord.Normal) > 0;
        }
    }
}
