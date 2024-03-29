﻿using InOneWeekend.DataTypes.Utility;

namespace InOneWeekend.DataTypes.Materials
{
    internal interface IMaterial
    {
        bool Scatter(Ray p_rayIn, HitRecord p_hitRecord, ref Color p_attenuation, ref Ray p_scatteredRay);
    }
}
