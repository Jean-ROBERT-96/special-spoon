using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public class Composite : Component
    {
        List<Component?> component;

        public override void Operation()
        {
            
        }

        public void Add(Component component)
        {
            this.component.Add(component);
        }

        public void Remove(Component component)
        {
            this.component.Remove(component);
        }

        public List<Component?> GetChild()
        {
            return component;
        }
    }
}
