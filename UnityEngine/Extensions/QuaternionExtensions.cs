namespace UnityEngine
{
    public static class QuaternionExtensions
    {
        public static void Deconstruct(in this Quaternion self, out float x, out float y, out float z, out float w)
        {
            x = self.x;
            y = self.y;
            z = self.z;
            w = self.w;
        }

        public static Quaternion With(in this Quaternion self, float? x = null, float? y = null, float? z = null, float? w = null)
            => new Quaternion(
                x ?? self.x,
                y ?? self.y,
                z ?? self.z,
                w ?? self.w
            );

        public static Quaternion WithEuler(this Quaternion self, float? x = null, float? y = null, float? z = null)
        {
            var e = self.eulerAngles;

            return Quaternion.Euler(
                x ?? e.x,
                y ?? e.y,
                z ?? e.z
            );
        }

        public static Quaternion WithAngleAxis(this Quaternion self, float? angle = null, in Vector3? axis = null)
        {
            self.ToAngleAxis(out var selfAngle, out var selfAxis);

            return Quaternion.AngleAxis(
                angle ?? selfAngle,
                axis ?? selfAxis
            );
        }
    }
}