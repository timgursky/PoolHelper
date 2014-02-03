namespace BasePoolAdapter
{
    public class PoolRound
    {
        public decimal Estimate { get; set; }
        public int Shares { get; set; }

        public override string ToString()
        {
            return string.Format("Estimate: {0}; Shares: {1}", Estimate, Shares);
        }
    }
}
