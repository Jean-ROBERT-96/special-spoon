using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IList<Personne> personnes; 

        public MainWindow()
        {
            personnes = new ObservableCollection<Personne>();

            Init();
            InitializeComponent();

            this.DataContext = this;
        }

        public void Init()
        {
            personnes.Add(new Personne
            {
                Nom = "Jean",
                Prenom = "Robert",
                Age = 26,
                Genre = 'M',
                Telephone = "0769389536",
                Mail = "truc@truc.fr"
            });
            personnes.Add(new Personne
            {
                Nom = "Albi",
                Prenom = "Dautaj",
                Age = 19,
                Genre = 'M',
                Telephone = "0610101010",
                Mail = "truc@overwatch.fr"
            });
            personnes.Add(new Personne
            {
                Nom = "Malika",
                Prenom = "Russie",
                Age = 24,
                Genre = 'F',
                Telephone = "0620202020",
                Mail = "truc@ouf.fr"
            });
            personnes.Add(new Personne
            {
                Nom = "Louis",
                Prenom = "Marriott",
                Age = 25,
                Genre = 'M',
                Telephone = "0630303030",
                Mail = "tonk@truc.fr"
            });
            personnes.Add(new Personne
            {
                Nom = "Aurélie",
                Prenom = "Leroy",
                Age = 256,
                Genre = 'F',
                Telephone = "0000000000",
                Mail = "truc@cesi.fr"
            });
        }
    }
}
