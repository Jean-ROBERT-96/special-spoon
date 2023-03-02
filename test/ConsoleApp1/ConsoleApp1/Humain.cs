using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Humain : ITest
    {
        protected string nom;
        public string prenom;
        protected char genre;

        public Humain(string nom, string prenom, char genre)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.genre = genre;
        }

        public virtual void Marcher()
        {
            Console.WriteLine($"Je suis {prenom} et je marche avec du dédain!");
        }
    }
}
