using System;

namespace Bank
{
    public class HistoryLine
    {
        private readonly decimal movement;
        private readonly Id id;
        public Balance Balance { get; }
        public DateTime Date { get; }
        public decimal Movement => movement;

        public HistoryLine(DateTime date, decimal movement, Id id, Balance balance)
        {
            Date = date;
            Balance = balance;
            this.id = id;
            this.movement = movement;
        }

        private bool Equals(HistoryLine other)
        {
            return movement == other.movement && id.Equals(other.id);
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
            return movement.GetHashCode();
        }

        public bool OwnedByAccountId(Id accountId)
        {
            return Equals(accountId, id);
        }
    }
}