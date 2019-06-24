using InOneWeekend.DataTypes.Utility;

namespace InOneWeekend.DataTypes.Shapes
{
    internal interface IHitTarget
    {
        bool WasHit(Ray p_ray, double p_tMin, double p_tMax, ref HitRecord p_hitRecord);
    }
}
