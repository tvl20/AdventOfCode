using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    public class Vector2 : ICloneable
    {
        public enum Direction { Up, Down, Left, Right }

        public int x, y;
        public Vector2(int x, int y) { this.x = x; this.y = y; }
        public Vector2() : this(0, 0) { }

        public static Vector2 operator +(Vector2 v1, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return new Vector2(v1.x, v1.y - 1);
                case Direction.Down:
                    return new Vector2(v1.x, v1.y + 1);
                case Direction.Left:
                    return new Vector2(v1.x - 1, v1.y);
                case Direction.Right:
                    return new Vector2(v1.x + 1, v1.y);
            }

            return v1;
        }

        public static Vector2 operator -(Vector2 v1, Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return v1 + Direction.Down;
                case Direction.Down:
                    return v1 + Direction.Up;
                case Direction.Left:
                    return v1 + Direction.Right;
                case Direction.Right:
                    return v1 + Direction.Left;
            }

            return v1;
        }

        public override string ToString()
        {
            return "x, y: " + x + ", " + y;
        }

        public override bool Equals(object? obj)
        {
            Vector2 v = obj as Vector2;
            if (v == null) return false;
            return v.x == x && v.y == y;
        }

        public object Clone()
        {
            return new Vector2(x, y);
        }
    }
}