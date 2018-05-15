using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;

namespace IA_Tema4
{
    public class TSP
    {
        public int NrOrase { get; set; }
        public List<List<double>> ListaOrase { get; set; }

        public void ParseXmlFile(string path)
        {
            this.ListaOrase = new List<List<double>>();
#pragma warning disable CS0618 // Type or member is obsolete
            XmlDataDocument xmldoc = new XmlDataDocument();
#pragma warning restore CS0618 // Type or member is obsolete
            XmlNodeList xmlnode;
            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);

            xmldoc.Load(fs);
            xmlnode = xmldoc.GetElementsByTagName("description");

            string description = Regex.Split(xmlnode[0].FirstChild.InnerText.Trim(), "-", RegexOptions.IgnorePatternWhitespace)[0];
            this.NrOrase = Convert.ToInt32(description);
            double self = 0.00d;

            xmlnode = xmldoc.GetElementsByTagName("vertex");

            for (int i = 0; i < this.NrOrase; i++)
            {
                List<double> Oras = new List<double>();
                for (int j = 0; j <= xmlnode[i].ChildNodes.Count; j++)
                {
                    if (i == j)
                        Oras.Add(self);
                    if (j != xmlnode[i].ChildNodes.Count)
                        if (double.TryParse(xmlnode[i].ChildNodes[j].Attributes.GetNamedItem("cost").InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out double number))
                        {
                            Oras.Add(number);
                        }
                }
                this.ListaOrase.Add(Oras);
            }
        }
    }
}
