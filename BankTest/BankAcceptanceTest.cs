using System;
using System.Text;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class BankAcceptanceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Verify_that_atm_print_the_right_message()
        {
            Mock<IPrinter> printerMock = new Mock<IPrinter>();
            Atm atm = new Atm(printerMock.Object);
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

    public interface IPrinter
    {
        void Print(string message);
    }

    public class Atm
    {
        public Atm(IPrinter printer)
        {
        }

        public string Print()
        {
            return string.Empty;
        }

        public void Deposit(decimal p0)
        {
            
        }

        public void Withdraw(decimal @decimal)
        {
            
        }
    }
}