using System;
using System.Collections.Generic;

namespace BasePoolAdapter
{
    public class PoolStats
    {
        public DateTime GatheredOn { get; set; }
        public decimal ConfirmedBalance { get; set; }
        public decimal PaidOutAmount { get; set; }
        public PoolRound Round { get; set; }
        public IList<PoolWorker> Workers { get; set; }

        public PoolStats()
        {
            Round = new PoolRound();
            Workers = new List<PoolWorker>();
        }

        public override string ToString()
        {
            return string.Format("ConfirmedBalance: {0}; PaidOutAmount: {1}; Round: {{{2}}}", ConfirmedBalance, PaidOutAmount, Round);
        }
    }
}
