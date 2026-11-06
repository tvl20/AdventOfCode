
using Day23;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using System.Text;

internal class Program
{
    private static string ListToString<T>(List<T> list, string seperator = "")
    {
        StringBuilder sb = new StringBuilder();
        foreach (T item in list)
        {
            sb.Append(item.ToString() + seperator);
        }
        return sb.ToString();
    }

    private static void Main(string[] args)
    {
        // Part1();
        Part2();
    }

    private static void Part2()
    {
        Dictionary<string, List<string>> connections = GetConnections();

        Stopwatch sw = new Stopwatch();
        sw.Start();

        HashSet<string> biggestSet = new HashSet<string>();
        //foreach (string initialConnection in connections.Keys)
        //for (int i = 0; i < connections.Keys.Count; i++) 
        while (connections.Count > 0) 
        {
            string startingNode = connections.Keys.First();
            HashSet<string> set = GetBiggestLoop(connections, startingNode, new());

            if (set.Count > biggestSet.Count) 
                biggestSet = new HashSet<string>(set.ToList());

            connections.Remove(startingNode);

            sw.Stop();
            Console.WriteLine(" Time: " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();
        }

        Console.WriteLine("Biggest set is " + biggestSet.Count);
        List<string> connList = biggestSet.ToList();
        connList.Sort();
        string output = ListToString(connList, ",");
        Console.WriteLine(output);
    }

    private static HashSet<string> GetBiggestLoop(Dictionary<string, List<string>> connectionMap, 
        string currentConnection, HashSet<string> previousConnections)
    {
        if (!connectionMap.ContainsKey(currentConnection)) return previousConnections;
        if (previousConnections.Contains(currentConnection)) return previousConnections;
        if (connectionMap[currentConnection].Intersect(previousConnections).Count() != previousConnections.Count) return previousConnections;
        
        HashSet<string> allConnections = new HashSet<string>(previousConnections.ToList());
        allConnections.Add(currentConnection);

        HashSet<string> output = new();
        foreach (string conn in connectionMap[currentConnection])
        {
            HashSet<string> newMap = GetBiggestLoop(connectionMap, conn, allConnections);
            if (output.Count < newMap.Count) output = newMap;
        }

        return output;
    }

    private static void Part1()
    {
        Dictionary<string, List<string>> connections = GetConnections();
        List<ConnectionSet> triangles = new();

        Console.WriteLine("Keys " + connections.Keys.Count);

        foreach (string connection in connections.Keys)
        {
            if (!connection.StartsWith('t')) 
                continue;

            foreach (string subConnection in connections[connection])
            {
                foreach (string thirdConnection in connections[subConnection])
                {
                    if (connections[connection].Contains(thirdConnection))
                    {
                        // tri formed
                        ConnectionSet triangle = new ConnectionSet(
                                new List<string>()
                                {
                                        connection, subConnection, thirdConnection
                                });
                        if (!triangles.Contains(triangle))
                            triangles.Add(triangle);
                    }
                }
            }
        }

        Console.WriteLine(triangles.Count);
    }

    private static Dictionary<string, List<string>> GetConnections()
    {
        Dictionary<string, List<string>> connections = new();

        using (StreamReader sr = DayReader.GetInputReader())
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            { 
                string[] conn = line.Split('-');
                
                // add connection
                connections.TryAdd(conn[0], new());
                connections[conn[0]].Add(conn[1]);

                // add reverse connection
                connections.TryAdd(conn[1], new());
                connections[conn[1]].Add(conn[0]);
            }
        }

        return connections;
    }
}