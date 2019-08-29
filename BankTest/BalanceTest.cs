using Bank;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class BalanceTest
    {
        [Test]
        public void balance_of_zero_plus_amount_of_500_equal_balance_of_500()
        {
            decimal amount = new decimal(500);
            decimal initialValue = 0;
            Balance oldBalance = new Balance(initialValue);
            Balance balance = oldBalance.Calculate(amount);
            balance.Should().Be(new Balance(initialValue + amount));
        }

        [Test]
        public void balance_of_zero_plus_amount_of_1000_equal_balance_of_1000()
        {
            decimal amount = new decimal(1000);
            decimal initialValue = 0;
            Balance oldBalance = new Balance(initialValue);
            Balance balance = oldBalance.Calculate(amount);
            balance.Should().Be(new Balance(initialValue + amount));
        }

        [Test]
        public void balance_of_300_plus_amount_of_1000_equal_balance_of_1000()
        {
            decimal amount = new decimal(1000);
            decimal initialValue = 300;
            Balance oldBalance = new Balance(initialValue);
            Balance balance = oldBalance.Calculate(amount);
            balance.Should().Be(new Balance(initialValue + amount));
        }
    }
}