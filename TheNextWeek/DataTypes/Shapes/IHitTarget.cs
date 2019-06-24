using TheNextWeek.DataTypes.Utility;

namespace TheNextWeek.DataTypes.Shapes
{
    internal interface IHitTarget
    {
        bool WasHit(Ray p_ray, double p_tMin, double p_tMax, ref HitRecord p_hitRecord);
        bool GenerateBoundingBox(double p_time1, double p_time2, ref BoundingBox p_box);
    }
}
