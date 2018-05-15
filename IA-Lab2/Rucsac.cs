using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace IA_Lab2
{
    class Rucsac
    {
        public int NumarObiecte { get; set; }
        public int GreutateMaxima { get; set; }
        public List<Obiect> listaObiecte { get; set; }

        public void CitesteDinFisier(string path)
        {
            string[] lines = File.ReadAllLines(path);
            this.NumarObiecte = Convert.ToInt32(lines[0]);
            List<Obiect> lista = new List<Obiect>(this.NumarObiecte);

            for (int i = 1; i <= this.NumarObiecte; i++)
            {
                string[] line = Regex.Split(lines[i], "\\s", RegexOptions.IgnorePatternWhitespace);
                Obiect ob = new Obiect();

                ob.Valoare = Convert.ToInt32(line[2]);
                ob.Greutate = Convert.ToInt32(line[3]);
                lista.Add(ob);
            }
            this.listaObiecte = lista;
            this.GreutateMaxima = Convert.ToInt32(lines[lines.Length - 1]);
        }

        public int GreutateTotalaASolutie(List<int> solutie)
        {
            int greutateTotala = 0;
            for (int i = 0; i < solutie.Capacity; i++)
            {
                if (solutie[i] == 1)
                    greutateTotala = greutateTotala + this.listaObiecte[i].Greutate;
            }

            return greutateTotala;
        }

        public int Fitnes(List<int> solutie)
        {
            int fitnes = 0;
            for (int i = 0; i < this.NumarObiecte; i++)
            {
                if (solutie[i] == 1)
                    fitnes += this.listaObiecte[i].Valoare;
            }

            return fitnes;
        }
    }
}
