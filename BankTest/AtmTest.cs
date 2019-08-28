using Bank;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class AtmTest
    {
        [Test]
        public void test()
        {
            Mock<IAccountManager> accountManagerMock = new Mock<IAccountManager>();
            Mock<ICardReader> cardReaderMock = new Mock<ICardReader>();
            decimal amountOfMoney = new decimal(1000);
            Id clientId = new Id("toto");
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(clientId);
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            IPrinter dummyPrinter = new Mock<IPrinter>().Object;
            Atm atm = new Atm(dummyPrinter, dummyClock, cardReaderMock.Object, accountManagerMock.Object);
            
            atm.Deposit(amountOfMoney);
            
            accountManagerMock.Verify(accountManager => accountManager.Transfer(amountOfMoney, clientId));
        }
    }
}