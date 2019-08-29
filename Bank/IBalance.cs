namespace Bank
{
    public interface IBalance
    {
        Balance Calculate(decimal amountOfMoney);
    }
}