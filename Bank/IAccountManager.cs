namespace Bank
{
    public interface IAccountManager
    {
        void CalculateBalance(decimal amountOfMoney, Id clientId);
    }
}