﻿using TheNextWeek.DataTypes.Utility;

namespace TheNextWeek.DataTypes.Materials
{
    internal interface IMaterial
    {
        bool Scatter(Ray p_rayIn, HitRecord p_hitRecord, ref Color p_attenuation, ref Ray p_scatteredRay);

        Color GetEmitted(double p_u, double p_v, Vec3 p_point);
    }
}
