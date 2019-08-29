namespace Bank
{
    public class Balance : IBalance
    {
        private readonly decimal balance;

        public Balance(decimal balance)
        {
            this.balance = balance;
        }

        public Balance Calculate(decimal amountOfMoney)
        {
            return null;
            //return new Balance(balance + amountOfMoney);
        }
    }
}