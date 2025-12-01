public class Map<T> where T : struct
{
    public int MapWidth => map[0].Count;
    public int MapHeight => map.Count;

    public T this[int x, int y] => map[y][x];
    public T this[Position pos] => map[pos.Y][pos.X];

    private List<List<T>> map = new();

    public Map(StreamReader reader)
    {
        map = new();

        using (reader)
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                map.Add(new());
                foreach (char c in line)
                {
                    map[map.Count - 1].Add((T)Convert.ChangeType("" + c, typeof(T)));
                }
            }
        }
    }

    public bool IsWithinBounds(Position pos)
    {
        bool xValid = pos.X >= 0 && pos.X < MapWidth;
        bool yValid = pos.Y >= 0 && pos.Y < MapHeight;

        return xValid && yValid;
    }

    public List<Position> FindAll(T value)
    {
        List<Position> list = new List<Position>();

        for (int y = 0; y < MapHeight; y++)
        {
            for (int x = 0; x < MapWidth; x++)
            {
                Position pos = new Position(x, y);
                if (this[pos].Equals(value))
                {
                    list.Add(pos);
                }
            }
        }

        return list;
    }
}
