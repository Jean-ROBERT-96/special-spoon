using System.ComponentModel;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new Dictionary<int, string>
            {
                { 3, "Tomate" },
                { 5, "Pizza" },
                { 7, "Cocombre" },
                { 11, "Cerise" }
            };

            int rep = int.Parse(Console.ReadLine());

            for(int i = 1; i < rep; i++)
            {
                
            }

            foreach (var item in list)
            {
                if ((rep % item.Key) == 0)
                {
                    Console.WriteLine(item.Value);
                }
            }
        }
    }
}