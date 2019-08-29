namespace Bank
{
    public interface IAccountManager
    {
        void UpdateBalance(decimal amountOfMoney, Id clientId);
        decimal GetBalance(Id accountId);
    }
}