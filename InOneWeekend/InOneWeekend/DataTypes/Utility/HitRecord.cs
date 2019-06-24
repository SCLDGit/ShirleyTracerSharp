using InOneWeekend.DataTypes.Materials;

namespace InOneWeekend.DataTypes.Utility
{
    internal struct HitRecord
    {
        internal double T { get; set; }
        internal Vec3 Point { get; set; }
        internal Vec3 Normal { get; set; }
        internal IMaterial Material { get; set; }
    }
}
