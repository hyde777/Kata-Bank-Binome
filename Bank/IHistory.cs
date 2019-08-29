using System;
using System.Collections.Generic;

namespace Bank
{
    public interface IHistory
    {
        void AddLine(decimal amountOfMoney, Id accountId, decimal balance, in DateTime today);
        List<HistoryLine> Get(Id accountId);
    }
}