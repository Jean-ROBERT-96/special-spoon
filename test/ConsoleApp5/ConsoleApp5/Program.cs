namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] livre = new string[50];
            bool exec = true;
            int choix = 0;

            while (exec)
            {
                Console.WriteLine("Bonjour, choisissez une opération :");
                Console.WriteLine("1 : Afficher les livres");
                Console.WriteLine("2 : Insérer les livres");
                Console.WriteLine("3 : Supprimer les livres");
                Console.WriteLine("4 : Mettre à jour un livre");
                Console.WriteLine("5 : Quitter le logiciel");

                try
                {
                    choix = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Veuillez entrer une valeur valide.");
                }
                
                if(choix == 1)
                {
                    int count = 0;
                    try
                    {
                        while (count < livre.Length)
                        {
                            Console.WriteLine($"{count} : {livre[count]}");
                            count++;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Bibliothèque vide");
                    }
                }
                else if(choix == 2)
                {
                    int index = 0;
                    if(livre.Length != null)
                        index = livre.Length;

                    Console.WriteLine("Veuillez entrer un nom de livre :");
                    string name = Console.ReadLine();
                    livre[index] = name;
                    Console.WriteLine($"Livre {name} ajouté.");
                }
            }
        }
    }
}