using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Text.RegularExpressions;

namespace IA_Tema4
{
    class Program
    {
        static void Main(string[] args)
        {
            TSP tsp = new TSP();
            tsp.ParseXmlFile(@"C:\Users\Robert Szacsko\Downloads\eil51.xml\eil51.xml");

            AE(tsp);
            //WriteSolution(tsp);
        }

        static private void AE(TSP tsp)
        {
            Console.Write("Numarul de generatii = ");
            int NumberOfGeneration = Convert.ToInt32(Console.ReadLine());

            Console.Write("Numarul de indivizi = ");
            int NumberOfPersons = Convert.ToInt32(Console.ReadLine());

            List<List<int>> Population = Init(NumberOfPersons, tsp.NrOrase);
            
            int t = 0;

            while (t < NumberOfGeneration)
            {
                int punct1 = new Random().Next(0, tsp.NrOrase);
                int punct2 = new Random().Next(punct1, tsp.NrOrase);

                List<List<int>> Children = OX(Population, punct1, punct2);
                t++;
            }
        }

        static private List<List<int>> Init(int NumberOfPersons, int NumberOfCity)
        {
            List<List<int>> Population = new List<List<int>>();
            Random rnd = new Random();

            for(int i = 0; i < NumberOfPersons; i++)
            {
                List<int> Individ = InitDefaultIndivid(NumberOfCity).OrderBy(a => Guid.NewGuid()).ToList();
                Population.Add(Individ);
            }

            return Population;
        }

        static private List<int> InitDefaultIndivid(int NumberOfCity)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < NumberOfCity; i++)
            {
                list.Add(i);
            }

            return list;
        }

        static private List<List<int>> OX(List<List<int>> Parents, int punct1, int punct2)
        {
            List<List<int>> Children = new List<List<int>>(Parents.Count());
            Console.WriteLine(punct1);
            Console.WriteLine(punct2);

            for (int i = 0; i < Parents.Count; i+=2)
            {
                List<int> o1 = new List<int>();
                List<int> o2 = new List<int>();

                o1.AddRange(Parents[i]);
                o2.AddRange(Parents[i + 1]);

                int j;
                int aux = punct2;
                int aux2 = punct2;
                for (j = punct2; j < Parents[i].Count; j++)
                {
                    if (CheckIfExists(o1, Parents[i + 1][j]) == false)
                    {
                        //o1[aux % Parents[i].Count] = Parents[i + 1][j];
                        o1.RemoveAt(aux % Parents[i].Count);
                        o1.Insert(aux % Parents[i].Count, Parents[i + 1][j]);
                        aux++;
                    }
                    if (CheckIfExists(o2, Parents[i][j]) == false)
                    {
                        o1[aux2 % Parents[i + 1].Count] = Parents[i][j];
                        aux2++;
                    }
                }

                for (j = 0; j < punct2; j++)
                {
                    if (CheckIfExists(o1, Parents[i + 1][j]) == false)
                    {
                        o1[aux % Parents[i].Count] = Parents[i + 1][j];
                        aux++;
                    }
                    if (CheckIfExists(o2, Parents[i][j]) == false)
                    {
                        o1[aux2 % Parents[i + 1].Count] = Parents[i][j];
                        aux2++;
                    }
                }
                WriteIndivid(o1);
                WriteIndivid(Parents[i]);
                WriteIndivid(o2);
                WriteIndivid(Parents[i + 1]);

                Children.Add(o1);
                Children.Add(o2);
            }

            return Children;
        }

        static private Boolean CheckIfExists(List<int> o1, int nod)
        {
            foreach(int node in o1)
            {
                if (node == nod)
                    return false;
            }
            return true;
        }

        static private void WriteIndivid(List<int> item)
        {
            item.ForEach(i => Console.Write("{0} ", i));
            Console.WriteLine();
        }
    }
}
