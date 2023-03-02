using Base_recursiv.Interfaces;
using Base_recursiv.Models;
using System.Collections.ObjectModel;

namespace Base_recursiv
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tables table = new Tables();

            table.Children.Add(new Tables());
            table.Children.Add(new Tables());
            table.Children.Add(new Tables());
            table.Children.Add(new Tables());
            table.Children.Add(new Tables());

            table.Children[0].Children.Add(new Tables());
            table.Children[0].Children.Add(new Tables());
            table.Children[1].Children.Add(new Tables());
            table.Children[1].Children.Add(new Tables());
            table.Children[2].Children.Add(new Tables());
            table.Children[3].Children.Add(new Tables());
            table.Children[3].Children.Add(new Tables());
            table.Children[3].Children.Add(new Tables());
            table.Children[3].Children.Add(new Tables());

            table.Children[3].Children[0].Children.Add(new Tables());
            table.Children[3].Children[0].Children.Add(new Tables());
            table.Children[3].Children[0].Children.Add(new Tables());
            table.Children[3].Children[1].Children.Add(new Tables());
            table.Children[3].Children[1].Children.Add(new Tables());
            table.Children[3].Children[1].Children.Add(new Tables());

            var counter = TablesCount(table);

            Console.WriteLine(counter);
        }

        static int TablesCount(INode node)
        {
            int count=0;

            Console.WriteLine(node);

            if (node is Tables)
                count++;

            foreach (var c in node.Children)
            {
                count += TablesCount(c);
            }

            return count;
        }
    }
}