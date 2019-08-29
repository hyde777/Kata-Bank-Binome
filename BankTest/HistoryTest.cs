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
            IHistory history = new History();
            List<HistoryLine> historyLines = history.Get(null);
            historyLines.Should().Equal(new List<HistoryLine>
            {
                new HistoryLine(1000)
            });
        }
        
        
    }
}