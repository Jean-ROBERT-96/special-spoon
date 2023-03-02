namespace WebApplication1.Models
{
    public class TestViewModel
    {
        public string nom { get; init; }
        public string prenom { get; init; }
        public int age { get; init; }

        public TestViewModel(string nom, string prenom, int age)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.age = age;
        }
    }
}
