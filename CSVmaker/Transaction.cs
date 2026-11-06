using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVmaker
{
    public class Transaction
    {
        public string TransactionDate { get; set; }
        public string ProcessedDate { get; set; }

        public string Description { get; set; }
        public string Company { get; set; } = "N/A";
        public string Country { get; set; } = "N/A";

        public string AmountForeign { get; set; } = "N/A";
        public string AmountEuro { get; set; }

        public string TransactionType { get; set; }

        public string ConversionRate { get; set; } = "N/A";

        public string toCSV()
        {
            return $"\"{TransactionDate}\",\"{ProcessedDate}\",\"{Description}\",\"{Company}\",\"{Country}\",\"{AmountForeign}\",\"{AmountEuro}\",\"{TransactionType}\",\"{ConversionRate}\"";
        }
    }
}
