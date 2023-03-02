using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace TestRecursiv
{
    public class Souche : INode
    {
        public IList<INode> Children { get; } = new ObservableCollection<INode>();

        public bool IsFeuille() => false;
    }
    public class Racine : INode
    {
        public IList<INode> Children { get; } = new ObservableCollection<INode>();
        public bool IsFeuille() => false;
    }
    public class GrosseBranche : INode
    {
        public IList<INode> Children { get; } = new ObservableCollection<INode>();
        public bool IsFeuille() => false;
    }

    public class Branche : INode
    {
        public IList<INode> Children { get; } = new ObservableCollection<INode>();
        public bool IsFeuille() => false;
    }

    public class Feuille : INode
    {
        public IList<INode> Children { get; } = new ObservableCollection<INode>();
        public bool IsFeuille() => true;
    }

    public class Arbre
    {
        Souche root;

        public Arbre()
        {
            root = new Souche();
            root.Children.Add(new Racine());
            root.Children.Add(new Racine());
            root.Children.Add(new Racine());

            var g1 = new GrosseBranche();
            g1.Children.Add(new Branche());
            g1.Children.Add(new Branche());
            g1.Children.Add(new Feuille());

            g1.Children[0].Children.Add(new Feuille());
            g1.Children[0].Children.Add(new Feuille());
            g1.Children[0].Children.Add(new Feuille());

            root.Children.Add(g1);
            root.Children.Add(new Branche());

            root.Children.Last().Children.Add(new Feuille());
            root.Children.Last().Children.Add(new Feuille());
            root.Children.Last().Children.Add(new Feuille());

            var firstFeuille = FindFirstFeuille(root);
            var feulleCount = CountFeuille(root);

            var pred1 = CountSmtg(root, this.IsBranch);
            var lambda = CountSmtg(root, (n) => n is Branche);

            Action<int> a = (i) =>
            {
                for(int ii=0; ii<i;i++)
                    root.Children.Add(new Racine());
            };

        }

        private bool IsBranch(INode n)
        {
            return n is Branche;
        }

        private INode FindFirstFeuille(INode node)
        {
            if (node is Feuille)
                return node;

            foreach(var c in node.Children)
            {
                var result = FindFirstFeuille(c);
                if(result != null) return result;
            }

            return null;
        }

        private int CountFeuille(INode node)
        {
            if (node.IsFeuille())
                return 1;

            int count = 0;
            foreach(var c in node.Children)
            {
                count += CountFeuille(c);
            }

            return count;
        }

        private int CountSmtg(INode node, Predicate<INode> predicate)
        {
            int count = 0;
            if (predicate(node))
                count++;

            foreach(var c in node.Children)
            {
                count += CountSmtg(c, predicate);
            }

            return count;
        }
    }
}
