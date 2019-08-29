using System.Collections.Generic;

namespace Bank
{
    public interface IStringFormatter
    {
        string Format(List<HistoryLine> historyLines);
    }
}