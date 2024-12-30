using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleSpinner
{
    int counter = 0;
    bool started = false;

    public void Start()
    {
        if (started) Stop();

        counter = 0;
        Console.Write("/");
    }

    public void Stop()
    {
        if (!started) return;

        counter = 0;
        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
        Console.Write(" ");
        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
    }

    public void Turn()
    {
        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);

        counter++;
        switch (counter % 4)
        {
            case 0: Console.Write("/"); break;
            case 1: Console.Write("-"); break;
            case 2: Console.Write("\\"); break;
            case 3: Console.Write("|"); break;
        }
    }
}

