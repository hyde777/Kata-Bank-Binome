namespace Bank
{
    public class HistoryLine
    {
        private decimal credit;

        public HistoryLine(decimal credit)
        {
            this.credit = credit;
        }

        protected bool Equals(HistoryLine other)
        {
            return credit == other.credit;
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
    }
}