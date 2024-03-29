﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Bank
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
            historyLines.Add(new HistoryLine(today, amountOfMoney, accountId, balance));
        }

        public List<HistoryLine> Get(Id accountId)
        {
            return historyLines.FindAll( historyline => historyline.OwnedByAccountId(accountId));
        }

        public Balance GetBalance(Id accountId)
        {
            if (!historyLines.Any())
            {
                return new Balance(0);
            }
            return historyLines.OrderByDescending(x => x.Date)
                                .First().Balance;
        }
    }
}