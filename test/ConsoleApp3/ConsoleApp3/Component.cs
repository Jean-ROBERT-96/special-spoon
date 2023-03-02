using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    public abstract class Component
    {
        public Composite composite;

        public abstract void Operation();
    }
}
