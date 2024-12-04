
internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
    }

    private static void Part1()
    {
        StreamReader sr = DayReader.GetInputReader();

        List<int> left = new();
        List<int> right = new();

        using (sr)
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] input = line.Split("   ");
                left.Add(int.Parse(input[0]));
                right.Add(int.Parse(input[1]));
                Console.WriteLine(line);
            }
        }

        Console.WriteLine(left.Count + " - " + right.Count);

        left.Sort();
        right.Sort();
        int dist = 0;

        for (int i = 0; i < left.Count; i++)
        {
            int diff = Math.Abs(left[i] - right[i]);
            Console.WriteLine(left[i] + " - " + right[i] + ">" + diff);
            dist += diff;
        }

        Console.WriteLine(dist);
    }
}