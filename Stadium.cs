namespace Jalgpalli;

public class Stadium
{
    public Stadium(int width, int height) // Создаём стадион
    {
        Width = width;
        Height = height;
    }

    public int Width { get; }

    public int Height { get; }

    public bool IsIn(double x, double y) // Проверка пределов стадиона
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
