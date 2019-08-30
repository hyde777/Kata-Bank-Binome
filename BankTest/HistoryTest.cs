using System.Collections.Generic;
using Bank;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class HistoryTest
    {
       
        [Test]
        public void should_josey_history_lines_when_we_ask_it()
        {
            HistoryLine josey = new HistoryLine(2000, new Id("josey"), null);
            HistoryLine marie = new HistoryLine(1000, new Id("marie"),null);
            List<HistoryLine> expectedHistorylines = new List<HistoryLine>
            {
                marie,
                josey
            };
            IHistory history = new History(expectedHistorylines);
            List<HistoryLine> historyLine = history.Get(new Id("josey"));
            historyLine.Should().Equal(new List<HistoryLine>
            {
                josey
            });
        }

        [Test]
        public void should_give_initial_balance_if_no_history_lines()
        {
            IBalance balance = new History(new List<HistoryLine>()).GetBalance(null);
            balance.Should().Be(new Balance(0));
        }

        [Test]
        public void should_give_the_last_balance()
        {
            List<HistoryLine> historyLines = new List<HistoryLine>
            {
                new HistoryLine(0,null,new Balance(1000))
            };
            IHistory history = new History(historyLines);
            IBalance balance = history.GetBalance(null);
            balance.Should().Be(new Balance(1000));
        }
    }
}