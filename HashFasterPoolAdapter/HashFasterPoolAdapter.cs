using BasePoolAdapter;
using BasePoolAdapter.Events;

namespace PoolAdapters
{
    public class HashFasterPoolAdapter : PoolAdapter
    {
        public HashFasterPoolAdapter()
        {
            PoolName = "HashFaster";
            PoolUrl = @"http://ltc.hashfaster.com/";
        }

        public override void GetPoolStatsAsync()
        {
            InvokeGetPoolStatsCompletedEvent(new PoolStats());
        }
    }
}
