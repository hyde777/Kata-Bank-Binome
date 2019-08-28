using System;
using Bank;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class AtmTest
    {
        [Test]
        public void verify_that_amount_of_money_is_deposited_to_account()
        {
            Mock<IAccountManager> accountManagerMock = new Mock<IAccountManager>();
            Mock<ICardReader> cardReaderMock = new Mock<ICardReader>();
            decimal amountOfMoney = new decimal(1000);
            Id clientId = new Id("toto");
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(clientId);
            Atm atm = new Atm(null, null, cardReaderMock.Object, accountManagerMock.Object);
            
            atm.Deposit(amountOfMoney);
            
            accountManagerMock.Verify(accountManager => accountManager.CalculateBalance(amountOfMoney, clientId));
        }

        [Test]
        public void verify_that_amount_of_money_is_withdraw_to_account()
        {
            Id clientId = new Id("toto");
            decimal amountOfMoney = new decimal(500);
            Mock<IAccountManager> accountManagerMock = new Mock<IAccountManager>();
            Mock<ICardReader> cardReaderMock = new Mock<ICardReader>();
            Atm atm = new Atm(null, null, cardReaderMock.Object, accountManagerMock.Object);
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(clientId);

            atm.Withdraw(amountOfMoney);
            
            accountManagerMock.Verify(accountManger => accountManger.CalculateBalance(decimal.Negate(amountOfMoney), clientId));
        }
    }
    
}