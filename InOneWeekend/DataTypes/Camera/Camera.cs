using InOneWeekend.DataTypes.Utility;

using Math = System.Math;

namespace InOneWeekend.DataTypes.Camera
{
    internal class Camera
    {
        internal Vec3 Origin          { get; private set; }
        internal Vec3 LowerLeftCorner { get; private set; }
        internal Vec3 HorizontalSize  { get; private set; }
        internal Vec3 VerticalSize    { get; private set; }
        internal Vec3 U { get; private set; }
        internal Vec3 V { get; private set; }
        internal Vec3 W { get; private set; }
        internal double ApertureRadius { get; private set; }

        public Camera(Vec3 p_lookFrom, Vec3 p_lookAt, Vec3 p_upVector, double p_verticalFieldOfView, double p_aspectRatio, double p_aperture, double p_focalLength)
        {
            ApertureRadius = p_aperture / 2;
            var theta = p_verticalFieldOfView * Math.PI / 180;
            var halfHeight = Math.Tan(theta / 2);
            var halfWidth = p_aspectRatio * halfHeight;

            Origin = p_lookFrom;

            W = Vec3.GetUnitVector(p_lookFrom - p_lookAt);
            U = Vec3.GetUnitVector(Vec3.GetCrossProduct(p_upVector, W));
            V = Vec3.GetCrossProduct(W, U);

            LowerLeftCorner = Origin - halfWidth * p_focalLength * U - halfHeight * p_focalLength * V - p_focalLength * W;
            HorizontalSize = 2 * halfWidth * p_focalLength * U;
            VerticalSize = 2 * halfHeight * p_focalLength * V;
        }

        public Ray GetRay(double p_s, double p_t)
        {
            var rd = ApertureRadius * Utility.Math.GetRandomPositionOnUnitDisk();
            var offset = U * rd.X + V * rd.Y;
            return new Ray(Origin + offset, LowerLeftCorner + p_s * HorizontalSize + p_t * VerticalSize - Origin - offset);
        }
    }
}
