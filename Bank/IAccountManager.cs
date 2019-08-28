namespace Bank
{
    public interface IAccountManager
    {
        void Transfer(decimal amountOfMoney, Id clientId);
    }
}