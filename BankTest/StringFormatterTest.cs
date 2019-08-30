using System;
using System.Collections.Generic;
using Bank;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;

namespace Tests
{
    public class StringFormatterTest
    {
        [Test]
        public void header_should_be_present_and_have_the_right_format()
        {
            List<HistoryLine> emptyHistory = new List<HistoryLine>();
            string header = new StringFormatter().Format(emptyHistory).Split(Environment.NewLine)[0];
            header.Should().Be("date || credit || debit || balance");
        }

        [Test]
        public void deposit_amount_should_be_in_the_right_formatn()
        {
            List<HistoryLine> historyLines = new List<HistoryLine>
            {
                new HistoryLine(new DateTime(2012,1,10),1000, null, new Balance(1000) )
            };
            string[] message = new StringFormatter().Format(historyLines).Split(Environment.NewLine);
            string firstHistoryFormattedLine = message[1];
            string[] splittedHistoryline = firstHistoryFormattedLine.Split("||");
            char space = ' ';
            splittedHistoryline[0].Should().Be("10/01/2012" + space);
            splittedHistoryline[1].Should().Be(space + "1000.00" + space);
            splittedHistoryline[2].Should().Be(space.ToString());
            splittedHistoryline[3].Should().Be(space + "1000.00");
            firstHistoryFormattedLine.Should().Be("10/01/2012 || 1000.00 || || 1000.00");
        }

        [Ignore("Pour plus tard")]
        [Test]
        public void should_contain_all_line_of_history()
        {
            Id anyAccountId = null;
            List<HistoryLine> historyLines = new List<HistoryLine>
            {
                new HistoryLine(new DateTime(2012,10,2), 500, anyAccountId,new Balance(500) )
            };
            int numberOfLineInMessage = new StringFormatter().Format(historyLines).Split(Environment.NewLine).Length;
            numberOfLineInMessage.Should().Be(historyLines.Count + 1);
        }
    }
}