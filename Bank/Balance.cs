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
            return new Balance(balance + amountOfMoney);
        }

        private bool Equals(Balance other)
        {
            return balance == other.balance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Balance) obj);
        }

        public override int GetHashCode()
        {
            return balance.GetHashCode();
        }
    }
}