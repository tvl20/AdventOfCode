
using System.Reflection.Metadata.Ecma335;

internal class Program
{
    private static Vector2 currentPosition = new();

    private class Vector2
    {
        public int x, y;
        public Vector2(int x, int y) { this.x = x; this.y = y; }
        public Vector2() : this(0, 0) { }
    }

    private enum Direction { Up, Down, Left, Right }

    private static void Main(string[] args)
    {
        Part1();
    }

    private static void Part1()
    {
        List<List<char>> map = LoadData();


    }

    private static List<List<char>> LoadData()
    {
        List<List<char>> data = new();
        using (StreamReader sr = DayReader.GetInputReader())
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                data.Add(new(line.ToCharArray()));

                if (line.Contains('^'))
                {
                    currentPosition.x = line.IndexOf('^');
                    currentPosition.y = data.Count - 1;
                }
            }
        }

        return data;
    }
}