using System.Net;
using BasePoolAdapter;
using BasePoolAdapter.Events;

namespace PoolAdapters
{
    public abstract class PoolAdapter
    {
        public string PoolName { get; protected set; }
        public string PoolUrl { get; protected set; }
        public NetworkCredential Credentials { get; set; }

        public abstract void GetPoolStatsAsync();
        
        protected void InvokeGetPoolStatsCompletedEvent(PoolStats poolStats)
        {
            GetPoolStatsCompleted(this, new GetPoolStatsEventArgs(poolStats));
        }

        public delegate void GetPoolStatsEventHandler(object sender, GetPoolStatsEventArgs e);

        public event GetPoolStatsEventHandler GetPoolStatsCompleted = delegate { };
    }
}
