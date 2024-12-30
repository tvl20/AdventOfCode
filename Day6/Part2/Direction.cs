using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Day6.Part2
{
    public class Direction
    {
        public readonly int x;
        public readonly int y;

        public Direction(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static readonly Direction up = new(0, -1);
        public static readonly Direction down = new(0, 1);
        public static readonly Direction right = new(1, 0);
        public static readonly Direction left = new(-1, 0);

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            Direction? other = obj as Direction;
            if (other == null) return false;
            return other.x == x && other.y == y;
        }
    }
}
