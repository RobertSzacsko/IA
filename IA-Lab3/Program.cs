using System;
using System.Collections.Generic;

namespace IA_Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            TSP tsp = new TSP();
            tsp.PredefinedOrase();
            List<int> sol = tsp.GenerateSolutionGreedy();

            WriteSol(sol);
            Console.WriteLine(tsp.Eval(sol));
            Console.ReadKey();

            //List<int> solfinal = SA(sol, tsp);

            //WriteSol(solfinal);
            //Console.WriteLine(tsp.Eval(solfinal));
            //Console.ReadKey();
        }

        private static void WriteSol(List<int> solutie)
        {
            foreach (var item in solutie)
            {
                Console.Write(item);
                Console.Write(' ');
            }
            Console.WriteLine();
        }

        //private static List<int> SA(List<int> solutie, TSP tsp)
        //{
        //    float t = 0;
        //    Console.Write("T= ");
        //    int T = Convert.ToInt32(Console.ReadLine());

        //    Console.Write("numarul de iteratii= ");
        //    int k = Convert.ToInt32(Console.ReadLine());

        //    int 

        //    Random rnd = new Random();

        //    while (t < T)
        //    {
        //        int indice = 0;
        //        while (indice < k)
        //        {
        //            List<int> vecin = 2Swap(solutie);

        //            if (tsp.Eval(solutie) < tsp.Eval(vecin))
        //                solutie.AddRange(vecin);
        //            else if (rnd.Next(0, 1) < Math.Exp((tsp.Eval(vecin) - tsp.Eval(solutie)) / T))
        //                solutie.AddRange(vecin);
        //        }


        //        t += 1;
        //    }
        //}
    }
}
