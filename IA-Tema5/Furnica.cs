using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IA_Tema4;

namespace IA_Tema5
{
    class Furnica
    {
        public List<int> Tur { get; set; }

        public double Eval(TSP tsp)
        {
            double value = 0;

            for(int i = 0; i < this.Tur.Count; i++)
            {
                value += tsp.ListaOrase[this.Tur[i]][this.Tur[(i + 1) % this.Tur.Count]];
            }

            return value;
        }
    }
}
