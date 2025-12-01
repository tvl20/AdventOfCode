using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class Node : IComparable<Node>
    {
        public Position Position;
        public float Priority;

        public Node(Position position, float priority)
        {
            Position = position;
            Priority = priority;
        }

        public int CompareTo(Node other)
        {
            return other.Priority < this.Priority ? -1 : 1;
        }

        public override bool Equals(object? obj)
        {
            return obj is Node node &&
                   EqualityComparer<Position>.Default.Equals(Position, node.Position) &&
                   Priority == node.Priority;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Priority);
        }
    }
}
