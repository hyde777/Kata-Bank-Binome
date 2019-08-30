using System.Collections.Generic;
using Bank;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class HistoryTest
    {
        [Test]
        public void should_have_one_history_line_with_credit_1000()
        {
            List<HistoryLine> expectation = new List<HistoryLine>
            {
                new HistoryLine(1000, null)
            };
            IHistory history = new History(expectation);
            List<HistoryLine> historyLines = history.Get(null);
            historyLines.Should().Equal(expectation);
        }

        [Test]
        public void should_have_one_history_line_with_one_credit_2000()
        {
            List<HistoryLine> expectation = new List<HistoryLine>
            {
                new HistoryLine(2000, null)
            };
            IHistory history = new History(expectation);
            List<HistoryLine> historyLines = history.Get(null);
            historyLines.Should().Equal(expectation);
        }

        [Test]
        public void should_return_lines_with_the_accountid()
        {
            HistoryLine josey = new HistoryLine(2000, new Id("josey"));
            List<HistoryLine> expectedHistorylines = new List<HistoryLine>
            {
                new HistoryLine(1000, new Id("marie")),
                josey
            };
            IHistory history = new History(expectedHistorylines);
            List<HistoryLine> historyLine = history.Get(new Id("josey"));
            historyLine.Should().Equal(new List<HistoryLine>
            {
                josey
            });
        }
    }
}