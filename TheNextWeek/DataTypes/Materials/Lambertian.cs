using System;

using TheNextWeek.DataTypes.Materials.Utility;
using TheNextWeek.DataTypes.Utility;

using Math = TheNextWeek.DataTypes.Utility.Math;

namespace TheNextWeek.DataTypes.Materials
{
    internal sealed class Lambertian : IMaterial
    {
        private ITexture Albedo { get; }

        internal Lambertian(ITexture p_albedo)
        {
            Albedo = p_albedo;
        }

        public bool Scatter(Ray p_rayIn, HitRecord p_hitRecord, ref Color p_attenuation, ref Ray p_scatteredRay)
        {
            if ( p_attenuation == null ) throw new ArgumentNullException(nameof(p_attenuation));
            var target = p_hitRecord.Point + p_hitRecord.Normal + Math.GetRandomPositionInUnitSphere();
            p_scatteredRay = new Ray(p_hitRecord.Point, target - p_hitRecord.Point, p_rayIn.Time);
            p_attenuation = Albedo.GetValue(p_hitRecord.U, p_hitRecord.V, p_hitRecord.Point);
            return true;
        }

        public Color GetEmitted(double p_u, double p_v, Vec3 p_point)
        {
            return new Color(0, 0, 0);
        }
    }
}
