using System.Collections.Generic;
using Bank;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class StringFormatterTest
    {
        [Test]
        public void header_should_be_present_and_have_the_right_format()
        {
            List<HistoryLine> emptyHistory = new List<HistoryLine>();
            string header = new StringFormatter().Format(emptyHistory);
            header.Should().Be("date || credit || debit || balance");
        }
    }
}