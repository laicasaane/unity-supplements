namespace UnityEngine
{
    public static class RayExtensions
    {
        public static void Deconstruct(this Ray self, out Vector3 origin, out Vector3 direction)
        {
            origin = self.origin;
            direction = self.direction;
        }

        public static void Deconstruct(this Ray2D self, out Vector3 origin, out Vector3 direction)
        {
            origin = self.origin;
            direction = self.direction;
        }

        public static Ray With(this Ray self, in Vector3? origin = null, in Vector3? direction = null)
            => new Ray(
                origin ?? self.origin,
                direction ?? self.direction
            );

        public static Ray2D With(this Ray2D self, in Vector3? origin = null, in Vector3? direction = null)
            => new Ray2D(
                origin ?? self.origin,
                direction ?? self.direction
            );
    }
}