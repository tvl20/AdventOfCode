

using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("part 1; ");
        Part1();

        Console.WriteLine("part 2; ");
        Part2();
    }

    private static void Part2()
    {
        StreamReader sr = DayReader.GetInputReader();
        int numbSafe = 0;

        using (sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                List<int> input = ConvertToIntList(line.Split(" "));
                if (IsSafeAscDamped(input, false) || IsSafeDscDamped(input, false))
                {
                    numbSafe++;
                }
            }
        }

        Console.WriteLine(numbSafe);
    }

    private static void Part1()
    {
        StreamReader sr = DayReader.GetInputReader();
        int numbSafe = 0;

        using (sr)
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                List<int> input = ConvertToIntList(line.Split(" "));
                if (IsSafeAsc(input) || IsSafeDsc(input))
                {
                    numbSafe++;
                }
            }
        }

        Console.WriteLine(numbSafe);
    }

    private static List<int> ConvertToIntList(string[] numbers)
    {
        List<int> result = new List<int>();
        foreach (string number in numbers)
        {
            int i;
            if (int.TryParse(number, out i)) result.Add(i);
        }
        return result;
    }

    private static bool IsSafeAscDamped(List<int> inputList, bool strict)
    {
        for (int i = 1; i < inputList.Count; i++)
        {
            int diff = inputList[i] - inputList[i - 1];
            if (diff <= 0 || diff > 3)
            {
                if (strict) return false;

                List<int> alt1 = inputList.ToList();
                List<int> alt2 = inputList.ToList();
                alt1.RemoveAt(i - 1);
                alt2.RemoveAt(i);
                return IsSafeAscDamped(alt1, true) || IsSafeAscDamped(alt2, true);
            }
        }

        return true;
    }

    private static bool IsSafeDscDamped(List<int> inputList, bool strict)
    {
        for (int i = 1; i < inputList.Count; i++)
        {
            int diff = inputList[i - 1] - inputList[i];
            if (diff <= 0 || diff > 3)
            {
                if (strict) return false;

                List<int> alt1 = inputList.ToList();
                List<int> alt2 = inputList.ToList();
                alt1.RemoveAt(i - 1);
                alt2.RemoveAt(i);
                return IsSafeDscDamped(alt1, true) || IsSafeDscDamped(alt2, true);
            }
        }

        return true;
    }

    private static bool IsSafeAsc(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            int diff = list[i] - list[i - 1];
            if (diff <= 0 || diff > 3) return false;
        }

        return true;
    }

    private static bool IsSafeDsc(List<int> list)
    {
        for (int i = 1; i < list.Count; i++)
        {
            int diff = list[i - 1] - list[i];
            if (diff <= 0 || diff > 3) return false;
        }

        return true;
    }
}