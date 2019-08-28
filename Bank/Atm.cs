using System;

namespace Bank
{
    public class Atm
    {
        private readonly ICardReader cardReader;
        private readonly IAccountManager accountManager;
        private readonly IAtmClock clock;

     
        public Atm(IPrinter printer, IAtmClock clock, ICardReader cardReader, IAccountManager accountManager)
        {
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
            accountManager.CalculateBalance(amountOfMoney, accountId);
            //DateTime today = clock.Today();
            //history.AddLine(amountOfMoney, accountId, today);
        }

        public void Withdraw(decimal amountOfMoney)
        {
            Id accountId = cardReader.Authenticate();
            accountManager.CalculateBalance(decimal.Negate(amountOfMoney), accountId);
        }
    }
}