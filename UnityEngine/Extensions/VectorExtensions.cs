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

        public static Vector2 With(in this Vector2 self, float? x = null, float? y = null)
            => new Vector2(
                x ?? self.x,
                y ?? self.y
            );

        public static Vector3 With(in this Vector3 self, float? x = null, float? y = null, float? z = null)
            => new Vector3(
                x ?? self.x,
                y ?? self.y,
                z ?? self.z
            );

        public static Vector4 With(in this Vector4 self, float? x = null, float? y = null, float? z = null, float? w = null)
            => new Vector4(
                x ?? self.x,
                y ?? self.y,
                z ?? self.z,
                w ?? self.w
            );

        public static Vector2Int With(in this Vector2Int self, int? x = null, int? y = null)
            => new Vector2Int(
                x ?? self.x,
                y ?? self.y
            );

        public static Vector3Int With(in this Vector3Int self, int? x = null, int? y = null, int? z = null)
            => new Vector3Int(
                x ?? self.x,
                y ?? self.y,
                z ?? self.z
            );
    }
}