using System;
using System.Collections.Generic;

namespace Bank
{
    public class Atm
    {
        private readonly ICardReader cardReader;
        private readonly IAccountManager accountManager;
        private readonly IAtmClock clock;
        private readonly IPrinter printer;
        private readonly IHistory history;
        private IStringFormatter stringFormatter;


        public Atm(IPrinter printer, IAtmClock clock, ICardReader cardReader, IAccountManager accountManager,
            IHistory history, IStringFormatter stringFormatter)
        {
            this.stringFormatter = stringFormatter;
            this.printer = printer;
            this.history = history;
            this.clock = clock;
            this.cardReader = cardReader;
            this.accountManager = accountManager;
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
            accountManager.UpdateBalance(amountOfMoney, accountId);
            decimal balance1 = accountManager.GetBalance(accountId);
            DateTime today = clock.Today();
            history.AddLine(amountOfMoney, accountId, balance1, today);
        }
    }
}