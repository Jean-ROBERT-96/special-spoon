namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Humain test1 = new Humain("Robert", "Jean", 'M');
            Humain test2 = new Humain("Robert", "Jeanne", 'M');
            Humain test3 = new Humain("Robert", "Jeannette", 'M');
            Humain test4 = new Humain("Robert", "François", 'M');
            Humain test5 = new Humain("Robert", "Julien", 'M');
            Humain test6 = new Humain("Robert", "Stéphanie", 'M');
            Humain test7 = new Humain("Robert", "Anthony", 'M');
            Humain test8 = new Humain("Robert", "Bastien", 'M');

            List<Humain> testList1 = new List<Humain>();
            List<Humain> testList2 = new List<Humain>();

            testList1.Add(test1);
            testList1.Add(test2);
            testList1.Add(test3);
            testList1.Add(test4);
            testList1.Add(test5);
            testList1.Add(test6);
            testList1.Add(test7);
            testList1.Add(test8);

            testList2 = testList1.Where(a => a.prenom.Contains("je", StringComparison.OrdinalIgnoreCase)).ToList();

            foreach(Humain humain in testList2)
            {
                Console.WriteLine(humain.prenom);
            }
            Console.WriteLine("Fin de programme");
        }
    }
}
