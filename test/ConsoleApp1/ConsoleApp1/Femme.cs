using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Femme : Humain, ITest
    {
        int speFemme;

        public Femme(int speFemme, string nom, string prenom, char genre) : base(nom, prenom, genre)
        {
            this.speFemme = speFemme;
        }

        public void MethodeFemme()
        {
            Console.WriteLine("Je fait un truc de femme!");
        }

        public override void Marcher()
        {
            Console.WriteLine($"Je suis {prenom} et je marche avec du dédain n'est-ce pas?");
        }
    }
}
