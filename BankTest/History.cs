using System;
using System.Collections.Generic;
using Bank;

namespace Tests
{
    public class History : IHistory
    {
        private readonly List<HistoryLine> historyLines;

        public History(List<HistoryLine> historyLines)
        {
            this.historyLines = historyLines;
        }

        public void AddLine(decimal amountOfMoney, Id accountId, Balance balance, in DateTime today)
        {
            throw new NotImplementedException();
        }

        public List<HistoryLine> Get(Id accountId)
        {
            return historyLines.FindAll( historyline => historyline.OwnedByAccountId(accountId));
        }

        public IBalance GetBalance(Id accountId)
        {
            return new Balance(0);
        }
    }
}