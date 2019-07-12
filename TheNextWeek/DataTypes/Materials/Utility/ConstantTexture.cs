using System;
using System.Collections.Generic;
using System.Text;

using TheNextWeek.DataTypes.Utility;

namespace TheNextWeek.DataTypes.Materials.Utility
{
    class ConstantTexture : ITexture
    {
        private Color Color { get; }

        public ConstantTexture(Color p_color)
        {
            Color = p_color;
        }

        public Color GetValue(double p_u, double p_v, Vec3 p_point)
        {
            return Color;
        }
    }
}
