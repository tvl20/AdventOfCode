using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public class Region
    {
        public char Label;
        public HashSet<Position> AreaPoints;
        public int Perimiter;

        public Region(char label)
        {
            Label = label;
            AreaPoints = new HashSet<Position>();
            Perimiter = 0;
        }

        public bool TryAdd(Position area)
        {
            if (AreaPoints.Contains(area)) return false;
            AreaPoints.Add(area);
            return true;
        }

        public bool Contains(Position area)
        {
            return AreaPoints.Contains(area);
        }

        public int GetTotalCost()
        {
            return AreaPoints.Count * Perimiter;
        }
    }
}
