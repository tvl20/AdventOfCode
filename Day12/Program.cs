using Day12;
using System.Net.Http.Headers;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
    }

    private static void Part1()
    {
        Map<char> map = new Map<char>(DayReader.GetInputReader());

        int totalCost = 0;
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
                FindAllConnectingRegions(map, startEvalPoint, ref evalRegion);
                allRegions.Add(evalRegion);

                Console.WriteLine("evalRegion label: " + evalRegion.Label + 
                    " area: " + evalRegion.AreaPoints.Count + 
                    " perimiter: " + evalRegion.Perimiter);
                totalCost += evalRegion.GetTotalCost();
            }
        }

        Console.WriteLine("Finished; " + allRegions.Count + " regions cost: " + totalCost);
    }

    // this doesn't work properly??
    private static bool IsContainedIn(List<Region> regions, Position position)
    {
        foreach (Region region in regions)
        {
            if (region.Contains(position)) return true;
        }
        return false;
    }

    private static void FindAllConnectingRegions(Map<char> map, Position evalPoint, ref Region region)
    {
        if (!region.TryAdd(evalPoint)) return; // point already exists, so has been evaled before

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
                FindAllConnectingRegions(map, neighbour, ref region);
            }
        }

        // TODO: check if bordering the edge of the map, those also count as perimiter points
    }

    private static string ListToString<T>(List<T> list, string seperator = "")
    {
        StringBuilder sb = new StringBuilder();
        foreach (T item in list)
        {
            sb.Append(item.ToString() + seperator);
        }
        return sb.ToString();
    }
}