using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Bank
{
    public class StringFormatter : IStringFormatter
    {
        public string Format(List<HistoryLine> historyLines)
        {
            StringBuilder message = new StringBuilder()
                .AppendLine("date || credit || debit || balance");
            if(historyLines.Any())    
                message.AppendLine($"{historyLines[0].Date.ToString("dd/MM/yyyy")} || {string.Format(CultureInfo.InvariantCulture, "{0:0.00}",historyLines[0].Movement)} || || {historyLines[0].Balance}");
            return message.ToString();
        }
    }
}