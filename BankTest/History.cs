using System;
using System.Collections.Generic;
using System.Linq;
using Bank;
using Castle.Core.Internal;

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

        public Balance GetBalance(Id accountId)
        {
            if (historyLines.IsNullOrEmpty())
            {
                return new Balance(0);
            }
            return historyLines.OrderByDescending(x => x.Date)
                                .First().Balance;
        }
    }
}