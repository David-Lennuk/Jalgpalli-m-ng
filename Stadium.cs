namespace Jalgpalli
{
    public class Stadium
    {
        public int Width { get; }
        public int Height { get; }

        public Stadium(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public bool IsIn(double x, double y)
        {
            return x >= 0 && x < Width && y >= 0 && y < Height;
        }
    }
}
