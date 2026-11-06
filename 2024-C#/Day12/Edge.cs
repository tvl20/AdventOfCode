using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public class Edge
    {
        public List<Position> Area = new();

        public Position StartPosition => Area[0];
        public Position EndPosition => Area[Area.Count-1];

        public int GetLength()
        {
            return Area.Count;
        }
    }
}
