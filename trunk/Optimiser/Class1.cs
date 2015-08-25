
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Optimiser
{
    class StaticOptimiser : BaseOptimiser
    {
        const int BW_WEIGHT = 10;
        const int LATENCY_WEIGHT = 9;
        const int PKT_DROP_RATE = 5;
        const int UPTIME = 20;
        const int NORMALIZATION_VALUE = 10;

        public override int ComputeRank(int bandWidth,
            int latency,
            int packetDropRate,
            int upTime)
        {

            return (RankBW(bandWidth) + RankLatency(latency) + RankPkts_Drop(packetDropRate) + RankUptime(upTime));
        }
        protected int RankBW(int bw)     // bw should be in KBps (Kilo Bytes per second)
        {
            if (bw < 0)
                return 0;
            return (int)(bw / 10);
        }
        protected int RankUptime(int ut)   // uptime should be in seconds
        {
            if (ut < 0)
                return 0;
            return (int)(ut / 1000);
        }
        protected int RankLatency(int latency) // latency should be in milliseconds
        {
            if (latency < 0)  // not possbile
                return 0;
            return (1 / (latency * 10));
        }
        protected int RankPkts_Drop(int pkts)
        {
            if (pkts < 0)  // not possbile
                return 0;
            return (1/pkts * 10);
        }
    }
}
