
using Day9;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        // The example works but not the actual input??
        Console.WriteLine("---- part 1 ----");
        Part1();

        Console.WriteLine("---- part 2 ----");
        Part2();
    }

    private static void Part2()
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
        List<DiskNode> diskMap = DiskMapFromInputPart2(input);
        stopwatch.Stop();
        // Console.WriteLine("DiskMap: " + ListToString(diskMap));
        Console.WriteLine("Generate Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        List<DiskNode> compressedDiskMap = CompressDiskMapPart2(diskMap);
        stopwatch.Stop();
        // Console.WriteLine("Compressed: " + ListToString(compressedDiskMap));
        Console.WriteLine("Compress Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        long checksum = GenerateDiskChecksum(DiskNodesToLongs(compressedDiskMap));
        stopwatch.Stop();
        Console.WriteLine("Checksum: " + checksum.ToString());
        Console.WriteLine("Generate Checksum: " + stopwatch.ElapsedMilliseconds + "ms");
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
        List<long> diskMap = DiskMapFromInputPart1(input);
        stopwatch.Stop();
        // Console.WriteLine("DiskMap: " + ListToString(diskMap));
        Console.WriteLine("Generate Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        List<long> compressedDiskMap = CompressDiskMapPart1(diskMap);
        stopwatch.Stop();
        // Console.WriteLine("Compressed: " + ListToString(compressedDiskMap));
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
            if (input[i] > 0) checksum += input[i] * i;
        }

        return checksum;
    }

    // something goes fucky in that it only does 1 iteration??
    private static List<DiskNode> CompressDiskMapPart2(List<DiskNode> diskmap)
    {
        List<DiskNode> result = new List<DiskNode>(diskmap.ToList());

        for (int i = result.Count - 1; i >= 0; i--)
        {
            // get lastDataNode node
            DiskNode lastDataNode = result[i];
            if (lastDataNode.ID == -1) // trim all empty from the end
            {
                continue;
            }

            // check if it can be placed anywhere
            for (int j = 0; j < i; j++)
            {
                DiskNode checkNode = result[j];
                if (checkNode.ID == -1 && checkNode.Size >= lastDataNode.Size)
                {
                    // set input to empty space to keep original position
                    result[i] = new DiskNode(-1, lastDataNode.Size);

                    checkNode.ResizeTo(checkNode.Size - lastDataNode.Size); // resize free space
                    if (checkNode.Size <= 0) result.RemoveAt(j); // remove if filled up completely

                    result.Insert(j, lastDataNode); // move data into free space
                    break;
                }
            }

        }

        return result;
    }

    private static List<DiskNode> DiskMapFromInputPart2(string input)
    {
        List<DiskNode> diskmap = new();

        // Todo improve this, its shitty but should suffice for now
        long currentId = 0;
        bool isFreeSpace = false;
        foreach (char c in input)
        {
            int i = int.Parse(c.ToString());
            diskmap.Add(new DiskNode( isFreeSpace ? -1 : currentId, i));

            if (!isFreeSpace) currentId++;
            isFreeSpace = !isFreeSpace;
        }

        return diskmap;
    }

    private static List<long> CompressDiskMapPart1(List<long> diskmap)
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

    private static List<long> DiskMapFromInputPart1(string input)
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

    private static List<long> DiskNodesToLongs(List<DiskNode> nodes)
    {
        List<long> longs = new();
        foreach (DiskNode node in nodes)
        {
            longs.AddRange(Enumerable.Repeat(node.ID, node.Size));
        }
        return longs;
    }
}