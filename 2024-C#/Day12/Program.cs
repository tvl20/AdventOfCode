using Day12;
using System.Net;
using System.Net.Http.Headers;
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
        Part1();
        Part2();
    }

    private static void Part2()
    {
        Map<char> map = new Map<char>(DayReader.GetInputReader());
        List<Region> allRegions = FindAllRegions(map);


    }

    //private static int GetSides(Map<char> map, Region region)
    //{
    //    // get top-most, then left-most coordinate
    //    List<Position> points = region.AreaPoints.ToList();
    //    Position startPoint = new();

    //    foreach (Position point in points)
    //    {
    //        if (point.Y <= startPoint.Y && point.X < startPoint.X)
    //        {
    //            startPoint.X = point.X;
    //            startPoint.Y = point.Y;
    //        }
    //    }

    //    Position evalPoint = new();
    //    int sides;
    //    do
    //    {



    //    }
    //    while (!evalPoint.Equals(startPoint));


    //    return 0;
    //}

    //private static Edge GetConnectedBorderEdge(Map<char> map, Position start, Direction dir, Direction edgeSide)
    //{
    //    Edge edge = new Edge();
    //    edge.Area.Add(start);

    //    Position endPosition = start + dir;
    //    while (map.IsWithinBounds(endPosition) && !map.IsWithinBounds(endPosition + edgeSide))
    //    {
    //        edge.Area.Add(endPosition);
    //        endPosition = start + dir;
    //    }

    //    return edge;
    //}






    private static void Part1()
    {
        Map<char> map = new Map<char>(DayReader.GetInputReader());
        List<Region> allRegions = FindAllRegions(map);

        int totalCost = 0;
        foreach (Region evalRegion in allRegions)
        {
            Console.WriteLine("evalRegion label: " + evalRegion.Label +
                " area: " + evalRegion.AreaPoints.Count +
                " perimiter: " + evalRegion.Perimiter);
            totalCost += evalRegion.GetTotalCost();
        }

        Console.WriteLine("Finished; " + allRegions.Count + " regions cost: " + totalCost);
    }

    private static List<Region> FindAllRegions(Map<char> map)
    {
        List<Region> allRegions = new List<Region>();

        for (int y = 0; y < map.MapHeight; y++)
        {
            for (int x = 0; x < map.MapWidth; x++)
            {
                // don't re-evaluate
                if (IsContainedIn(allRegions, new Position(x, y))) continue;

                //Region evalRegion = new Region(map[0, 0]);
                Region evalRegion = new Region(map[x, y]);
                Position startEvalPoint = new Position(x, y);
                FindCompleteRegionConnecting(map, startEvalPoint, ref evalRegion);
                allRegions.Add(evalRegion);
            }
        }

        return allRegions;
    }

    private static bool IsContainedIn(List<Region> regions, Position position)
    {
        foreach (Region region in regions)
        {
            if (region.Contains(position)) return true;
        }
        return false;
    }

    private static void FindCompleteRegionConnecting(Map<char> map, Position evalPoint, ref Region region)
    {
        if (!region.TryAdd(evalPoint)) return; // evalPoint already exists, so has been evaled before

        List<Position> allAdjacent = map.FindNeighbours(evalPoint);

        // check perimiter points
        foreach (Position neighbour in allAdjacent)
        {
            if (map[neighbour] != region.Label)
            {
                region.Perimiter++; // todo; also count fence for shit that falls off the map -,-
            }
            else if (!region.Contains(neighbour))
            {
                // area not evaluated yet so check it
                FindCompleteRegionConnecting(map, neighbour, ref region);
            }
        }

        // TODO: check if bordering the edge of the map, those also count as perimiter points
        if (!map.IsWithinBounds(evalPoint + Direction.Up)) region.Perimiter++;
        if (!map.IsWithinBounds(evalPoint + Direction.Right)) region.Perimiter++;
        if (!map.IsWithinBounds(evalPoint + Direction.Down)) region.Perimiter++;
        if (!map.IsWithinBounds(evalPoint + Direction.Left)) region.Perimiter++;
    }
}