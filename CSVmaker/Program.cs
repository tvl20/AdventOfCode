using CSVmaker;
using CSVmaker.Default;

internal class Program
{
    private static void Main(string[] args)
    {
        ConsoleSpinner spinner = new ConsoleSpinner();
        StreamReader sr = DayReader.GetInputReader();
        StreamWriter sw = DayReader.GetOutputWriter();

        string header = "TransactionDate,ProcessedDate,Description,Company,Country,AmountForeign,AmountEuro,TransactionType,ConversionRate";
        sw.WriteLine(header);

        Console.Write("Processing transactions... |");
        while (!sr.EndOfStream)
        {
            Transaction transaction = GetTransaction(sr);
            sw.WriteLine(transaction.toCSV());

            spinner.Turn();
        }

        sr.Close();
        sw.Close();
        Console.WriteLine();
        Console.WriteLine("\nDone processing transactions.");
    }

    private static Transaction GetTransaction(StreamReader sr)
    {
        Transaction transaction = new Transaction();
        transaction.TransactionDate = sr.ReadLine();
        transaction.ProcessedDate = sr.ReadLine();
        transaction.Description = sr.ReadLine();

        // check if the transaction is a cash withdrawal - if so exit early
        if (transaction.Description == "KOSTEN KASOPNAME")
        {
            transaction.AmountEuro = sr.ReadLine();
            transaction.TransactionType = sr.ReadLine();
            return transaction;
        }

        transaction.Company = sr.ReadLine();
        transaction.Country = sr.ReadLine();

        // check if the amount is in foreign currency or euro
        string amount = sr.ReadLine();
        if (amount[amount.Length - 3] != ',')
        {
            transaction.AmountForeign = amount;
            transaction.AmountEuro = sr.ReadLine();
        }
        else
        {
            transaction.AmountEuro = amount;
        }

        transaction.TransactionType = sr.ReadLine();

        // check if the conversion rate is present (wisselkoers)
        if (sr.Peek() != -1)
        {
            char nextChar = Convert.ToChar(sr.Peek());
            if (nextChar == 'W' || nextChar == 'w')
            {
                string valuta = sr.ReadLine().Split(' ')[1];
                transaction.ConversionRate = sr.ReadLine() + " " + valuta;
            }
        }

        return transaction;
    }
}