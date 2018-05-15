using System;
using System.Collections.Generic;

namespace IA_Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Numarul de particule: ");
            List<Particula> list = Init(Convert.ToInt32(Console.ReadLine()));
            Particula gBest = GBest(list);

            Console.Write("Numarul de iteratii: ");
            int NumarIteratii = Convert.ToInt32(Console.ReadLine());
            PSO(NumarIteratii, list);
        }

        private static float FR(List<float> x)
        {
            float sum = 0f;

            for (int i = 0; i < 2; i++)
            {
                double x2 = Math.Pow((double)x[i], 2);
                double cos = 10 * Math.Cos(2 * Math.PI * x[i]);

                sum += (float)(x2 - cos);
            }

            return (10 * 2 + sum);
        }

        private static List<Particula> Init(int NumberPopulation)
        {
            List<Particula> list = new List<Particula>();

            for(int i = 0; i < NumberPopulation; i++)
            {
                Random rnd = new Random();
                Particula par = new Particula();

                par.Pozitie.Add((float)(5.12f * rnd.NextDouble()) - 5.12f);
                par.Pozitie.Add((float)(5.12f * rnd.NextDouble()) - 5.12f);

                par.Viteze.Add((float)(1f * rnd.NextDouble()) - 1f);
                par.Viteze.Add((float)(1f * rnd.NextDouble()) - 1f);

                par.Fitnes = FR(par.Pozitie);
                list.Add(par);
            }

            return list;
        }

        private static void PSO(int NumarIteratii, List<Particula> pBest)
        {
            int i = 0;
            while (i < NumarIteratii)
            {

            }
        }

        private static Particula GBest(List<Particula> list)
        {
            Particula par = new Particula();

            par.Fitnes = list[0].Fitnes;
            par.Viteze.AddRange(list[0].Viteze);
            par.Pozitie.AddRange(list[0].Pozitie);

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].Fitnes > par.Fitnes)
                    par = list[i];
            }
            return par;
        }
    }
}
