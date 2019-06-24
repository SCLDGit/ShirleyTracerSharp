﻿using System;
using System.Collections.Generic;
using System.Text;

using TheNextWeek.DataTypes.Materials;
using TheNextWeek.DataTypes.Utility;

using Math = System.Math;

namespace TheNextWeek.DataTypes.Shapes
{
    class MovingSphere : IHitTarget
    {
        private Vec3 Center1 { get; }
        private Vec3 Center2 { get; }
        private double Time1 { get; }
        private double Time2 { get; }
        private double Radius { get; }
        private IMaterial Material { get; }

        internal MovingSphere(Vec3 p_center1, Vec3 p_center2, double p_time1, double p_time2, double p_radius, IMaterial p_material)
        {
            Center1 = p_center1;
            Center2 = p_center2;
            Time1 = p_time1;
            Time2 = p_time2;
            Radius = p_radius;
            Material = p_material;
        }

        public Vec3 GetCenter(double p_time)
        {
            return Center1 + ((p_time - Time1) / (Time2 - Time1)) * (Center2 - Center1);
        }

        public bool WasHit(Ray p_ray, double p_tMin, double p_tMax, ref HitRecord p_hitRecord)
        {
            // If the quadratic is confusing, remember that several 2s are pre-canceled out. - Comment by Matt Heimlich on 06/23/2019 @ 12:15:02
            var oc           = p_ray.Origin - GetCenter(p_ray.Time);
            var a            = Vec3.GetDotProduct(p_ray.Direction, p_ray.Direction);
            var b            = Vec3.GetDotProduct(oc, p_ray.Direction);
            var c            = Vec3.GetDotProduct(oc, oc) - Radius * Radius;
            var discriminant = b * b - a * c;
            if (discriminant > 0)
            {
                var sqrtCache = Math.Sqrt(b * b - a * c);
                var temp      = (-b - sqrtCache) / a;
                if (temp < p_tMax && temp > p_tMin)
                {
                    p_hitRecord.T        = temp;
                    p_hitRecord.Point    = p_ray.PointAt(p_hitRecord.T);
                    p_hitRecord.Normal   = (p_hitRecord.Point - GetCenter(p_ray.Time)) / Radius;
                    p_hitRecord.Material = Material;
                    return true;
                }
                temp = (-b + sqrtCache) / a;
                if (temp < p_tMax && temp > p_tMin)
                {
                    p_hitRecord.T        = temp;
                    p_hitRecord.Point    = p_ray.PointAt(p_hitRecord.T);
                    p_hitRecord.Normal   = (p_hitRecord.Point - GetCenter(p_ray.Time)) / Radius;
                    p_hitRecord.Material = Material;
                    return true;
                }
            }

            return false;
        }

        public bool GenerateBoundingBox(double p_time1, double p_time2, ref BoundingBox p_box)
        {
            var box1 = new BoundingBox(GetCenter(p_time1) - new Vec3(Radius, Radius, Radius), GetCenter(p_time1) + new Vec3(Radius, Radius, Radius));
            var box2 = new BoundingBox(GetCenter(p_time2) - new Vec3(Radius, Radius, Radius), GetCenter(p_time2) + new Vec3(Radius, Radius, Radius));
            p_box = BoundingBox.GetSurroundingBox(box1, box2);
            return true;

        }
    }
}
