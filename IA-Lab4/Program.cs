using System;
using System.Collections.Generic;

namespace IA_Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Numarul de indivizi = ");
            int NumarIndivizi = Convert.ToInt32(Console.ReadLine());
            List<List<float>> Indivizi = GenerateRandomIndivizi(NumarIndivizi);

            AE(Indivizi, 10, NumarIndivizi);

            Console.ReadKey();
        }

        private static List<List<float>> GenerateRandomIndivizi(int NumarIndivizi)
        {
            List<List<float>> Lista = new List<List<float>>();

            for (int i = 0; i < NumarIndivizi; i++)
            {
                List<float> Individ = GenerateRandomIndivid();
                Lista.Add(Individ);
            }

            return Lista;
        }

        private static List<float> GenerateRandomIndivid()
        {
            Random rnd = new Random();
            List<float> Individ = new List<float>();
            float x1 = GetRandom(rnd);
            float x2 = GetRandom(rnd);

            Individ.Add(x1);
            Individ.Add(x2);

            return Individ;
        }

        private static float GetRandom(Random rnd)
        {
            int random = rnd.Next(0, 1025);
            float i;
            for (i = -5.12f; i <= 5.12f; i = i + 0.01f)
            {
                if (random == 0)
                    break;
                random--;
            }

            return i;
        }

        private static void AE(List<List<float>> lista, int NumarPerechi, int NumarIndivizi)
        {
            List<List<float>> ListaParinti = new List<List<float>>();
            List<List<float>> ListaCopii = new List<List<float>>();
            Random rnd = new Random();

            for (int i = 0; i < NumarPerechi; i++)
            {
                List<float> SelectFirstParinte = SelectRandomParinte(lista, NumarIndivizi);
                List<float> SelectSecondParinte = SelectTurnirParinte(lista, NumarIndivizi);

                ListaParinti.Add(SelectFirstParinte);
                ListaParinti.Add(SelectSecondParinte);
            }

            //for (int i = 0; i < NumarPerechi; i+=2)
            //{
            //    int random = rnd.Next(0, 2);
            //    float  media = (ListaParinti[i][random] + ListaParinti[i++][random]) / 2;
            //    ListaCopii.AddRange()

            //}
        }

        private static List<float> SelectRandomParinte(List<List<float>> lista, int NumarIndivizi)
        {
            Random random = new Random();
            int rnd = random.Next(0, NumarIndivizi);

            List<float> SelectedParinte = new List<float>();
            SelectedParinte.AddRange(lista[rnd]);

            return SelectedParinte;
        }

        private static List<float> SelectTurnirParinte(List<List<float>> lista, int NumarIndivizi)
        {
            Random random = new Random();
            int rnd = random.Next(0, NumarIndivizi);
            int rnd2 = random.Next(0, NumarIndivizi);
            List<float> listaTurnir = new List<float>();

            if (FR(lista[rnd]) > FR(lista[rnd2]))
                listaTurnir.AddRange(lista[rnd2]);
            else
                listaTurnir.AddRange(lista[rnd]);

            return listaTurnir;
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
    }
}
