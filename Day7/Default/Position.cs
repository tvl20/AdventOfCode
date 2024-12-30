public class Position
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Position()
    {
        X = 0;
        Y = 0;
    }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Position operator +(Position original, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                return new Position(original.X - 1, original.Y);
            case Direction.Right:
                return new Position(original.X + 1, original.Y);
            case Direction.Up:
                return new Position(original.X, original.Y - 1);
            case Direction.Down:
                return new Position(original.X, original.Y + 1);
            default:
                return new Position(original.X, original.Y);
        }
    }
}
