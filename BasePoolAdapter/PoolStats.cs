using System;
using System.Collections.Generic;

namespace BasePoolAdapter
{
    public class PoolStats
    {
        public DateTime GatheredOn { get; set; }
        public decimal Balance { get; set; }
        public PoolRound Round { get; set; }
        public IList<PoolWorker> Workers { get; set; }

        public PoolStats()
        {
            Round = new PoolRound();
            Workers = new List<PoolWorker>();
        }
    }
}
