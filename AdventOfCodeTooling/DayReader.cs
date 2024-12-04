using System.Runtime.CompilerServices;

namespace AdventOfCodeTooling
{
    public static class DayReader
    {
        public static StreamReader GetInputReader()
        {
            string inputPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/input.txt";
            return new StreamReader(inputPath);
        }
    }
}
