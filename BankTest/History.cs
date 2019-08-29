using System;
using System.Collections.Generic;
using Bank;

namespace Tests
{
    public class History : IHistory
    {
        public void AddLine(decimal amountOfMoney, Id accountId, Balance balance, in DateTime today)
        {
            throw new NotImplementedException();
        }

        public List<HistoryLine> Get(Id accountId)
        {
            return new List<HistoryLine>
            {
                new HistoryLine(1000)
            };
        }

        public IBalance GetBalance(Id accountId)
        {
            throw new NotImplementedException();
        }
    }
}