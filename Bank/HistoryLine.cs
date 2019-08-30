using System;

namespace Bank
{
    public class HistoryLine
    {
        private readonly decimal credit;
        private readonly Id id;
        public Balance Balance { get; }
        public DateTime Date { get; }

        public HistoryLine(DateTime date, decimal credit, Id id, Balance balance)
        {
            Date = date;
            this.Balance = balance;
            this.id = id;
            this.credit = credit;
        }

        private bool Equals(HistoryLine other)
        {
            return credit == other.credit && id.Equals(other.id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HistoryLine) obj);
        }

        public override int GetHashCode()
        {
            return credit.GetHashCode();
        }

        public bool OwnedByAccountId(Id accountId)
        {
            return Equals(accountId, id);
        }
    }
}