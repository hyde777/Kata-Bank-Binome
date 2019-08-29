using System;
using System.Collections.Generic;
using System.Text;
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
        private Mock<IStringFormatter> formatterMock;
        private Mock<IPrinter> printerMock;

        [SetUp]
        public void setup()
        {
            accountManagerMock = new Mock<IAccountManager>();
            cardReaderMock = new Mock<ICardReader>();
            clockMock = new Mock<IAtmClock>();
            historyMock = new Mock<IHistory>();
            formatterMock = new Mock<IStringFormatter>();
            printerMock = new Mock<IPrinter>();
        }

        [Test]
        public void verify_that_amount_of_money_is_deposited_to_account()
        {
            decimal amountOfMoney = new decimal(1000);
            Id clientId = new Id("toto");
            SetupCardReader(clientId);
            IHistory dummyHistory = new Mock<IHistory>().Object;
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, accountManagerMock.Object, dummyHistory, null);
            
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
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, accountManagerMock.Object, dummyHistory, null);
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
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, accountManagerMock.Object, historyMock.Object, null);
            SetupCardReader(clientId);
            accountManagerMock.Setup(accountManager => accountManager.GetBalance(clientId))
                .Returns(balance);
            clockMock.Setup(clock => clock.Today())
                .Returns(today);
            
            atm.Deposit(amountOfMoney);

            historyMock.Verify(history => history.AddLine(amountOfMoney, clientId, balance, today));
        }

        [Test]
        public void verify_that_print_call_the_printer()
        {
            List<HistoryLine> SetupHistoryMock(Id id)
            {
                List<HistoryLine> list = new List<HistoryLine>();
                historyMock.Setup(history => history.Get(id))
                    .Returns(list);
                return list;
            }

            Id accountId = new Id("toto");
            SetupCardReader(accountId);
            List<HistoryLine> historyLines = SetupHistoryMock(accountId);
            string formattedHistory = SetupStringFormatMock(historyLines);
            Atm atm = new Atm(printerMock.Object, null, cardReaderMock.Object, 
                null, historyMock.Object, formatterMock.Object);
            
            atm.Print();
            
            printerMock.Verify(printer => printer.Print(formattedHistory));
        }

        private string SetupStringFormatMock(List<HistoryLine> historyLines)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("date || credit || debit || balance")
                .AppendLine("14/01/2012 || || 500.00 || 2500.00")
                .AppendLine("13/01/2012 || 2000.00 || || 3000.00")
                .AppendLine("10/01/2012 || 1000.00 || || 1000.00");
            string s = stringBuilder.ToString();
            formatterMock.Setup(formatter => formatter.Format(historyLines))
                .Returns(s);
            return s;
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
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, accountManagerMock.Object, historyMock.Object, null);
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