
using System.Text.RegularExpressions;

internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
        Part2();
    }

    private static void Part2()
    {
        StreamReader sr = DayReader.GetInputReader();

        string data;
        using (sr)
        {
            data = sr.ReadToEnd();
        }

        var matches = Regex.EnumerateMatches(data, "mul\\((\\d+,\\d+)\\)|do\\(\\)|don't\\(\\)");

        int total = 0;
        bool funcEnabled = true;

        foreach (ValueMatch match in matches)
        {
            string command = data.Substring(match.Index, match.Length);

            if (command == "do()") funcEnabled = true;
            else if (command == "don't()") funcEnabled = false;
            else if (funcEnabled)
            {
                string[] values = command.Substring(4, match.Length - 5).Split(',');
                int left = int.Parse(values[0]);
                int right = int.Parse(values[1]);

                int addition = left * right;
                total += addition;
            }

        }
        Console.WriteLine(total);
    }

    private static void Part1()
    {
        StreamReader sr = DayReader.GetInputReader();

        string data;
        using (sr)
        {
            data = sr.ReadToEnd();
        }

        var matches = Regex.EnumerateMatches(data, "mul\\((\\d+,\\d+)\\)");

        int total = 0;

        foreach (ValueMatch match in matches)
        {
            string command = data.Substring(match.Index, match.Length);
            string[] values = command.Substring(4, match.Length - 5).Split(',');
            int left = int.Parse(values[0]);
            int right = int.Parse(values[1]);

            int addition = left * right;
            total += addition;
        }

        Console.WriteLine(total);
    }
}