﻿using System;
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
        private Mock<IBalance> balanceMock;
        private Mock<ICardReader> cardReaderMock;
        private Mock<IAtmClock> clockMock;
        private Mock<IHistory> historyMock;
        private Mock<IStringFormatter> formatterMock;
        private Mock<IPrinter> printerMock;

        [SetUp]
        public void setup()
        {
            balanceMock = new Mock<IBalance>();
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
            Balance initialBalance = new Balance(0);
            historyMock.Setup(history => history.GetBalance(clientId))
                .Returns(balanceMock.Object);
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, historyMock.Object, null);
            
            atm.Deposit(amountOfMoney);
            
            balanceMock.Verify(balanceManager => balanceManager.Calculate(amountOfMoney));
        }

        [Test]
        public void verify_that_amount_of_money_is_withdraw_to_account()
        {
            Id clientId = new Id("toto");
            decimal amountOfMoney = new decimal(500);
            Balance initialBalance = new Balance(3000);
            historyMock.Setup(history => history.GetBalance(clientId))
                .Returns(balanceMock.Object);
            IAtmClock dummyClock = new Mock<IAtmClock>().Object;
            Atm atm = new Atm(null, dummyClock, cardReaderMock.Object, historyMock.Object, null);
            SetupCardReader(clientId);

            atm.Withdraw(amountOfMoney);
            balanceMock.Verify(balanceManager =>balanceManager.Calculate(decimal.Negate(amountOfMoney)));
        }

        [Test]
        public void verify_that_deposit_adds_one_line_of_history()
        {
            DateTime today = new DateTime(2012, 1, 10);
            Id clientId = new Id("toto");
            decimal amountOfMoney = new decimal(1000);
            decimal initialBalance = 0;
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, historyMock.Object, null);
            SetupCardReader(clientId);
            Balance initialBalanceFromHistory = new Balance(initialBalance);
            historyMock.Setup(history => history.GetBalance(clientId))
                .Returns(balanceMock.Object);
            
            Balance newBalance = new Balance(initialBalance + amountOfMoney);
            balanceMock.Setup(balanceManager => balanceManager.Calculate(amountOfMoney))
                .Returns(newBalance);
            clockMock.Setup(clock => clock.Today())
                .Returns(today);
            
            atm.Deposit(amountOfMoney);

            historyMock.Verify(history => history.AddLine(amountOfMoney, clientId, newBalance, today));
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
            Atm atm = new Atm(printerMock.Object, null, cardReaderMock.Object, historyMock.Object, formatterMock.Object);
            
            atm.Print();
            
            printerMock.Verify(printer => printer.Print(formattedHistory));
        }

        [Test]
        public void verify_that_withdraw_adds_one_line_of_history()
        {
            Id clientId = new Id("toto");
            Atm atm = new Atm(null, clockMock.Object, cardReaderMock.Object, historyMock.Object, null);
            SetupCardReader(clientId);
            decimal amountOfMoney = new decimal(500);
            decimal initialBalance = 3000;
            SetupHistoryMock(clientId);
            Balance newBalance = SetupBalanceManagerMock(initialBalance, decimal.Negate(amountOfMoney));
            DateTime today = SetupClock();

            atm.Withdraw(amountOfMoney);

            historyMock.Verify(history => history.AddLine(decimal.Negate(amountOfMoney), clientId, newBalance, today));
        }

        private Balance SetupBalanceManagerMock(decimal initialBalance, decimal negateMoney)
        {
            Balance newBalance = new Balance(initialBalance + negateMoney);
            balanceMock.Setup(balanceManager =>
                    balanceManager.Calculate(negateMoney))
                .Returns(newBalance);
            return newBalance;
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

        private void SetupHistoryMock(Id clientId)
        {
            historyMock.Setup(history => history.GetBalance(clientId))
                .Returns(balanceMock.Object);
        }

        private DateTime SetupClock()
        {
            DateTime today = new DateTime(2012, 1, 14);
            clockMock.Setup(clock => clock.Today())
                .Returns(today);
            return today;
        }

        private void SetupCardReader(Id clientId)
        {
            cardReaderMock.Setup(reader => reader.Authenticate()).Returns(clientId);
        }
    }
}