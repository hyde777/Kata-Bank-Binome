using System;
using Bank;
using FluentAssertions.Common;
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
            IHistory dummyHistory = new Mock<IHistory>().Object;
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, accountManagerMock.Object, dummyHistory);
            
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
            Atm atm = new Atm(null, null, cardReaderMock.Object, accountManagerMock.Object, null);
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(clientId);

            atm.Withdraw(amountOfMoney);
            
            accountManagerMock.Verify(accountManger => accountManger.CalculateBalance(decimal.Negate(amountOfMoney), clientId));
        }

        [Test]
        public void METHOD()
        {
            DateTime today = new DateTime(2012, 1, 10);
            Id accountId = new Id("toto");
            decimal amountOfMoney = new decimal(1000);
            Mock<IAccountManager> accountManagerMock = new Mock<IAccountManager>();
            Mock<ICardReader> cardReaderMock = new Mock<ICardReader>();
            Mock<IAtmClock> clockMock = new Mock<IAtmClock>();
            decimal balance = 0 + amountOfMoney;
            Mock<IHistory> historyMock = new Mock<IHistory>();
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, accountManagerMock.Object, historyMock.Object);
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(accountId);
            accountManagerMock.Setup(accountManager => accountManager.CalculateBalance(amountOfMoney, accountId))
                .Returns(balance);
            clockMock.Setup(clock => clock.Today())
                .Returns(today);
            
            atm.Deposit(amountOfMoney);

            historyMock.Verify(history => history.AddLine(amountOfMoney, accountId, balance, today));
        }
    }
}