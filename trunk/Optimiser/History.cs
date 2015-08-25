using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zhimera.Optimiser
{
    class History : BaseOptimiser
    {

        const UInt32 CUR_UPTIME_WEIGHT = 10;
        const UInt32 FIRST_UPTIME_HIS_WEIGHT = 5;
        const UInt32 SECOND_UPTIME_HIS_WEIGHT = 4;
        const UInt32 THIRD_UPTIME_HIS_WEIGHT = 3;
        const UInt32 FOURTH_UPTIME_HIS_WEIGHT = 2;
        const UInt32 FIFTH_UPTIME_HIS_WEIGHT = 1;

        const double EXPO_RATE = -0.000096;
        const UInt32 EXPO_INTIAL_RANK = 10;

        public override UInt32 Compute_rank(UInt32[] array, UInt32 cur_uptime)
        {
            UInt32 weights = Add_weights(array, cur_uptime);
            Console.WriteLine("weighted rank = " + weights);
            return (Compute_final_rank(weights));

        }

        public UInt32 Add_weights(UInt32[] array, UInt32 cur_uptime)
        {
            return ((UInt32)((cur_uptime * CUR_UPTIME_WEIGHT) +
                (array[0] * FIRST_UPTIME_HIS_WEIGHT) +
                (array[1] * SECOND_UPTIME_HIS_WEIGHT) +
                (array[2] * THIRD_UPTIME_HIS_WEIGHT) +
                (array[3] * FOURTH_UPTIME_HIS_WEIGHT) +
                (array[4] * FIFTH_UPTIME_HIS_WEIGHT)));

        }

        public UInt32 Compute_final_rank(UInt32 weighted_rank)
        {
            double result;
            result = Math.Exp(EXPO_RATE * weighted_rank);

            Console.WriteLine("result = " + result);

            double d = Math.Round(EXPO_INTIAL_RANK * result);
            Console.WriteLine("d = " + d);
            return (UInt32)d;

        }

    }

}
