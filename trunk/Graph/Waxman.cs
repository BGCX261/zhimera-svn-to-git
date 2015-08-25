using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Graph
{
    class Waxman
    {
        const int MAX_X = 200;
        const int MAX_Y = 200;
        public int GetRandomX(int seed)
        {
            // Use dt.Millisecond as the seed in the real system.
            // Fr testing purpose just use a known seed.

            DateTime dt = DateTime.Now;
            //Random rand = new Random(seed + dt.Millisecond);
            Random rand = new Random(seed);
            int r = rand.Next(MAX_X);
            return r;

        }

        public int GetRandomY(int seed)
        {
            // Use dt.Millisecond as the seed in the real system.
            // Fr testing purpose just use a known seed.

            DateTime dt = DateTime.Now;
            //Random rand = new Random(seed + dt.Millisecond);
            Random rand = new Random(seed);
            int r = rand.Next(MAX_Y);

            // this is to get a new random number as the seed will be the same when GetRandomX() and GetRandomY()
            // are called successively
            r = rand.Next(MAX_Y);
            return r;

        }

        public double ProbFunc(Node src, Node dest)
        {
            double distance, L, alpha, beta;
            alpha = 0.15;
            beta = 0.2;
            int x1, x2, y1, y2, dx, dy;
            x1 = src.Xpos; x2 = dest.Xpos;
            y1 = src.Ypos; y2 = dest.Ypos;

            dx = x2 - x1;
            dy = y2 - y1;

            distance = Math.Sqrt(dx * dx + dy * dy);
            //Console.WriteLine("Distance between " + src.node_id + " and " + dest.node_id + " = " + distance);
            L = Math.Sqrt(2) * MAX_X;

            /*Console.WriteLine("distance = " + distance);
            Console.WriteLine("L = " + L);*/
            //Console.WriteLine("final value = " + (alpha * Math.Exp(-1.0 * (distance / (beta * L)))));

            return 10 * alpha * Math.Exp(-1.0 * (distance / (beta * L)));
        }

    }
}
