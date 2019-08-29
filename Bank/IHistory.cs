using System;
using System.Collections.Generic;

namespace Bank
{
    public interface IHistory
    {
        void AddLine(decimal amountOfMoney, Id accountId, Balance balance, in DateTime today);
        List<HistoryLine> Get(Id accountId);
        IBalance GetBalance(Id accountId);
    }
}