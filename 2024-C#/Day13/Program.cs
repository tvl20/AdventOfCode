
using Day13;
using System.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
    }

    // 0 = goal; 1+ = steps
    private static List<List<Position>> LoadMachines()
    {
        List<List<Position>> machines = new List<List<Position>>();

        using (StreamReader sr = DayReader.GetInputReader())
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] coordinates = line.Split(":");
                coordinates = coordinates[1].Split(",");
                int x = int.Parse(coordinates[0].Split("+")[1]);
                int y = int.Parse(coordinates[1].Split("+")[1]);
                Position buttonA = new Position(x, y);

                line = sr.ReadLine();
                coordinates = line.Split(":");
                coordinates = coordinates[1].Split(",");
                x = int.Parse(coordinates[0].Split("+")[1]);
                y = int.Parse(coordinates[1].Split("+")[1]);
                Position buttonB = new Position(x, y);

                line = sr.ReadLine();
                coordinates = line.Split(":");
                coordinates = coordinates[1].Split(",");
                x = int.Parse(coordinates[0].Split("=")[1]);
                y = int.Parse(coordinates[1].Split("=")[1]);
                Position goal = new Position(x, y);

                machines.Add(new List<Position> { goal, buttonA, buttonB });
                line = sr.ReadLine();
            }
        }

        return machines;
    }

    private static void Part1()
    {
        List<List<Position>> machines = LoadMachines();
        List<Position> solution = FindPath(new(), machines[0][0], machines[0].GetRange(1, machines[0].Count -1));
        Console.WriteLine("out 1 " + solution.Count);
    }

    private static List<Position> FindPath(Position start, Position end, List<Position> validSteps)
    {
        //PriorityQueue<Position, float> possiblePaths = new PriorityQueue<Position, float>();
        //SortedList<Position, float> possiblePaths = new SortedList<Position, float>();
        SortedSet<Node> possiblePaths = new SortedSet<Node>();

        possiblePaths.Add(new Node(start, 0));

        Dictionary<Position, Position> previousNodeList = new Dictionary<Position, Position>();
        Dictionary<Position, float> priceToNodeList = new Dictionary<Position, float>(); // G
        Dictionary<Position, float> priceToEndList = new Dictionary<Position, float>(); // F
        priceToNodeList[start] = 0;
        priceToEndList[start] = start.AbsoluteDifference(end);

        while (possiblePaths.Count > 0)
        {
            Node currentNode = possiblePaths.First();
            Position current = currentNode.Position;
            possiblePaths.Remove(currentNode);

            if (current.Equals(end))
                return ReconstructList(current, previousNodeList);

            foreach (Position possibleStep in validSteps)
            {
                Position step = current + possibleStep;
                if (step.X > end.X || step.Y > end.Y)
                {
                    continue;
                }

                // TODO: check if this weight for one step should be absdiff
                float priceToStep = priceToNodeList[current] + step.AbsoluteDifference(current);
                if (priceToStep < priceToNodeList.GetValueOrDefault(step, float.MaxValue))
                {
                    previousNodeList[step] = current;
                    priceToNodeList[step] = priceToStep;
                    priceToEndList[step] = priceToStep + step.AbsoluteDifference(end);

                    Node next = new Node(step, priceToEndList[step]);
                    if (!possiblePaths.Contains(next))
                        possiblePaths.Add(next);
                }
            }
        }

        return new(); // no path available
    }

    private static List<Position> ReconstructList(Position current, Dictionary<Position, Position> previousNodeList)
    {
        List<Position> path = new List<Position>() { current };
        Position eval = current;
        while (previousNodeList.ContainsKey(eval))
        {
            eval = previousNodeList[eval];
            path.Add(eval);
        }
        return path;
    }
}