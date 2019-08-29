using System;
using Bank;
using FluentAssertions.Common;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class AtmTest
    {
        private Mock<IAccountManager> accountManagerMock;
        private Mock<ICardReader> cardReaderMock;
        private Mock<IAtmClock> clockMock;
        private Mock<IHistory> historyMock;

        [SetUp]
        public void setup()
        {
            accountManagerMock = new Mock<IAccountManager>();
            cardReaderMock = new Mock<ICardReader>();
            clockMock = new Mock<IAtmClock>();
            historyMock = new Mock<IHistory>();
        }

        [Test]
        public void verify_that_amount_of_money_is_deposited_to_account()
        {
            decimal amountOfMoney = new decimal(1000);
            Id clientId = new Id("toto");
            SetupCardReader(clientId);
            IHistory dummyHistory = new Mock<IHistory>().Object;
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, accountManagerMock.Object, dummyHistory);
            
            atm.Deposit(amountOfMoney);
            
            accountManagerMock.Verify(accountManager => accountManager.UpdateBalance(amountOfMoney, clientId));
        }

        [Test]
        public void verify_that_amount_of_money_is_withdraw_to_account()
        {
            Id clientId = new Id("toto");
            decimal amountOfMoney = new decimal(500);
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            IHistory dummyHistory = new Mock<IHistory>().Object;
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, accountManagerMock.Object, dummyHistory);
            SetupCardReader(clientId);

            atm.Withdraw(amountOfMoney);
            
            accountManagerMock.Verify(accountManager => accountManager.UpdateBalance(decimal.Negate(amountOfMoney), clientId));
        }

        [Test]
        public void verify_that_deposit_adds_one_line_of_history()
        {
            DateTime today = new DateTime(2012, 1, 10);
            Id clientId = new Id("toto");
            decimal amountOfMoney = new decimal(1000);
            decimal previousBalance = 0;
            decimal balance = previousBalance + amountOfMoney;
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, accountManagerMock.Object, historyMock.Object);
            SetupCardReader(clientId);
            accountManagerMock.Setup(accountManager => accountManager.GetBalance(clientId))
                .Returns(balance);
            clockMock.Setup(clock => clock.Today())
                .Returns(today);
            
            atm.Deposit(amountOfMoney);

            historyMock.Verify(history => history.AddLine(amountOfMoney, clientId, balance, today));
        }

        [Test]
        public void verify_that_withdraw_adds_one_line_of_history()
        {
            DateTime today = new DateTime(2012, 1, 14);
            Id clientId = new Id("toto");
            decimal amountOfMoney = new decimal(500);
            decimal previousBalance = 3000;
            decimal negateMoney = decimal.Negate(amountOfMoney);
            decimal balance = previousBalance + negateMoney;
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, accountManagerMock.Object, historyMock.Object);
            SetupCardReader(clientId);
            accountManagerMock.Setup(accountManager => accountManager.GetBalance(clientId))
                .Returns(balance);
            clockMock.Setup(clock => clock.Today())
                .Returns(today);
            
            atm.Withdraw(amountOfMoney);

            historyMock.Verify(history => history.AddLine(negateMoney, clientId, balance, today));
        }
        
        private void SetupCardReader(Id clientId)
        {
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(clientId);
        }
    }
}