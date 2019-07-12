using TheNextWeek.DataTypes.Utility;

namespace TheNextWeek.DataTypes.Materials.Utility
{
    internal interface ITexture
    {
        Color GetValue(double p_u, double p_v, Vec3 p_point);
    }
}
