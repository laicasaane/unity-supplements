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
    }
}