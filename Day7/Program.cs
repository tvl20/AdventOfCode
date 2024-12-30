
using Day7;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        Part1();
    }

    private static void Part1()
    {
        long total = 0;
        List<Equation> equations = LoadEquations();

        foreach (var equation in equations)
        {
            if (IsSolvable(equation)) 
                total += equation.Result;
        }

        Console.WriteLine("total value " + total);
    }

    private static bool IsSolvable(Equation equation, int index = 0, long currentTotal = 0)
    {
        if (equation == null) return false;
        if (index >= equation.Inputs.Count) return false;

        long sum = currentTotal + equation.Inputs[index];
        long mul = index == 0 ? equation.Inputs[index] : currentTotal * equation.Inputs[index];
        long concat = long.Parse(currentTotal.ToString() + equation.Inputs[index].ToString());

        if (index == equation.Inputs.Count - 1)
        {
            bool sumSolve = sum == equation.Result;
            bool mulSolve = mul == equation.Result;
            bool concatSolve = concat == equation.Result;

            return sumSolve || mulSolve || concatSolve;
        }

        index++;
        return
            IsSolvable(equation, index, sum) ||
            IsSolvable(equation, index, mul) ||
            IsSolvable(equation, index, concat);
    }


    public static List<Equation> LoadEquations()
    {
        List<Equation> equations = new List<Equation>();

        using (StreamReader sr = DayReader.GetInputReader())
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                Equation equation = new Equation();

                string[] sides = line.Split(':');
                equation.Result = long.Parse(sides[0]);

                string[] formula = sides[1].TrimStart().Split(' ');
                foreach (string number in formula)
                {
                    equation.Inputs.Add(long.Parse(number));
                }

                equations.Add(equation);
            }
        }

        return equations;
    }
}