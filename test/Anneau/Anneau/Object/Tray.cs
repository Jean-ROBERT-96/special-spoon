using Anneau.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Anneau.Object
{
    public class Tray : ITray
    {
        public List<Tower> Towers { get; set; }

        public Tray()
        {
            Towers = new List<Tower>
            {
                new Tower(),
                new Tower(),
                new Tower()
            };
            Initialize();
            Console.WriteLine("Plateau créé.");
        }

        void Initialize()
        {
            for(int i = 5; i > 0; i--)
            {
                Towers[0].Rings.Add(new Ring(i));
            }
        }

        public void MoveRings(int ring, int colSelect, int colFinal)
        {
            Ring ringSelect = Towers[colSelect].Rings.Find(x => x.size == ring);
            Towers[colFinal].Rings.Add(ringSelect);
            Towers[colSelect].Rings.Remove(ringSelect);
        }
    }
}
