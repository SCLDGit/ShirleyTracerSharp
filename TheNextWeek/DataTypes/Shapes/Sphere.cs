using TheNextWeek.DataTypes.Materials;
using TheNextWeek.DataTypes.Utility;

using Math = System.Math;

namespace TheNextWeek.DataTypes.Shapes
{
    internal sealed class Sphere : IHitTarget
    {
        private Vec3 Center { get; }
        private double Radius { get; }

        private IMaterial Material { get; }

        public Sphere(Vec3 p_center, double p_radius, IMaterial p_material)
        {
            Center = p_center;
            Radius = p_radius;
            Material = p_material;
        }

        public bool WasHit(Ray p_ray, double p_tMin, double p_tMax, ref HitRecord p_hitRecord)
        {
            // If the quadratic is confusing, remember that several 2s are pre-canceled out. - Comment by Matt Heimlich on 06/23/2019 @ 12:15:02
            var oc           = p_ray.Origin - Center;
            var a            = Vec3.GetDotProduct(p_ray.Direction, p_ray.Direction);
            var b            = Vec3.GetDotProduct(oc, p_ray.Direction);
            var c            = Vec3.GetDotProduct(oc, oc) - Radius * Radius;
            var discriminant = b * b - a * c;
            if ( discriminant > 0 )
            {
                var sqrtCache = Math.Sqrt(b * b - a * c);
                var temp = (-b - sqrtCache) / a;
                if ( temp < p_tMax && temp > p_tMin )
                {
                    p_hitRecord.T = temp;
                    p_hitRecord.Point = p_ray.PointAt(p_hitRecord.T);
                    p_hitRecord.Normal = (p_hitRecord.Point - Center) / Radius;
                    p_hitRecord.Material = Material;
                    return true;
                }
                temp = (-b + sqrtCache) / a;
                if ( temp < p_tMax && temp > p_tMin )
                {
                    p_hitRecord.T      = temp;
                    p_hitRecord.Point  = p_ray.PointAt(p_hitRecord.T);
                    p_hitRecord.Normal = (p_hitRecord.Point - Center) / Radius;
                    p_hitRecord.Material = Material;
                    return true;
                }
            }

            return false;
        }

        public bool GenerateBoundingBox(double p_time1, double p_time2, ref BoundingBox p_box)
        {
            p_box = new BoundingBox(Center - new Vec3(Radius, Radius, Radius), Center + new Vec3(Radius, Radius, Radius));
            return true;
        }
    }
}
