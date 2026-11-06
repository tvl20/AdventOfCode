

public class Program
{
    public class Antenna
    {
        public Position Position;
        public char Callsign;

        public Antenna(int x, int y, char callsign)
        {
            Position = new Position(x, y);
            Callsign = callsign;
        }

        public override bool Equals(object? obj)
        {
            return obj is Antenna antenna &&
                   EqualityComparer<Position>.Default.Equals(Position, antenna.Position) &&
                   Callsign == antenna.Callsign;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Position, Callsign);
        }
    }

    private static void Main(string[] args)
    {
        //Part1();
        Console.WriteLine("----");
        Part2();
    }

    private static void Part1()
    {
        Map<char> map = new Map<char>(DayReader.GetInputReader());

        HashSet<Antenna> antennas = GetAntennas(map);
        HashSet<Position> resonanceZones = GetResonanceZones(map, antennas.ToList());

        Console.WriteLine("Finished part1; " + resonanceZones.Count + " zones");

    }

    private static void Part2()
    {
        Map<char> map = new Map<char>(DayReader.GetInputReader());

        HashSet<Antenna> antennas = GetAntennas(map);
        HashSet<Position> resonanceZones = GetAllZones(map, antennas.ToList());

        Console.WriteLine("Finished part2; " + resonanceZones.Count + " zones");

    }

    private static HashSet<Position> GetAllZones(Map<char> map, List<Antenna> antennas)
    {
        HashSet<Position> locations = new();

        for (int i = 0; i < antennas.Count; i++) // current
        {
            bool hasResonance = false;
            for (int j = 0; j < antennas.Count; j++) // target
            {
                if (i == j) continue;

                if (antennas[i].Callsign == antennas[j].Callsign)
                {
                    hasResonance = true;

                    Position offset = antennas[j].Position - antennas[i].Position;

                    Position resonanceZoneLocation = antennas[j].Position + offset;
                    while (map.IsWithinBounds(resonanceZoneLocation))
                    {
                        locations.Add(resonanceZoneLocation);
                        resonanceZoneLocation = resonanceZoneLocation + offset;
                    }
                }

            }
            if (hasResonance) locations.Add(antennas[i].Position); // there's resonance, add current
        }

        return locations;
    }

    private static HashSet<Antenna> GetAntennas(Map<char> map)
    {
        HashSet<Antenna> result = new();

        for (int y = 0; y < map.MapHeight; y++)
        {
            for (int x = 0; x < map.MapWidth; x++)
            {
                if (map[x, y] != '.') result.Add(new Antenna(x, y, map[x, y]));
            }
        }

        return result;
    }

    private static HashSet<Position> GetResonanceZones(Map<char> map, List<Antenna> antennas)
    {
        HashSet<Position> locations = new();

        for (int i = 0; i < antennas.Count; i++) // current
        {
            for (int j = 0; j < antennas.Count; j++) // target
            {
                if (i == j) continue;

                if (antennas[i].Callsign == antennas[j].Callsign)
                {
                    Position offset = antennas[j].Position - antennas[i].Position;
                    Position resonanceZoneLocation = antennas[j].Position + offset;

                    if (map.IsWithinBounds(resonanceZoneLocation))
                    {
                        locations.Add(resonanceZoneLocation);
                    }
                }

            }
        }

        return locations;
    }
}