namespace UnityEngine
{
    public static class RayExtensions
    {
        public static void Deconstruct(in this Ray self, out Vector3 origin, out Vector3 direction)
        {
            origin = self.origin;
            direction = self.direction;
        }

        public static void Deconstruct(in this Ray2D self, out Vector3 origin, out Vector3 direction)
        {
            origin = self.origin;
            direction = self.direction;
        }
    }
}