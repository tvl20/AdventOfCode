
using System.Security.Cryptography;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }

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

    public static Position operator -(Position original, Direction dir)
    {
        switch (dir)
        {
            case Direction.Left:
                return new Position(original.X + 1, original.Y);
            case Direction.Right:
                return new Position(original.X - 1, original.Y);
            case Direction.Up:
                return new Position(original.X, original.Y + 1);
            case Direction.Down:
                return new Position(original.X, original.Y - 1);
            default:
                return new Position(original.X, original.Y);
        }
    }

    public static Position operator +(Position original, Position other)
    {
        return new Position(original.X + other.X, original.Y + other.Y);
    }
    
    public static Position operator -(Position original, Position other)
    {
        return new Position(original.X - other.X, original.Y - other.Y);
    }

    public static Position Difference(Position original, Position other)
    {
        return new Position(
            Math.Abs(original.X - other.X),
            Math.Abs(original.Y - other.Y)
            );
    }

    public Position Difference(Position other)
    {
        return Position.Difference(this, other);
    }

    public override bool Equals(object? obj)
    {
        return obj is Position position &&
               X == position.X &&
               Y == position.Y;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override string ToString()
    {
        return "(" + X + ", " + Y + ")";
    }
}
