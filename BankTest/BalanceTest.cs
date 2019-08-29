using Bank;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class BalanceTest
    {
        [Test]
        public void METHOD()
        {
            decimal amount = new decimal(500);
            decimal initialValue = 0;
            Balance oldBalance = new Balance(initialValue);
            Balance balance = oldBalance.Calculate(amount);
            balance.Should().Be(new Balance(initialValue + amount));
        }
    }
}