using System;

namespace Bank
{
    public class Atm
    {
        private readonly ICardReader cardReader;
        private readonly IAccountManager accountManager;
        private readonly IAtmClock clock;

        public Atm(IPrinter printer, IAtmClock clock)
        {
            this.clock = clock;
        }

        public Atm(IPrinter printer, IAtmClock clock, ICardReader cardReader, IAccountManager accountManager)
            : this(printer, clock)
        {
            this.cardReader = cardReader;
            this.accountManager = accountManager;
        }

        public string Print()
        {
            return string.Empty;
        }

        public void Deposit(decimal amountOfMoney)
        {
            Id accountId = cardReader.Authenticate();
            accountManager.Transfer(amountOfMoney, accountId);
            //DateTime today = clock.Today();
            //history.AddLine(amountOfMoney, accountId, today);
        }

        public void Withdraw(decimal @decimal)
        {
            
        }
    }
}