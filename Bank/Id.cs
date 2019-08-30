namespace Bank
{
    public class Id
    {
        private readonly string clientName;

        public Id(string clientName)
        {
            this.clientName = clientName;
        }

        private bool Equals(Id other)
        {
            return clientName == other.clientName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Id) obj);
        }

        public override int GetHashCode()
        {
            return (clientName != null ? clientName.GetHashCode() : 0);
        }
    }
}