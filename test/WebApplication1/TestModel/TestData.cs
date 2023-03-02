using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    public class TestData
    {
        public List<User> users = new List<User>();
        public TestData()
        {
            for(int i = 0; i < 5; i++)
            {
                users.Add(new User(i, "User" + i.ToString(), 25));
            }
        }
    }
}
