using System;
using System.Collections.Generic;
using System.Text;

using TheNextWeek.DataTypes.Materials.Utility;
using TheNextWeek.DataTypes.Utility;

namespace TheNextWeek.DataTypes.Materials
{
    class Emission : IMaterial
    {
        internal ITexture Color { get; }
        internal double Power { get; }

        internal Emission(ITexture p_texture, double p_power)
        {
            Color = p_texture;
            Power = p_power;
        }

        public bool Scatter(Ray p_rayIn, HitRecord p_hitRecord, ref Color p_attenuation, ref Ray p_scatteredRay)
        {
            return false;
        }

        public Color GetEmitted(double p_u, double p_v, Vec3 p_point)
        {
            return Color.GetValue(p_u, p_v, p_point) * Power;
        }
    }
}
