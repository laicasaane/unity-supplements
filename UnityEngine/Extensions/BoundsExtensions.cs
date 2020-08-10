namespace UnityEngine
{
    public static class BoundsExtensions
    {
        public static void Deconstruct(in this BoundsInt self, out int xMin, out int yMin, out int zMin,
                                       out int sizeX, out int sizeY, out int sizeZ)
        {

            xMin = self.xMin;
            yMin = self.yMin;
            zMin = self.zMin;
            sizeX = self.size.x;
            sizeY = self.size.y;
            sizeZ = self.size.z;
        }

        public static void Deconstruct(in this Bounds self, out Vector3 center, out Vector3 size)
        {
            center = self.center;
            size = self.size;
        }

        public static void Deconstruct(in this Bounds self, out Vector3 center, out Vector3 size,
                                       out Vector3 min, out Vector3 max)
        {
            center = self.center;
            size = self.size;
            min = self.min;
            max = self.max;
        }

        public static void Deconstruct(in this BoundsInt self, out Vector3Int position, out Vector3Int size)
        {
            position = self.position;
            size = self.size;
        }

        public static void Deconstruct(in this BoundsInt self, out Vector3Int position, out Vector3Int size,
                                       out Vector3Int min, out Vector3Int max)
        {
            position = self.position;
            size = self.size;
            min = self.min;
            max = self.max;
        }
    }
}