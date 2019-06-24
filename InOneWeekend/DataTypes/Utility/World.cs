using System.Collections.Generic;

using InOneWeekend.DataTypes.Shapes;

namespace InOneWeekend.DataTypes.Utility
{
    internal sealed class World : IHitTarget
    {
        private List<IHitTarget> WorldHitTargets { get; }

        public World()
        {
            WorldHitTargets = new List<IHitTarget>();
        }

        public World(List<IHitTarget> p_worldHitTargets)
        {
            WorldHitTargets = p_worldHitTargets;
        }

        public bool WasHit(Ray p_ray, double p_tMin, double p_tMax, ref HitRecord p_hitRecord)
        {
            var tempHitRecord = new HitRecord();

            var hitAnything = false;

            var closestHitSoFar = p_tMax;
            foreach ( var target in WorldHitTargets )
            {
                if ( !target.WasHit(p_ray, p_tMin, closestHitSoFar, ref tempHitRecord) ) continue;
                hitAnything     = true;
                closestHitSoFar = tempHitRecord.T;
                p_hitRecord     = tempHitRecord;
            }

            return hitAnything;
        }
    }
}
