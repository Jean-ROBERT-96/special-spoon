using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Homme : Humain, ITest
    {
        int speHomme;

        public Homme(int speHomme, string nom, string prenom, char genre) : base(nom, prenom, genre)
        {
            this.speHomme = speHomme;
        }

        public void MethodeHomme()
        {
            Console.WriteLine("Je fait un truc d'homme!");
        }
    }
}
