using System;

namespace TheNextWeek.DataTypes.Utility
{
    internal static class Math
    {
        internal static Vec3 GetRandomPositionInUnitSphere()
        {
            var rng = new Random();
            Vec3 p;
            do
            {
                p = 2.0 * new Vec3(rng.NextDouble(), rng.NextDouble(), rng.NextDouble()) - new Vec3(1);
            } while (p.GetLengthSquared() >= 1.0);

            return p;
        }

        internal static Vec3 GetRandomPositionOnUnitDisk()
        {
            var rng = new Random();
            Vec3 p;
            do
            {
                p = 2.0 * new Vec3(rng.NextDouble(), rng.NextDouble(), 0) - new Vec3(1, 1, 0);
            } while (Vec3.GetDotProduct(p, p) >= 1.0);

            return p;
        }

        internal static double SchlickApproximation(double p_cosine, double p_indexOfRefraction)
        {
            var r0 = (1 - p_indexOfRefraction) / (1 + p_indexOfRefraction);
            r0 = r0 * r0;
            return r0 + (1 - r0) * System.Math.Pow(1 - p_cosine, 5);
        }

        internal static double GetMin(double p_a, double p_b)
        {
            return p_a < p_b ? p_a : p_b;
        }

        internal static double GetMax(double p_a, double p_b)
        {
            return p_a > p_b ? p_a : p_b;
        }
    }
}
