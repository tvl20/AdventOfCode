
internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
        Part2();
    }

    private static void Part1()
    {
        Dictionary<int, List<int>> orderRules;
        List<List<int>> pageList;

        using (StreamReader sr = DayReader.GetInputReader())
        {
            orderRules = LoadRules(sr);
            pageList = LoadLists(sr);
        }

        int output = 0;
        int numbCorrect = 0;
        foreach (List<int> list in pageList)
        {
            if (IsCorrectOrder(orderRules, list))
            {
                numbCorrect++;
                output += list[(int)Math.Floor(list.Count / 2d)];
            }
        }

        Console.WriteLine("Correct; " + numbCorrect + " value; " + output);
    }

    private static void Part2()
    {
        Dictionary<int, List<int>> orderRules;
        List<List<int>> pageList;

        using (StreamReader sr = DayReader.GetInputReader())
        {
            orderRules = LoadRules(sr);
            pageList = LoadLists(sr);
        }

        int output = 0;
        int numbErr = 0;
        foreach (List<int> list in pageList)
        {
            if (!IsCorrectOrder(orderRules, list))
            {
                numbErr++;
                List<int> fixedlist = FixOrdering(orderRules, list);
                output += fixedlist[(int)Math.Floor(fixedlist.Count / 2d)];
            }
        }

        Console.WriteLine("Corrected; " + numbErr + " value; " + output);
    }

    private static List<int> FixOrdering(Dictionary<int, List<int>> rules, List<int> list)
    {
        if (list.Count == 1) return list;
        List<int> toOrder = list.ToList();
        List<int> result = new List<int>();

        for (int i = 0; i < list.Count; i++)
        {
            for (int j = 0; j < toOrder.Count; j++)
            {
                List<int> mustBefore = rules.GetValueOrDefault(toOrder[j], new());
                IEnumerable<int> duplicates = mustBefore.Intersect(toOrder);

                if (!duplicates.Any())
                {
                    result.Add(toOrder[j]);
                    toOrder.RemoveAt(j);
                    break;
                }
            }
        }

        return result;
    }

    private static bool IsCorrectOrder(Dictionary<int, List<int>> rules, List<int> list)
    {
        if (list.Count == 1) return true;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            List<int> rest = list.GetRange(0, i);
            List<int> mustBefore = rules.GetValueOrDefault(list[i], new());
            IEnumerable<int> duplicates = mustBefore.Intersect(rest);

            if (duplicates.Any()) return false;
        }

        return true;
    }

    private static Dictionary<int, List<int>> LoadRules(StreamReader sr)
    {
        // list with all the items the key needs to be placed in front of
        Dictionary<int, List<int>> rules = new Dictionary<int, List<int>>();

        string line;

        while ((line = sr.ReadLine()) != null)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line)) break;
            string[] values = line.Split('|');
            int left = int.Parse(values[0]);
            int right = int.Parse(values[1]);

            if (rules.ContainsKey(left)) rules[left].Add(right);
            else rules.Add(left, new List<int>() { right });
        }

        return rules;
    }

    private static List<List<int>> LoadLists(StreamReader sr)
    {
        List<List<int>> lists = new List<List<int>>();

        string line;

        while ((line = sr.ReadLine()) != null)
        {
            string[] values = line.Split(',');
            List<int> input = new List<int>();
            foreach (string value in values)
            {
                input.Add(int.Parse(value));
            }
            lists.Add(input);
        }

        return lists;
    }
}