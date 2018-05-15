using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using IA_Tema4;

namespace IA_Tema5
{
    class Program
    {
        static void Main(string[] args)
        {
            /* AS: furnica = lista (orase = int),
             * m = nr furnici
             * n = numarul de iteratii
             * taju(k) = lista din furnica
             */
            TSP tsp = new TSP();
            tsp.ParseXmlFile(@"C:\Users\Robert Szacsko\Downloads\eil51.xml\eil51.xml");

            Console.Write("Numarul de iterarii: ");
            int T = Convert.ToInt32(Console.ReadLine());

            Console.Write("Numarul de furnici: ");
            int NumberOfAnts = Convert.ToInt32(Console.ReadLine());

            Console.Write("Alpha: ");
            double alpha = Convert.ToDouble(Console.ReadLine());

            Console.Write("Beta: ");
            double beta = Convert.ToDouble(Console.ReadLine());

            AC(tsp, T, NumberOfAnts, alpha, beta);
        }

        static private void AC(TSP tsp, int T, int NumberOfAnts, double alpha, double beta)
        {
            int t = 0;
            List<int> ListPositionAnts = Init(tsp.NrOrase);
            List<Furnica> Ants = new List<Furnica>(InitAnts(ListPositionAnts, NumberOfAnts));
            double[,] Pheromone = new double[tsp.NrOrase, tsp.NrOrase];
            double[,] miu = new double[tsp.NrOrase, tsp.NrOrase];
            Furnica BestAnt = new Furnica
            {
                Tur = new List<int>()
            };

            Console.Write("p: ");
            double p = Convert.ToDouble(Console.ReadLine());

            Console.Write("Q: ");
            int Q = Convert.ToInt32(Console.ReadLine());

            InitPheromone(Pheromone, tsp.NrOrase, tsp.NrOrase);
            InitMiu(miu, tsp);
            
            while (t < T)
            {
                for (int pas = 1; pas < tsp.NrOrase; pas++)
                {
                    for (int k = 0; k < NumberOfAnts; k++)
                    {
                        if (t == 0)
                            Ants[k].Tur.Add(StatetransitionRule(tsp, Ants[k], Pheromone, miu, alpha, beta, pas));
                        else
                            Ants[k].Tur[pas] = StatetransitionRule(tsp, Ants[k], Pheromone, miu, alpha, beta, pas);
                    }
                }
                UpdatePheromone(Pheromone, Ants, tsp, p, Q);
                BestAnt.Tur.Clear();
                BestAnt.Tur.AddRange(DetBestAnt(Ants, tsp));
                t++;
            }
            WriteAnts(Ants);
            WriteEvalAnts(Ants, tsp);
            WritePheromone(Pheromone);
            WriteSol(BestAnt.Tur);
            WriteEvalSol(BestAnt, tsp);
            Console.ReadKey();
        }

        static private List<int> Init(int NumberOfAnts)
        {
            List<int> list = new List<int>();

            for(int i = 0; i < NumberOfAnts; i++)
            {
                list.Add(i);
            }

            return list.OrderBy(a => Guid.NewGuid()).ToList();
        }

        static private List<Furnica> InitAnts(List<int> listPosition, int NumberOfAnts)
        {
            List<Furnica> Ants = new List<Furnica>();

            for(int position = 0; position < NumberOfAnts; position++)
            {
                Furnica Ant = new Furnica
                {
                    Tur = new List<int>()
                };
                Ant.Tur.Add(listPosition[position]);
                Ants.Add(Ant);
            }

            return Ants;
        }

        static private void InitPheromone(double[,] list, int NumberOFAnts, int NumberOfCities)
        {
            for (int i = 0; i < NumberOFAnts; i++)
            {
                for (int j = 0; j < NumberOfCities; j++)
                {
                    list[i, j] = 0.1d;
                }
            }
        }

        static private void InitMiu(double[,] miu, TSP tsp)
        {
            for(int i = 0; i < tsp.NrOrase; i++)
            {
                for (int j = 0; j < tsp.NrOrase; j++)
                {
                    if (i != j)
                        miu[i, j] = 1 / tsp.ListaOrase[i][j];
                    else
                        miu[i, j] = 0;
                }
            }
        }

        static private void InitProb(double[] prob, double[,] Pheromone, double[,] miu, Furnica Ant, double alpha, double beta, int pas)
        {
            int index = Ant.Tur[pas - 1];
            double sum = 0;
            for(int i = 0; i < prob.Length; i++)
            {
                if (Ant.Tur.FindIndex(0, pas, x => x == i) == -1)
                    sum = sum + (Math.Pow(Pheromone[index, i], alpha) * Math.Pow(miu[index, i], beta));
            }

            for(int i = 0; i < prob.Length; i++)
            {
                if (i != index && Ant.Tur.FindIndex(0, pas, x => x == i) == -1)
                    prob[i] = (Math.Pow(Pheromone[index, i], alpha) * Math.Pow(miu[index, i], beta)) / sum;
                else
                    prob[i] = 0;
            }
        }

        static private double InitPartialSum(double[] array, int startIndex, int lastIndex)
        {
            double sum = 0;

            for(int i = startIndex; i <= lastIndex; i++)
                sum += array[i];

            return sum;
        }

        static private int StatetransitionRule(TSP tsp, Furnica Ant, double[,] Pheromone, double[,] miu, double alpha, double beta, int pas)
        {
            int BestCity = 0;
            double rnd = new Random().NextDouble();
            double[] prob = new double[tsp.NrOrase];
            InitProb(prob, Pheromone, miu, Ant, alpha, beta, pas);

            for(int i = 0; i < prob.Length; i++)
            {
                double partialSum = InitPartialSum(prob, 0, i);
                if (rnd <= partialSum)
                {
                    BestCity = i;
                    break;
                }
            }

            return BestCity;
        }

        static private void UpdatePheromone(double[,] Pheromone, List<Furnica> list, TSP tsp, double p, double Q)
        {
            for (int i = 0; i < Pheromone.GetLength(0); i++)
            {
                for (int j = 0; j < Pheromone.GetLength(1); j++)
                {
                    Pheromone[i, j] = ((1 - p) * Pheromone[i, j]) + DeltaPheromone(Pheromone, list, tsp, i, j, Q);
                }
            }
        }

        static private double DeltaPheromone(double[,] Pheromone, List<Furnica> list, TSP tsp, int i, int j, double Q)
        {
            double sum = 0;
            
            for(int k = 0; k < list.Count; k++)
            {
                double Deltak = 0;
                int index = 0;
                if (list[k].Tur.Contains(i))
                    index = list[k].Tur.IndexOf(i);
                if (index != 0 && list[k].Tur[(index + 1) % list[k].Tur.Count] == j)
                    Deltak = Q / list[k].Eval(tsp);
                else
                    Deltak = 0;

                sum += Deltak;
            }

            return sum;
        }

        static private List<int> DetBestAnt(List<Furnica> Ants, TSP tsp)
        {
            double eval = 10000000;
            int index = -1;

            for(int i = 0; i < Ants.Count; i++)
            {
                double partial = Ants[i].Eval(tsp);
                if (eval > partial)
                {
                    eval = partial;
                    index = i;
                }
            }

            return Ants[index].Tur;
        }

        static private void WriteSol(List<int> sol)
        {
            Console.WriteLine();
            Console.WriteLine("Best: ");
            sol.ForEach(x => Console.Write("{0} ", x));
            Console.WriteLine();
        }

        static private void WriteAnts(List<Furnica> list)
        {
            list.ForEach(x => WriteSol(x.Tur));
        }

        static private void WriteEvalAnts(List<Furnica> list, TSP tsp)
        {
            list.ForEach(x => Console.WriteLine("Fitness = {0}", x.Eval(tsp)));
        }

        static private void WritePheromone(double[,] phe)
        {
            StreamWriter sw = new StreamWriter(@"C:\Users\Robert Szacsko\source\repos\IA-Lab2\IA-Tema5\Feromoni.txt");

            for (int i = 0;i < phe.GetLength(0); i++)
            {
                for (int j = 0; j < phe.GetLength(1); j++)
                {
                    sw.Write("{0:0.0} ", phe[i, j]);
                }
                sw.WriteLine();
            }
        }

        static private void WriteEvalSol(Furnica ant, TSP tsp)
        {
            Console.WriteLine("Fitness = {0}", ant.Eval(tsp));
        }
    }
}
