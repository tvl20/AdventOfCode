
using Day11;
using System.Diagnostics;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        SolvePart1();

        Console.WriteLine(" ---- part 2 ---- ");

        BigSolver solver = new BigSolver();
        long totalStones = solver.DetermineStoneAmount();
        Console.WriteLine("TOTAL: " + totalStones);
    }

    private static void SolvePart1()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        List<long> allStones = LoadStones();
        stopwatch.Stop();
        Console.WriteLine("Load Input: " + stopwatch.ElapsedMilliseconds + "ms");

        long timeElapsed = 0;
        for (int i = 0; i < 25; i++)
        {
            stopwatch.Restart();

            List<long> newStones = new List<long>();

            foreach (long stone in allStones)
            {
                newStones.AddRange(BlinkStone(stone));
            }
            
            //Console.WriteLine("newstones: " + ListToString(newStones, " "));

            allStones = newStones;

            stopwatch.Stop();
            Console.WriteLine("Iteration: " + i.ToString() + " Time: " + stopwatch.ElapsedMilliseconds + "ms");
            timeElapsed += stopwatch.ElapsedMilliseconds;
        }

        Console.WriteLine("Total Time Run Data: " + timeElapsed + "ms");

        Console.WriteLine("Total Stones: " + allStones.Count);

    }

    public static List<long> LoadStones()
    {
        List<long> stones = new List<long>();
        string input = "";

        using (StreamReader reader = DayReader.GetInputReader())
        {
            input = reader.ReadLine();
        }

        foreach (string str in input.Split(' '))
        {
            stones.Add(long.Parse(str));
        }

        return stones;
    }

    public static List<long> BlinkStone(long stoneValue)
    {
        List<long> list = new List<long>();

        if (stoneValue == 0)
        {
            list.Add(1);
        }
        else if (stoneValue.ToString().Length % 2 == 0)
        {
            string value = stoneValue.ToString();
            int size = value.Length / 2;
            list.Add(long.Parse(value.Substring(0, size)));
            list.Add(long.Parse(value.Substring(size, size)));
        }
        else
        {
            list.Add(stoneValue * 2024);
        }

        return list;
    }
}