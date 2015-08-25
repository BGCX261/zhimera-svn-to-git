using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Timers;


namespace TimeStoreFile
{
    class Program
    {
      
        /*static void Main(string[] args)
        {
            UpTimeLogger ut = new UpTimeLogger();

                    
            Console.Read();

        }*/
        /*public void junk()
        {
           // Final algo that will be going to TimeStoreImpl...
           StreamReader sr;

            DateTime cur_system_time, last_uptime_time;
            TimeSpan time_diff;
            String cur_system_time_str, cum_uptime_str;

            int count = 0;
            cur_system_time = DateTime.Now;
            ArrayList Arraylines = new ArrayList();
            string line;

            FileStream fptr = new FileStream("F:/LiveProjects/C#Rookie/TimeStore.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            sr = new StreamReader(fptr);
            if ((line = sr.ReadLine()) == null)
            {
                Console.WriteLine("readline=null");
                StreamWriter sout = new StreamWriter(fptr);

                cur_system_time_str = Convert.ToString(cur_system_time);

                sout.WriteLine(cur_system_time_str);
                sout.Close();
                sr.Close();
                fptr.Close();
                return;
            }

            else
            {
                Console.WriteLine("entering ELSE");
                fptr.Seek(0, SeekOrigin.Begin);
                while ((line = sr.ReadLine()) != null)
                {
                    Arraylines.Add(line);

                    count++;
                    Console.WriteLine("count = " + count);
                }

                sr.Close();
                fptr.Close();

                last_uptime_time = Convert.ToDateTime(Arraylines[count - 1]);
                Console.WriteLine("Last line = " + Arraylines[count - 1]);

                time_diff = new TimeSpan();
                time_diff = cur_system_time - last_uptime_time;
                Console.WriteLine("time diff in seconds = " + time_diff.TotalSeconds);
                if (time_diff.TotalSeconds > 3)
                {
                    // Update the uptime_history file and the TimeStore file

                    UInt32 cum_uptime = 0;
                    for (int z = Arraylines.Count - 1; z > 0; z--)
                    {
                        DateTime last_uptime = Convert.ToDateTime(Arraylines[z]);
                        DateTime last_last_uptime = Convert.ToDateTime(Arraylines[z - 1]);
                        TimeSpan diff = last_uptime - last_last_uptime;

                        if (diff.TotalSeconds < 3)
                        {
                            cum_uptime = (UInt32)(cum_uptime + diff.TotalSeconds);
                        }

                        else
                        {
                            break;
                        }
                    }
                    Console.WriteLine("cum_uptime = " + cum_uptime);

                    Console.WriteLine("Updating uptime_history file");
                    FileStream fp = new FileStream("F:/LiveProjects/C#Rookie/Uptime_history.txt", FileMode.Append);
                    StreamWriter sout = new StreamWriter(fp);

                    cum_uptime_str = Convert.ToString(cum_uptime);

                    sout.WriteLine(cum_uptime_str);

                    sout.Close();
                    fp.Close();

                    Console.WriteLine("Updating TimeStore file");
                    fp = new FileStream("F:/LiveProjects/C#Rookie/TimeStore.txt", FileMode.Append);
                    sout = new StreamWriter(fp);
                    cur_system_time_str = Convert.ToString(cur_system_time);

                    sout.WriteLine(cur_system_time_str);

                    sout.Close();
                    fp.Close();

                    return;
                }
                else
                {
                    // Update the TimeStore file
                    Console.WriteLine("Updating only the TimeStore file");
                    FileStream fp;

                    fp = new FileStream("F:/LiveProjects/C#Rookie/TimeStore.txt", FileMode.Append);
                    StreamWriter sout = new StreamWriter(fp);

                    cur_system_time_str = Convert.ToString(cur_system_time);
                    //cur_system_time_str = cur_system_time_str + "\r\n";
                    sout.WriteLine(cur_system_time_str);

                    sout.Close();
                    fp.Close();
                }

          
        }*/

    }
}
