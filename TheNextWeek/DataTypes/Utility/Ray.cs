namespace TheNextWeek.DataTypes.Utility
{
    internal class Ray
    {
        public Vec3 Origin { get; }
        public Vec3 Direction { get; }
        public double Time { get; }

        public Ray(Vec3 p_origin, Vec3 p_direction, double p_time = 0.0)
        {
            Origin = p_origin;
            Direction = p_direction;
            Time = p_time;
        }

        public Vec3 PointAt(double p_double)
        {
            return Origin + p_double * Direction;
        }
    }
}
