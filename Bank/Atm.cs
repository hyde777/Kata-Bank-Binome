using System;
using System.Collections.Generic;

namespace Bank
{
    public class Atm
    {
        private readonly ICardReader cardReader;
        private readonly IBalance balance;
        private readonly IAtmClock clock;
        private readonly IPrinter printer;
        private readonly IHistory history;
        private readonly IStringFormatter stringFormatter;


        public Atm(IPrinter printer, IAtmClock clock, ICardReader cardReader, IBalance balance,
            IHistory history, IStringFormatter stringFormatter)
        {
            this.stringFormatter = stringFormatter;
            this.printer = printer;
            this.history = history;
            this.clock = clock;
            this.cardReader = cardReader;
            this.balance = balance;
        }

        public void Print()
        {
            Id accountId = cardReader.Authenticate();
            List<HistoryLine> historyOfAccount = history.Get(accountId);
            string formattedHistory = stringFormatter.Format(historyOfAccount);
            printer.Print(formattedHistory);
        }

        public void Deposit(decimal amountOfMoney)
        {
            ComputeBalanceAndHistorize(amountOfMoney);
        }

        public void Withdraw(decimal amountOfMoney)
        {
            decimal negativeAmountOfMoney = decimal.Negate(amountOfMoney);
            ComputeBalanceAndHistorize(negativeAmountOfMoney);
        }

        private void ComputeBalanceAndHistorize(decimal amountOfMoney)
        {
            Id accountId = cardReader.Authenticate();
            IBalance oldBalance = history.GetBalance(accountId);
            Balance newBalance = oldBalance.Calculate(amountOfMoney);
            DateTime today = clock.Today();
            history.AddLine(amountOfMoney, accountId, newBalance, today);
        }
    }
}