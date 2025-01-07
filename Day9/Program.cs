
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
        List<long> diskMap = DiskMapFromInput(input);
        stopwatch.Stop();
        Console.WriteLine("DiskMap: " + ListToString(diskMap));
        Console.WriteLine("Generate Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        List<long> compressedDiskMap = CompressDiskMap(diskMap);
        stopwatch.Stop();
        Console.WriteLine("Compressed: " + ListToString(compressedDiskMap));
        Console.WriteLine("Compress Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        long checksum = GenerateDiskChecksum(compressedDiskMap);
        stopwatch.Stop();
        Console.WriteLine("Checksum: " + checksum.ToString());
        Console.WriteLine("Generate Checksum: " + stopwatch.ElapsedMilliseconds + "ms");
    }

    private static long GenerateDiskChecksum(List<long> input)
    {
        long checksum = 0;

        for (int i = 0; i < input.Count; i++)
        {
            checksum += input[i] * i;
        }

        return checksum;
    }

    private static List<long> CompressDiskMap(List<long> diskmap)
    {
        LinkedList<long> list = new LinkedList<long>(diskmap);

        while (list.Contains(-1))
        {
            LinkedListNode<long> firstGap = list.Find(-1);

            LinkedListNode<long> lastData;
            do
            {
                lastData = list.Last;
                list.RemoveLast();
            } while (lastData?.Value == -1);

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

        return list.ToList();
    }

    private static List<long> DiskMapFromInput(string input)
    {
        List<long> diskmap = new();

        // Todo improve this, its shitty but should suffice for now
        long currentId = 0;
        bool isFreeSpace = false;
        foreach (char c in input)
        {
            int i = int.Parse(c.ToString());
            diskmap.AddRange(Enumerable.Repeat(isFreeSpace ? -1 : currentId, i));

            if (!isFreeSpace) currentId++;
            isFreeSpace = !isFreeSpace;
        }

        return diskmap;
    }

    private static string ListToString<T>(List<T> list)
    {
        StringBuilder sb = new StringBuilder();
        foreach (T item in list)
        {
            sb.Append(item.ToString());
        }
        return sb.ToString();
    }
}