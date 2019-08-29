using System;
using System.Text;
using Bank;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class BankAcceptanceTest
    {
        [Test]
        public void Verify_that_atm_print_the_right_message()
        {
            Mock<IPrinter> printerMock = new Mock<IPrinter>();
            Mock<IAtmClock> clockMock = new Mock<IAtmClock>();
            clockMock.SetupSequence(clock => clock.Today())
                .Returns(new DateTime(2012, 01, 10))
                .Returns(new DateTime(2012, 01, 13))
                .Returns(new DateTime(2012, 01, 14));

            ICardReader dummyCardReader = new Mock<ICardReader>().Object;
            IBalanceManager dummyBalanceManager = new Mock<IBalanceManager>().Object;
            IHistory dummyHistory = new Mock<IHistory>().Object;
            IStringFormatter dummyStringFormatter = new Mock<IStringFormatter>().Object;
            Atm atm = new Atm(printerMock.Object, clockMock.Object, dummyCardReader, dummyBalanceManager, dummyHistory, dummyStringFormatter);
            atm.Deposit(new decimal(1000));
            atm.Deposit(new decimal(2000));
            atm.Withdraw(new decimal(500));

            atm.Print();

            StringBuilder message = new StringBuilder();
            message.AppendLine("date || credit || debit || balance")
                .AppendLine("14/01/2012 || || 500.00 || 2500.00")
                .AppendLine("13/01/2012 || 2000.00 || || 3000.00")
                .AppendLine("10/01/2012 || 1000.00 || || 1000.00");

            printerMock.Verify(printer => printer.Print(message.ToString()));
        }
    }
}