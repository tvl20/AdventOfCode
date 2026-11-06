using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Day11
{
    public class BigSolver
    {
        public List<long> inputStones { get; private set; }
        private Dictionary<long, Dictionary<int, long>> sumTable;

        public BigSolver()
        {
            inputStones = Program.LoadStones();
            sumTable = new();
        }

        public long DetermineStoneAmount()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            long output = 0;
            foreach (long stone in inputStones)
            {
                stopwatch.Start();
                output += FillTable(ref sumTable, stone, 1, 75);
                stopwatch.Stop();
                Console.WriteLine("stone process time: " + stopwatch.ElapsedMilliseconds + "ms");
                stopwatch.Reset();
            }            

            return output;
        }

        private long FillTable(ref Dictionary<long, Dictionary<int, long>> lookupTable,
            long startStone, int currentDepth = 1, int maxDepth = 25)
        {
            List<long> currentLayerStones = Program.BlinkStone(startStone);
            long outputStones = 0;

            if (lookupTable.ContainsKey(startStone) && lookupTable[startStone].ContainsKey(currentDepth))
            {
                // we already did this
                return lookupTable[startStone][currentDepth];
            }

            if (currentDepth == maxDepth)
            {
                outputStones = currentLayerStones.Count;
            }
            else
            {
                int nextDepth = currentDepth + 1;
                outputStones = new();
                for (int i = 0; i < currentLayerStones.Count; i++) 
                {
                    outputStones += FillTable(ref lookupTable, currentLayerStones[i], nextDepth, maxDepth);
                }
            }

            if (!lookupTable.ContainsKey(startStone)) lookupTable[startStone] = new();
            lookupTable[startStone][currentDepth] = outputStones;

            return outputStones;
        }

        private static string ListToString<T>(List<T> list, string seperator = "")
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in list)
            {
                sb.Append(item.ToString() + seperator);
            }
            return sb.ToString();
        }
    }
}
