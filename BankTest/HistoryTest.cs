using System;
using System.Collections.Generic;
using Bank;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class HistoryTest
    {
        private readonly Id anyAccountId = new Id("test");

        [Test]
        public void should_josey_history_lines_when_we_ask_it()
        {
            HistoryLine josey = new HistoryLine(new DateTime(2000,1,1), 2000, new Id("josey"), null);
            HistoryLine marie = new HistoryLine(new DateTime(2000,1,1), 1000, new Id("marie"),null);
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
            Balance balance = new History(new List<HistoryLine>()).GetBalance(anyAccountId);
            balance.Should().Be(new Balance(0));
        }

       [Test]
        public void should_give_the_last_balance()
        {
            List<HistoryLine> historyLines = new List<HistoryLine>
            {
                new HistoryLine(new DateTime(2012,1,10), 0,anyAccountId,new Balance(1000)),
                new HistoryLine(new DateTime(2012,1,14),0,anyAccountId,new Balance(3000))
            };
            IHistory history = new History(historyLines);
            Balance balance = history.GetBalance(anyAccountId);
            balance.Should().Be(new Balance(3000));
        }
    }
}