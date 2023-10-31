namespace Common
{
    public enum RunDirection
    {
        Right,
        Left,
    }

    internal static class RunDirectionTools
    {
        public static float ToFloat(this RunDirection direction)
        {
            return direction == RunDirection.Right ? 1f : -1f;
        }

        public static RunDirection ToOpposite(this RunDirection direction)
        {
            return direction == RunDirection.Right ? RunDirection.Left : RunDirection.Right;
        }
    }
}
