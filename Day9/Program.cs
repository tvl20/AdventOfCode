
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
        //Part1();
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
        Console.WriteLine("DiskMap: " + ListToString(diskMap));
        Console.WriteLine("Generate Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        List<DiskNode> compressedDiskMap = CompressDiskMapPart2(diskMap);
        stopwatch.Stop();
        Console.WriteLine("Compressed: " + ListToString(compressedDiskMap));
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
        Console.WriteLine("DiskMap: " + ListToString(diskMap));
        Console.WriteLine("Generate Diskmap: " + stopwatch.ElapsedMilliseconds + "ms");

        stopwatch.Reset();
        stopwatch.Start();
        List<long> compressedDiskMap = CompressDiskMapPart1(diskMap);
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

    // something goes fucky in that it only does 1 iteration??
    private static List<DiskNode> CompressDiskMapPart2(List<DiskNode> diskmap)
    {
        LinkedList<DiskNode> list = new LinkedList<DiskNode>(diskmap.ToList());

        for (int i = list.Count - 1; i >= 0; i--)
        {
            LinkedListNode<DiskNode> lastNode = list.Last; // get lastNode node
            while (lastNode.Value.ID == -1) // trim all empty from the end
            {
                list.RemoveLast();
                lastNode = list.Last;
                i--; // move indexer down to adjust for removing item
            }

            // check if it can be placed anywhere
            LinkedListNode<DiskNode> checkNode = list.First;
            bool valueUpdated = false;
            while (checkNode != null && !checkNode.Equals(lastNode) && !valueUpdated) // move up until you're at the pos you're checking
            {
                // free space found, that is bigger or equal size
                if (checkNode.Value.ID == -1 && checkNode.Value.Size >= lastNode.Value.Size)
                {
                    list.Remove(lastNode);
                    list.AddBefore(checkNode, lastNode);

                    int restSize = checkNode.Value.Size - lastNode.Value.Size;
                    if (restSize > 0)
                    {
                        checkNode.Value.ResizeTo(restSize);
                    }
                    else
                    {
                        list.Remove(checkNode);
                    }

                    valueUpdated = true;
                }

                checkNode = checkNode.Next;
            }
        }

        return list.ToList();
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