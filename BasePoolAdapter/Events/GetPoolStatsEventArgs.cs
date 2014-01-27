using System;

namespace BasePoolAdapter.Events
{
    public class GetPoolStatsEventArgs : EventArgs
    {
        public readonly PoolStats PoolStats;

        public GetPoolStatsEventArgs(PoolStats poolStats)
        {
            PoolStats = poolStats;
        }
    }
}
