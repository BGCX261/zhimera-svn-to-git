

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Optimiser
{
    abstract class BaseOptimiser
    {
        public abstract int ComputeRank(int bandWidth,
            int latency,
            int packetDropRate,
            int upTime);

        public static void Main(string[] args)
        {
            StaticOptimiser s = new StaticOptimiser();
            int rank = s.ComputeRank(10, 3, 4, 45);
            Console.WriteLine("Rank = +", rank);
            
        }
    }
}
