using System;

namespace Bank
{
    public class Atm
    {
        private readonly ICardReader cardReader;
        private readonly IAccountManager accountManager;
        private readonly IAtmClock clock;
        private readonly IHistory history;


        public Atm(IPrinter printer, IAtmClock clock, ICardReader cardReader, IAccountManager accountManager,
            IHistory history)
        {
            this.history = history;
            this.clock = clock;
            this.cardReader = cardReader;
            this.accountManager = accountManager;
        }

        public void Print()
        {
        }

        public void Deposit(decimal amountOfMoney)
        {
            Id accountId = cardReader.Authenticate();
            decimal balance = accountManager.CalculateBalance(amountOfMoney, accountId);
            DateTime today = clock.Today();
            history.AddLine(amountOfMoney, accountId, balance ,today);
        }

        public void Withdraw(decimal amountOfMoney)
        {
            Id accountId = cardReader.Authenticate();
            accountManager.CalculateBalance(decimal.Negate(amountOfMoney), accountId);
        }
    }
}