public static class DayReader
{
    public static StreamReader GetInputReader()
    {
        string inputPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/Default/input.txt";
        return new StreamReader(inputPath);
    }
}
