

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Zhimera.Optimiser
{
    abstract class BaseOptimiser
    {
        public abstract void Compute_rank(Optimiser.node.Node n);

       /* public static void Main(string[] args)
        {
            StaticOptimiser s = new StaticOptimiser();
            DateTime currentSystemTime = DateTime.Now;
            Console.WriteLine(currentSystemTime);
            Console.Read();
        }*/
    }
}
