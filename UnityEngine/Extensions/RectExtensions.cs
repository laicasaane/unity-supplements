namespace UnityEngine
{
    public static class RectExtensions
    {
        public static void Deconstruct(in this Rect self, out float x, out float y, out float width, out float height)
        {
            x = self.x;
            y = self.y;
            width = self.width;
            height = self.height;
        }

        public static void Deconstruct(in this RectInt self, out int x, out int y, out int width, out int height)
        {
            x = self.x;
            y = self.y;
            width = self.width;
            height = self.height;
        }

        public static void Deconstruct(in this Rect self, out Vector2 position, out Vector2 size)
        {
            position = self.position;
            size = self.size;
        }

        public static void Deconstruct(in this RectInt self, out Vector2Int position, out Vector2Int size)
        {
            position = self.position;
            size = self.size;
        }

        public static void Deconstruct(this RectOffset self, out int left, out int right, out int top, out int bottom)
        {
            left = self.left;
            right = self.right;
            top = self.top;
            bottom = self.bottom;
        }
    }
}