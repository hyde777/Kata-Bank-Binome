using System.Collections.Generic;

namespace Bank
{
    public class StringFormatter : IStringFormatter
    {
        public string Format(List<HistoryLine> historyLines)
        {
            return "date || credit || debit || balance";
        }
    }
}