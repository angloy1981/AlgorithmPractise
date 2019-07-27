using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DataStrutAndAlgorithmPractise
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] lData = CreateArray(5000000);
            ArrayList<int> lArrayData = new ArrayList<int>();
            foreach (var item in lData)
            {
                lArrayData.Add(item);
            }
            Stopwatch oWatch = new Stopwatch();
            oWatch.Start();
            lArrayData.Sort((m, n) => { if (m < n) return true; else return false; });
            oWatch.Stop();
            //lArrayData.Print(m => { Console.Write(m.ToString() + "-"); });
            Console.WriteLine("");
            Console.WriteLine("总耗时：" + oWatch.ElapsedMilliseconds.ToString()+"ms");

            //lArrayData.Reverse();
            //lArrayData.Print(m => { Console.Write(m.ToString() + "-"); });
            //Console.WriteLine("");
            Console.Read();
        }
        static int[] CreateArray(int iCount)
        {
            Random oRandom = new Random();
            int[] lData = new int[iCount];
            for (int i = 0; i < iCount; i++)
            {
                lData[i] = oRandom.Next(0, iCount);
            }
            return lData;
        }
    }
}
