namespace UnityEngine
{
    public static class VectorExtensions
    {
        public static void Deconstruct(in this Vector2 self, out float x, out float y)
        {
            x = self.x;
            y = self.y;
        }

        public static void Deconstruct(in this Vector3 self, out float x, out float y, out float z)
        {
            x = self.x;
            y = self.y;
            z = self.z;
        }

        public static void Deconstruct(in this Vector4 self, out float x, out float y, out float z, out float w)
        {
            x = self.x;
            y = self.y;
            z = self.z;
            w = self.w;
        }

        public static void Deconstruct(in this Vector2Int self, out int x, out int y)
        {
            x = self.x;
            y = self.y;
        }

        public static void Deconstruct(in this Vector3Int self, out int x, out int y, out int z)
        {
            x = self.x;
            y = self.y;
            z = self.z;
        }
    }
}