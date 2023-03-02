using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Server
{
    public class UserPool
    {
        private static UserPool _instance;
        private List<User> _users;

        private UserPool()
        {

        }

        public static UserPool GetInstance()
        {
            if(_instance == null)
                _instance = new UserPool();
            
            return _instance;
        }

        public int UserCount()
        {
            return _users.Count;
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void RemoveUserByLogin(int id)
        {
            _users.RemoveAll(x => x.Id == id);
        }

        public bool UserExist(int id)
        {
            if (_users.Any(x => x.Id == id))
                return true;

            return false;
        }

        public void Broadcast(Communication communication)
        {

        }
    }
}
