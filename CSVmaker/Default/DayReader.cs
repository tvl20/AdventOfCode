public static class DayReader
{
    public static StreamReader GetInputReader()
    {
        string inputPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/Default/input.txt";
        return new StreamReader(inputPath);
    }

    public static StreamWriter GetOutputWriter()
    {
        string outputPath = $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent}/Default/output.txt";
        return new StreamWriter(outputPath);
    }
}
