using System;
using System.Collections.Generic;

namespace IA_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Rucsac rucsac = new Rucsac();
            string path = Meniu();
            string con = "y";
            rucsac.CitesteDinFisier(path);

            while (con == "y")
            {
                Console.Write("k = ");
                int k = Convert.ToInt32(Console.ReadLine());
                List<int> solFinal = SAHL(rucsac, k);
                int fitnes = rucsac.GreutateTotalaASolutie(solFinal);
                
                Console.WriteLine(fitnes);
                WriteSolutie(solFinal);
                WriteSolutieToFile(solFinal, fitnes);

                Console.WriteLine("Vrei sa mai rulezi inca o data? (y sau n)");
                con = Console.ReadLine();
            }
        }

        private static string  Meniu()
        {
            Console.WriteLine("1. Rucsac 20");
            Console.WriteLine("2. Rucsac 200");
            Console.WriteLine("0. Exit");
            int option = Convert.ToInt32(Console.ReadLine());
            string path = "";

            switch (option)
            {
                case 1:
                    path = @"D:\Robi\An3\Sem2\IA\IA-Lab1\IA-Lab1\Objects-20.txt";
                    break;
                case 2:
                    path = @"D:\Robi\An3\Sem2\IA\IA-Lab1\IA-Lab1\Objects-200.txt";
                    break;
                default:
                    break;
            }

            return path;
        }

        private static List<List<int>> GenerateKSolutii(int k, int NumarObiecte)
        {
            List<List<int>> listaSolutii = new List<List<int>>(NumarObiecte);

            for (int i = 0; i < k; i++)
            {
                List<int> solutie = new List<int>(NumarObiecte);
                Random rnd = new Random();
                solutie = GenerateRandomSearch(NumarObiecte, rnd);
                listaSolutii.Add(solutie);
            }
            return listaSolutii;
        }

        private static List<int> GenerateRandomSearch(int NumarObiecte, Random rnd)
        {
            List<int> solutie = new List<int>(NumarObiecte);

            for (int i = 0; i < NumarObiecte; i++)
            {
                solutie.Add(rnd.Next(0, 2));
            }
            return solutie;
        }

        private static void WriteSolutie(List<int> solutie)
        {
            foreach (int bit in solutie)
            {
                Console.Write(bit + " ");
            }
            Console.WriteLine();
            Console.ReadKey();
        }

        private static List<int> SAHL(Rucsac r, int k)
        {
            List<int> solutie = GenerateRandomSearch(r.NumarObiecte, new Random());
            List<int> best = new List<int>(r.NumarObiecte);
            int indice = 0;
            int contor = 0;

            solutie = RepairSolution(solutie, r);
            best = new List<int>(solutie);
            Console.WriteLine(r.GreutateTotalaASolutie(solutie));
            Console.WriteLine("Valoare = " + r.Fitnes(solutie));
            WriteSolutie(solutie);
            
            while (indice < k)
            {
                List<int> vecinSol = GetVecin(solutie, r.NumarObiecte);

                if (Eval(vecinSol, solutie, r) == true)
                {
                    solutie = new List<int>(vecinSol);
                    Console.WriteLine("\nSe schimba solutie\n");

                    if (Eval(solutie, best, r) == true)
                    {
                        best = new List<int>(solutie);
                        Console.WriteLine("\nSe schimba bestul\n");
                    }
                }
                else
                    contor++;

                if (contor == r.NumarObiecte)
                {
                    solutie = GenerateRandomSearch(r.NumarObiecte, new Random());
                    solutie = RepairSolution(solutie, r);
                    contor = 0;
                }
                indice++;
            }

            return best;
        }

        private static Boolean CheckIsValid(List<int> sol, Rucsac r)
        {
            if (r.GreutateTotalaASolutie(sol) > r.GreutateMaxima)
                return false;
            return true;
        }

        private static List<int> RepairSolution(List<int> sol, Rucsac r)
        {
            while (CheckIsValid(sol, r) == false)
            {
                Random rnd = new Random();
                int indice = rnd.Next(0, r.NumarObiecte);
                while (sol[indice] == 0)
                {
                    indice = rnd.Next(0, r.NumarObiecte);
                }
                sol[indice] = 0;
            }
            return sol;
        }

        private static List<int> GetVecin(List<int> sol, int NO)
        {
            int indice = new Random().Next(0, NO);

            if (sol[indice] == 0)
                sol[indice] = 1;
            else
                sol[indice] = 0;

            return sol;
        }

        private static Boolean Eval(List<int> vecin, List<int> sol, Rucsac r)
        {
            if (CheckIsValid(vecin, r) == true)
            {
                if (r.Fitnes(vecin) > r.Fitnes(sol))
                {
                    return true;
                }
            }
            return false;
        }

        private static void WriteSolutieToFile(List<int> solutie, int fitnes)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Robert Szacsko\source\repos\IA-Lab2\IA-Lab2\Results.txt", true);

            foreach (int bit in solutie)
            {
                file.Write(bit + ' ');
            }
            file.Write('\n');
            file.Write(fitnes + '\n');
            file.Close();
        }
    }
}