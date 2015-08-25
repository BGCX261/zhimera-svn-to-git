using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Erasure
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream fs = new FileStream("F:/LiveProjects/C#Rookie/debugstatements.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            Console.SetOut(sw);
           
            FinalImpl fi = new FinalImpl();
            fi.BeginEncoding("F:/LiveProjects/C#Rookie/song.mp3", 4);

            sw.Close();
            fs.Close();
            
            Console.Read();
        }
    }
}
