namespace Common
{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down,
        None
    }

    public enum VerticalDirection
    {
        Up,
        Down,
    }

    internal static class FlyDirectionTools
    {
        public static float ToFloat(this VerticalDirection direction)
        {
            return direction == VerticalDirection.Up ? 1f : -1f;
        }

        public static VerticalDirection ToOpposite(this VerticalDirection direction)
        {
            return direction == VerticalDirection.Up
                ? VerticalDirection.Down
                : VerticalDirection.Up;
        }
    }

    public enum HorizontalDirection
    {
        Right,
        Left,
    }

    internal static class RunDirectionTools
    {
        public static float ToFloat(this HorizontalDirection direction)
        {
            return direction == HorizontalDirection.Right ? 1f : -1f;
        }

        public static HorizontalDirection ToOpposite(this HorizontalDirection direction)
        {
            return direction == HorizontalDirection.Right
                ? HorizontalDirection.Left
                : HorizontalDirection.Right;
        }
    }
}
