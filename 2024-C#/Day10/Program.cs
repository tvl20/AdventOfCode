
using System.Diagnostics;
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
        Console.WriteLine(" ---- Part 1 ---- ");
        Part1();

        Console.WriteLine(" ---- Part 2 ---- ");
        Part2();
    }

    private static void Part1()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        Map<int> map = new Map<int>(DayReader.GetInputReader());
        stopwatch.Stop();
        Console.WriteLine("Read Input: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Restart();
        List<Position> trailHeads = map.FindAll(0);
        stopwatch.Stop();
        // Console.WriteLine("Trail Heads: " + ListToString(trailHeads, ", "));
        Console.WriteLine("Determine Heads: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Restart();
        int sum = 0;
        foreach (Position pos in trailHeads)
        {
            Dictionary<Position, int> reachablePeaks = new();
            CheckCompletableRoutes(ref reachablePeaks, map, pos);
            sum += reachablePeaks.Count;
        }
        stopwatch.Stop();
        Console.WriteLine("Peak sum: " + sum, ", ");
        Console.WriteLine("Route Calculation: " + stopwatch.ElapsedMilliseconds + "ms");
    }

    private static void Part2()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        Map<int> map = new Map<int>(DayReader.GetInputReader());
        stopwatch.Stop();
        Console.WriteLine("Read Input: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Restart();
        List<Position> trailHeads = map.FindAll(0);
        stopwatch.Stop();
        // Console.WriteLine("Trail Heads: " + ListToString(trailHeads, ", "));
        Console.WriteLine("Determine Heads: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Restart();
        int sum = 0;
        foreach (Position pos in trailHeads)
        {
            Dictionary<Position, int> reachablePeaks = new();
            CheckCompletableRoutes(ref reachablePeaks, map, pos);

            foreach (var item in reachablePeaks)
            {
                sum += item.Value;
            }
        }
        stopwatch.Stop();
        Console.WriteLine("Peak sum: " + sum, ", ");
        Console.WriteLine("Route Calculation: " + stopwatch.ElapsedMilliseconds + "ms");
    }

    private static void CheckCompletableRoutes(ref Dictionary<Position, int> reachablePeaks, Map<int> map, Position checkPosition, int prevHeight = -1)
    {

        if (!map.IsWithinBounds(checkPosition)) return; // keep within bounds

        int currentHeight = map[checkPosition];
        if (currentHeight != prevHeight + 1) return; // keep going up

        if (currentHeight == 9) // end reached
        {
            if (reachablePeaks.ContainsKey(checkPosition))
            {
                reachablePeaks[checkPosition]++;
            }
            else
            {
                reachablePeaks[checkPosition] = 1;
            }
            return;
        }

        CheckCompletableRoutes(ref reachablePeaks, map, checkPosition + Direction.Up, currentHeight);
        CheckCompletableRoutes(ref reachablePeaks, map, checkPosition + Direction.Right, currentHeight);
        CheckCompletableRoutes(ref reachablePeaks, map, checkPosition + Direction.Down, currentHeight);
        CheckCompletableRoutes(ref reachablePeaks, map, checkPosition + Direction.Left, currentHeight);
    }
}