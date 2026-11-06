using System.Diagnostics;
using System.Text;
using Day24;

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

    public static Dictionary<string, IWireValue> connections;
    public static List<string> zGates;

    private static void Main(string[] args)
    {
        connections = new();
        zGates = new();

        Part1();
    }

    private static void Part1()
    {
        Stopwatch sw = new Stopwatch();

        using (StreamReader sr = DayReader.GetInputReader())
        {
            sw.Start();

            LoadWires(sr);

            sw.Stop();
            Console.WriteLine("Load wires: " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            LoadGates(sr);

            sw.Stop();
            Console.WriteLine("Load gates: " + sw.ElapsedMilliseconds + "ms");
        }

        sw.Restart();

        zGates.Sort();
        zGates.Reverse(); // for some reason??
        StringBuilder sb = new();
        foreach (string gate in zGates)
        {
            sb.Append(connections[gate].GetOutput().ToString());
        }
        string binary = sb.ToString();
        Console.WriteLine("Binary " + binary);
        Console.WriteLine("Decimal " + Convert.ToInt64(binary, 2).ToString());
    }

    private static void LoadGates(StreamReader sr)
    {
        // example; ntg XOR fgs -> mjb
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            string[] gateData = line.Split(" ");

            string resultID = gateData[4];
            if (resultID.StartsWith('z')) zGates.Add(resultID);

            IWireValue? storedValue;
            if (!connections.TryGetValue(resultID, out storedValue))
            {
                storedValue = new LogicGate(resultID);
                connections[resultID] = storedValue;
            }

            if (storedValue is LogicGate)
            {
                LogicGate gate = (LogicGate)storedValue;

                gate.left = GetGate(gateData[0]);
                gate.right = GetGate(gateData[2]);
                gate.mode = GetType(gateData[1]);
            }
            else
            {
                Console.WriteLine("ERROR TRYING TO LOAD IN WIRE TO GATE " + resultID);
            }
        }
    }

    private static LogicType GetType(string type)
    {
        if (type.Equals("OR")) return LogicType.OR;
        if (type.Equals("AND")) return LogicType.AND;
        return LogicType.XOR; // in case its neither or nor and it's xor
    }

    private static IWireValue GetGate(string gateID)
    {
        IWireValue? output;
        if (!connections.TryGetValue(gateID, out output))
        {
            output = new LogicGate(gateID);
            connections[gateID] = output;
        }
        return output;
    }

    private static void LoadWires(StreamReader sr)
    {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
            if (string.IsNullOrWhiteSpace(line)) break;

            string[] wireData = line.Split(": ");
            connections[wireData[0]] =
                new WireConnection(wireData[0], int.Parse(wireData[1]));
        }
    }
}