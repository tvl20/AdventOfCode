
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        // The example works but not the actual input??
        Part1();
    }

    private static void Part1()
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.Start();
        string input = "";
        using (StreamReader reader = DayReader.GetInputReader())
        {
            input = reader.ReadToEnd();
        }
        stopwatch.Stop();
        Console.WriteLine("Read Input: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        string diskMap = DiskMapFromInput(input);
        stopwatch.Stop();
        //Console.WriteLine("DiskMap: " + diskMap);
        Console.WriteLine("Generate Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        string compressedDiskMap = CompressDiskMap(diskMap);
        stopwatch.Stop();
        //Console.WriteLine("Compressed: " + compressedDiskMap);
        Console.WriteLine("Compress Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        long checksum = GenerateDiskChecksum(compressedDiskMap);
        stopwatch.Stop();
        Console.WriteLine("Checksum: " + checksum.ToString());
        Console.WriteLine("Generate Checksum: " + stopwatch.ElapsedMilliseconds + "ms");
    }

    private static long GenerateDiskChecksum(string input)
    {
        long checksum = 0;

        for (int i = 0; i < input.Length; i++)
        {
            checksum += int.Parse(input[i].ToString()) * i;
        }

        return checksum;
    }

    private static string CompressDiskMap(string diskmap)
    {
        LinkedList<char> list = new LinkedList<char>(diskmap);

        while (list.Contains('.'))
        {
            LinkedListNode<char>? firstGap = list.Find('.');

            LinkedListNode<char>? lastData;
            do
            {
                lastData = list.Last;
                list.RemoveLast();
            } while (lastData?.Value == '.');

            if (firstGap.List != null)
            {
                list.AddBefore(firstGap, lastData);
                list.Remove(firstGap);
            }
            else
            {
                list.AddLast(lastData);
            }
        }

        return string.Join("", list);
    }

    private static string DiskMapFromInput(string input)
    {
        StringBuilder stringBuilder = new StringBuilder();

        // Todo improve this, its shitty but should suffice for now
        int currentId = 0;
        bool isFreeSpace = false;
        foreach (char c in input)
        {
            for (int i = 0; i < int.Parse(c.ToString()); i++)
            {
                stringBuilder.Append(isFreeSpace ? "." : currentId);
            }

            if (!isFreeSpace) currentId++;
            isFreeSpace = !isFreeSpace;
        }

        return stringBuilder.ToString();
    }
}