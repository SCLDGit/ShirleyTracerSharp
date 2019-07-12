using System;
using System.Collections.Generic;
using System.Text;

using SixLabors.ImageSharp.Processing;

using TheNextWeek.DataTypes.Utility;

using Math = System.Math;

namespace TheNextWeek.DataTypes.Materials.Utility
{
    internal enum NoiseTypes
    {
        REGULAR,
        SMOOTHED,
        TURBULENCE,
        MARBLE
    }

    class NoiseTexture : ITexture
    {
        internal Color Color { get; }
        internal double Scale { get; }
        private Perlin Noise { get; }
        internal NoiseTypes NoiseType { get; }
        internal double MultiplyStrength { get; }
        internal double DistortionPower { get; }

        internal NoiseTexture(Color p_color, double p_scale, NoiseTypes p_noiseType, double p_multiplyStrength = 0.5, double p_distortionPower = 1.0, int p_seed = 0)
        {
            Color = p_color;
            Scale = p_scale;
            NoiseType = p_noiseType;
            MultiplyStrength = p_multiplyStrength;
            DistortionPower = p_distortionPower;
            Noise = new Perlin(p_seed);
        }

        public Color GetValue(double p_u, double p_v, Vec3 p_point)
        {
            switch ( NoiseType ) {
                case NoiseTypes.REGULAR:
                    return Color * MultiplyStrength * (DistortionPower * Noise.GetNoise(p_point * Scale));
                case NoiseTypes.SMOOTHED:
                    return Color * MultiplyStrength * (1 + DistortionPower * Noise.GetSmoothedNoise(p_point * Scale));
                case NoiseTypes.TURBULENCE:
                    return Color * MultiplyStrength * (DistortionPower * Noise.GetTurbulentNoise(p_point * Scale));
                case NoiseTypes.MARBLE:
                    return Color * MultiplyStrength * (1 + Math.Sin(Scale * p_point.Z + DistortionPower * Noise.GetTurbulentNoise(p_point)));
                default:
                    return Color * MultiplyStrength * (DistortionPower * Noise.GetNoise(p_point * Scale));
            }
        }
    }
}
