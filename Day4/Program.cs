

internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
        Part2();
    }


    private static void Part2()
    {
        List<List<char>> wordList = LoadInput();
        int foundWords = 0;

        for (int x = 0; x < wordList.Count; x++)
        {
            for (int y = 0; y < wordList[x].Count; y++)
            {
                if (wordList[x][y] == 'A')
                {
                    Console.WriteLine("Found 'A'");

                    if (HasXMASPattern(x, y, wordList))
                    {
                        foundWords++;
                    }
                }
            }
        }

        Console.WriteLine("Part2: " + foundWords);
    }

    private static bool HasXMASPattern(int x, int y, List<List<char>> wordList)
    {
        Console.WriteLine("x " + x + " count; " + wordList.Count);
        Console.WriteLine("y " + y + " count; " + wordList[x].Count);

        // check if there's a possible full word
        if (x - 1 < 0 || x + 1 >= wordList.Count ||
            y - 1 < 0 || y + 1 >= wordList[x].Count)
        {
            Console.WriteLine("No space for pattern");
            return false;
        }

        bool diagonal1 = false;
        bool diagonal2 = false;


        // diagonal 1
        if (wordList[x - 1][y - 1] == 'M' && wordList[x + 1][y + 1] == 'S') diagonal1 = true;
        if (wordList[x - 1][y - 1] == 'S' && wordList[x + 1][y + 1] == 'M') diagonal1 = true;

        // diagonal 2
        if (wordList[x - 1][y + 1] == 'M' && wordList[x + 1][y - 1] == 'S') diagonal2 = true;
        if (wordList[x - 1][y + 1] == 'S' && wordList[x + 1][y - 1] == 'M') diagonal2 = true;

        Console.WriteLine("Diagonal2: " + diagonal2);

        return diagonal1 && diagonal2;
    }

    // todo: fix orientation representation; x=y y=x but it somehow still worked
    private static void Part1()
    {
        List<List<char>> wordList = LoadInput();
        int foundWords = 0;

        for (int x = 0; x < wordList.Count; x++)
        {
            for (int y = 0; y < wordList[x].Count; y++)
            {
                if (wordList[x][y] == 'X')
                {
                    foundWords += FindXMAS(x, y, wordList);
                }
            }
        }

        Console.WriteLine("Part1: " + foundWords);
    }

    private static int FindXMAS(int x, int y, List<List<char>> wordList)
    {
        int returnCount = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int xRange = x + i * 3;
                int yRange = y + j * 3;

                // check if there's a possible full word
                if ((i == 0 && j == 0) ||
                    xRange < 0 || xRange >= wordList[x].Count ||
                    yRange < 0 || yRange >= wordList.Count)
                {
                    continue;
                }

                if (wordList[x + i][y + j] == 'M' &&
                    wordList[x + (i * 3)][y + (j * 3)] == 'S' &&
                    wordList[x + (i * 2)][y + (j * 2)] == 'A')
                {
                    returnCount++;
                }
            }
        }

        return returnCount;
    }

    private static List<List<char>> LoadInput()
    {
        List<List<char>> data = new();

        StreamReader sr = DayReader.GetInputReader();

        using (sr)
        {
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                data.Add(new List<char>(line.ToCharArray()));
            }
        }

        return data;
    }
}