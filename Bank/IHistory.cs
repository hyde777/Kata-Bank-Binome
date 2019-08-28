using System;

namespace Bank
{
    public interface IHistory
    {
        void AddLine(decimal amountOfMoney, Id accountId, decimal balance, in DateTime today);
    }
}