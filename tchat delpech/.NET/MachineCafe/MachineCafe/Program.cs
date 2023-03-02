using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace MachineCafe
{
    class Program
    {
        private static string[] drinkList = new string[] { "COCA", "FANTA", "EAU", "VIN" };

        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Bienvenue sur la machine à café !");
            Console.WriteLine("---------------------------------");
            while (true)
            {
                ShowMenu();
            }
        }

        private static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Que voulez-vous faire ?");
            Console.WriteLine("1 - Mode maintenance");
            Console.WriteLine("2 - Boire");
            Console.WriteLine("3 - Boire à plusieurs");
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Maintenance();
                    break;
                case "2":
                    ShowDrinkList();
                    break;
                case "3":
                    MultiDrink();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void MultiDrink()
        {
            Console.WriteLine("Combien êtes vous ?");
            string input = Console.ReadLine();
            for (int i = 0; i < Convert.ToInt16(input); i++)
            {
                Console.WriteLine($"Client N°{i + 1}...");
                ShowDrinkList();
            }
        }

        private static void Maintenance()
        {
            Console.WriteLine("Mode maintenance activé...");
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        private static void ShowDrinkList()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Que voulez vous boire ?");
            for (int i = 1; i <= drinkList.Length; i++)
            {
                Console.WriteLine($"{i} - {drinkList[i - 1]}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            if (Regex.IsMatch(input, @"^\d+$") && Convert.ToInt16(input) <= drinkList.Length)
            {
                Delivery(Convert.ToInt32(input));
                Order();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hein !!! ??? Try again !");
                Console.ForegroundColor = ConsoleColor.White;
                ShowDrinkList();
            }
        }

        private static void Delivery(int selectedDrink)
        {
            Console.WriteLine($"Votre boisson: {drinkList[selectedDrink - 1]}");
        }

        private static void Order()
        {
            int amountTotal = 0;
            int price = 50;

            while (amountTotal < 50)
            {
                Console.WriteLine($"Merci de payer {price} centimes, il reste {price - amountTotal}");
                string input = Console.ReadLine();
                int amount = Convert.ToInt16(input);
                amountTotal += amount;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            if (amountTotal > price)
            {
                Console.WriteLine($"Retour monnaie: {amountTotal - price}");
                Console.WriteLine("Glou glou glou...");
            }
            else
                Console.WriteLine("Glou glou glou...");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Merci de votre votre passage à la marchine à café");
            Console.WriteLine("---------------------------------");
        }
    }
}
