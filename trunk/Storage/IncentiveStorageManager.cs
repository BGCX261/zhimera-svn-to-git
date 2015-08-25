using System;
using System.Collections.Generic;
using System.Text;

namespace Zhimera.Storage
{
    class IncentiveStorageManager
    {
        double churnRank;
        double storageRank;
        double bandwidthRank;

        public IncentiveStorageManager()
        {
            double churnRank = 5.0;
            double storageRank = 5.0;
            double bandwidthRank = 5.0;

        }

        public double[] getRank()
        {
            return new double[3]{churnRank, storageRank, bandwidthRank};
        }
    }
}
