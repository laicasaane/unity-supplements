namespace UnityEngine
{
    public static class RectExtensions
    {
        public static void Deconstruct(this Rect self, out float x, out float y, out float width, out float height)
        {
            x = self.x;
            y = self.y;
            width = self.width;
            height = self.height;
        }

        public static void Deconstruct(this RectInt self, out int x, out int y, out int width, out int height)
        {
            x = self.x;
            y = self.y;
            width = self.width;
            height = self.height;
        }

        public static void Deconstruct(this Rect self, out Vector2 position, out Vector2 size)
        {
            position = self.position;
            size = self.size;
        }

        public static void Deconstruct(this RectInt self, out Vector2Int position, out Vector2Int size)
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

        public static Rect With(this Rect self, float? x = null, float? y = null, float? width = null, float? height = null)
            => new Rect(
                x ?? self.x,
                y ?? self.y,
                width ?? self.width,
                height ?? self.height
            );

        public static RectInt With(this RectInt self, int? x = null, int? y = null, int? width = null, int? height = null)
            => new RectInt(
                x ?? self.x,
                y ?? self.y,
                width ?? self.width,
                height ?? self.height
            );

        public static Rect With(this Rect self, Vector2? position = null, Vector2? size = null)
            => new Rect(
                position ?? self.position,
                size ?? self.size
            );

        public static RectInt With(this RectInt self, Vector2Int? position = null, Vector2Int? size = null)
            => new RectInt(
                position ?? self.position,
                size ?? self.size
            );

        public static RectOffset With(this RectOffset self, int? left = null, int? right = null, int? top = null, int? bottom = null)
            => new RectOffset(
                left ?? self.left,
                right ?? self.right,
                top ?? self.top,
                bottom ?? self.bottom
            );
    }
}