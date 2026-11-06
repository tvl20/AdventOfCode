using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    public class ConnectionSet
    {
        public List<string> Connections = new();

        public ConnectionSet(List<string> connections)
        {
            foreach (var connection in connections)
            {
                Connections.Add(connection);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (!(obj is ConnectionSet)) return false;

            ConnectionSet other = (ConnectionSet)obj;
            if (other.Connections.Count != Connections.Count) return false;

            List<string> otherConnections = new List<string>(other.Connections.ToList());
            List<string> currentConnections = new List<string>(this.Connections.ToList());
            otherConnections.Sort();
            currentConnections.Sort();

            for (int i = 0; i < otherConnections.Count; i++)
            {
                if (!otherConnections[i].Equals(currentConnections[i])) return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            List<string> sortedList = new List<string>(Connections.ToList());
            sortedList.Sort();
            return HashCode.Combine(sortedList);
        }
    }
}
