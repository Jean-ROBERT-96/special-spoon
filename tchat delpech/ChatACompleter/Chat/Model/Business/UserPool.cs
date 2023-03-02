using ChatServer.Model.Business;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Chat.Model.Business
{
    public class UserPool
    {
        public List<User> UserList { get; set; }
        private static UserPool instance = null;
        private UserPool()
        {
            // INSTANCE ???
        }

        public static UserPool Instance
        {
            get
            {
                if (instance == null)
                {
                    // ???
                }
                return instance;
            }
        }

        public int CountUser()
        {
            return UserList.Count;
        }

        public void AddUser(User user)
        {
            UserList.Add(user);
        }

        public void RemoveUserByLogin(string login)
        {
            var user = UserList.Where(u => u.login == login).FirstOrDefault();
            if (user != null)
            {
                // REMOVE USER ???
            }
        }

        public bool UserExist(string login)
        {
            // LINQ ???
            return ;
        }

        public void SendBack(byte[] message)
        {
            foreach (User user in UserList)
            {
                NetworkStream userStream = user.client.GetStream();
                userStream.Write(message, 0, message.Length);
            }
        }
    }
}
