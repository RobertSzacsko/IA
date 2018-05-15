using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IA_Lab3
{
    class TSP
    {
        int[,] orase = new int[5,5];
        int NrOrase = 5;

        public void PredefinedOrase()
        {
            this.orase[0,1] = 1;
            this.orase[0,2] = 2;
            this.orase[0,3] = 3;
            this.orase[0,4] = 4;
            this.orase[1,2] = 2;
            this.orase[1,3] = 3;
            this.orase[1,4] = 4;
            this.orase[2,2] = 5;
            this.orase[2,3] = 4;
            this.orase[3,4] = 5;
        }

        public List<int> GenerateSolutionGreedy()
        {
            Console.Write("numarul initial = ");
            int k = Convert.ToInt32(Console.ReadLine());
            List<int> sol = new List<int>(5);
            sol.Add(k);

            int costDrum = 100000;
            int oras = 0;
            int count = 0;
            while (count < 5)
            {
                int i = k % 5;
                for (int j = (count + 1) % 5; j < this.NrOrase; j++)
                {
                    if ( sol.Contains(j) == false && costDrum > this.orase[i,j])
                    {
                        costDrum = this.orase[i,j];
                        oras = j;
                    }
                }
                costDrum = 100000;
                sol.Add(oras);
                count++;
            }

            return sol;
        }

        public int Eval(List<int> Solutie)
        {
            int costTotal = 0;

            for (int i = 0; i < this.NrOrase; i++)
            {
                costTotal += this.orase[i, (i + 1) % 5];
            }

            return costTotal;
        }
    }
}
