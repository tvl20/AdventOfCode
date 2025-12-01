using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6.Part2
{
    public class SynchronousSolver
    {
        private static Vector2 currentPosition = new();
        private static Vector2.Direction currentDirection;


        public int Solve(List<List<char>> map)
        {
            List<Vector2> visitedLocations = new();

            Vector2.Direction prevDir = currentDirection;

            while (!OutOfBounds(map, currentPosition + prevDir))
            {
                while (!OutOfBounds(map, currentPosition) && map[currentPosition.y][currentPosition.x] != '#')
                {
                    if (!visitedLocations.Contains(currentPosition)) visitedLocations.Add(currentPosition);
                    currentPosition = currentPosition + currentDirection;
                }

                currentPosition -= currentDirection;

                prevDir = currentDirection;

                switch (currentDirection)
                {
                    case Vector2.Direction.Up:
                        currentDirection = Vector2.Direction.Right;
                        break;
                    case Vector2.Direction.Down:
                        currentDirection = Vector2.Direction.Left;
                        break;
                    case Vector2.Direction.Left:
                        currentDirection = Vector2.Direction.Up;
                        break;
                    case Vector2.Direction.Right:
                        currentDirection = Vector2.Direction.Down;
                        break;
                }
            }

            //Console.WriteLine(visitedLocations.Count);
            return visitedLocations.Count;
        }

        private static bool OutOfBounds(List<List<char>> map, Vector2 pos)
        {
            if (pos.x < 0 || pos.y < 0) return true;
            if (map.Count <= pos.y || map[pos.y].Count <= pos.x)
                return true;
            return false;
        }

        public static List<List<char>> LoadData()
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
                        currentDirection = Vector2.Direction.Up;
                        currentPosition.x = line.IndexOf('^');
                        currentPosition.y = data.Count - 1;
                    }
                }
            }

            return data;
        }
    }
}
