using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Zhimera.Optimiser
{
    class ReadArray
    {
        public UInt32[] ReturnArray()
        {
            UInt32[] uptime_array = { 0, 0, 0, 0, 0 };
            ArrayList uptime_list = new ArrayList(5);
            FileStream fp;
            string line;
            try
            {

                fp = new FileStream("F:/LiveProjects/C#Rookie/Uptime_history.txt", FileMode.Open, FileAccess.Read);
            }
            catch (IOException)
            {
                Console.WriteLine("No history file, returning empty array");
                return uptime_array;
            }

            StreamReader sr = new StreamReader(fp);

            while ((line = sr.ReadLine()) != null)
            {
                uptime_list.Add(line);

            }
            fp.Close();

            uptime_list.Reverse();
            if (uptime_list.Count > 5)
            {
                //we are interested in only the last 5 entries
                for (int z = 0; z < 5; z++)
                {
                    uptime_array[z] = Convert.ToUInt32(uptime_list[z]);
                }
            }

            else if (uptime_list.Count < 5)
            {
                for (int z = 0; z < uptime_list.Count; z++)
                {
                    uptime_array[z] = Convert.ToUInt32(uptime_list[z]);
                }
            }


            for (int z = 0; z < 5; z++)
            {
                Console.WriteLine("reversed contents = " + uptime_array[z]);
            }

            return uptime_array;
        }
        
        /*  
            UInt32 cur_uptime = 200, rank = 0;
            
            ReadArray ra = new ReadArray();
            UInt32[] uptime_array = ra.ReturnArray();
            ExpoImpl ee = new ExpoImpl();
            rank = ee.Compute_rank(uptime_array, cur_uptime);
         */

    }
}
