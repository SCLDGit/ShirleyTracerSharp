namespace TheNextWeek.DataTypes.Utility
{
    internal class Color : Vec3
    {
        public Color(double p_r, double p_g, double p_b)
        {
            X = p_r;
            Y = p_g;
            Z = p_b;
        }

        public void GammaCorrect(double p_gamma)
        {
            // Take a math shortcut if p_gamma is 2. - Comment by Matt Heimlich on 06/23/2019 @ 13:20:44
            if ( System.Math.Abs(p_gamma - 2.0) < 0.00001 )
            {
                X = System.Math.Sqrt(X);
                Y = System.Math.Sqrt(Y);
                Z = System.Math.Sqrt(Z);
            }
            else
            {
                X = System.Math.Pow(X, 1 / p_gamma);
                Y = System.Math.Pow(Y, 1 / p_gamma);
                Z = System.Math.Pow(Z, 1 / p_gamma);
            }
        }

        public static Color operator +(Color p_v1, Color p_v2)
        {
            return new Color(p_v1.X + p_v2.X, p_v1.Y + p_v2.Y, p_v1.Z + p_v2.Z);
        }

        public static Color operator -(Color p_v1, Color p_v2)
        {
            return new Color(p_v1.X - p_v2.X, p_v1.Y - p_v2.Y, p_v1.Z - p_v2.Z);
        }

        public static Color operator *(Color p_v1, Color p_v2)
        {
            return new Color(p_v1.X * p_v2.X, p_v1.Y * p_v2.Y, p_v1.Z * p_v2.Z);
        }

        public static Color operator /(Color p_v1, Color p_v2)
        {
            return new Color(p_v1.X / p_v2.X, p_v1.Y / p_v2.Y, p_v1.Z / p_v2.Z);
        }

        public static Color operator *(Color p_v1, double p_double)
        {
            return new Color(p_v1.X * p_double, p_v1.Y * p_double, p_v1.Z * p_double);
        }

        public static Color operator *(double p_double, Color p_v1)
        {
            return new Color(p_v1.X * p_double, p_v1.Y * p_double, p_v1.Z * p_double);
        }

        public static Color operator /(Color p_v1, double p_double)
        {
            return new Color(p_v1.X / p_double, p_v1.Y / p_double, p_v1.Z / p_double);
        }
    }
}