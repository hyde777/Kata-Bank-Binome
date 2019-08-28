namespace Bank
{
    public interface IAccountManager
    {
        decimal CalculateBalance(decimal amountOfMoney, Id clientId);
    }
}