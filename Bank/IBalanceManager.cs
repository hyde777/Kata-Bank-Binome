namespace Bank
{
    public interface IBalanceManager
    {
        Balance Calculate(decimal amountOfMoney, Balance balance);
        decimal Get(Id accountId);
    }
}