namespace MachineACafe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string password = "160164";
            Dictionary<int, string> listCofee = new Dictionary<int, string>() { {1, "expresso"}, {2, "Cappucino"}, {3, "Café au lait"}, {4, "Chocolat"} };
            int select = 0;
            bool repair = false;

            while(true)
            {
                while(select <= 0 || select > listCofee.Count || select == 99 || select == 100)
                {
                    select = Select(listCofee);
                    if(select <= 0 || select > listCofee.Count && select != 99 && select != 100)
                    {
                        Console.WriteLine("Choix invalide.");
                        Thread.Sleep(2000);
                        Console.Clear();
                    }
                    else if(select == 99)
                    {
                        Console.WriteLine("Mode maintenance, entrez le mot de passe : ");
                        string pass = Console.ReadLine();
                        if(!repair && pass == password)
                        {
                            repair = true;
                            Console.WriteLine("Mode maintenance activé.");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        else if(repair && pass == password)
                        {
                            repair = false;
                            Console.WriteLine("Mode maintenance désactivé.");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                        else
                        {
                            Console.WriteLine("Mot de passe incorrect.");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                    }
                    else if(select == 100)
                    {
                        Console.WriteLine("Extinction, veuillez entrer le mot de passe");
                        string pass = Console.ReadLine();
                        if (pass == password)
                        {
                            Console.WriteLine("Au revoir.");
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                        }
                        else
                        {
                            Console.WriteLine("Mot de passe incorrect.");
                            Thread.Sleep(2000);
                            Console.Clear();
                        }
                    }
                }

                if(repair)
                {
                    Console.WriteLine("Machine en maintenance, veuillez nous excuser.");
                    Thread.Sleep(2000);
                }
                
                if(repair == false && select >= 0 && select <= listCofee.Count && Buy())
                {
                    Console.WriteLine("Café en préparation, veuillez patienter...");
                    Thread.Sleep(5000);
                    Console.WriteLine("Café prêt, vous pouvez vous servir.");
                    Thread.Sleep(5000);
                }

                select = 0;
                Console.Clear();
            }
        }

        public static int Select(Dictionary<int, string> list)
        {
            Console.WriteLine("Bonjour, selectionnez votre choix :");
            foreach (var item in list)
            {
                Console.WriteLine($"{item.Key.ToString()} : {item.Value}");
            }

            int res = 0;
            try
            {
                res = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur de saisie, veuillez entrer un chiffre valide.");
            }

            return res;
        }

        public static bool Buy()
        {
            float money = 0f;
            Console.WriteLine("Prix de votre selection, 0.50€, veuillez entrez la monnaie : ");
            while(money < 0.50f)
            {
                try
                {
                    money = money + float.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erreur d'entrée, veuillez entrez de la monnaie." + e.Message);
                }

                if(money < 0.50f)
                {
                    Console.WriteLine($"Il reste {(0.50f - money)} à payer.");
                }
            }

            if(money > 0.50f)
            {
                Console.WriteLine($"Merci, nous vous rendons {(money - 0.50f)}");
            }

            return true;
        }
    }
}