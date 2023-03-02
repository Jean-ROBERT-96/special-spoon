using ChatServer.Model.Business;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Chat.Model.Business
{
    public class UserPool
    {
        public List<User> UserList { get; set; } = new List<User>();
        private static UserPool instance = null;

        private UserPool()
        {
            // UserList = new List<User>();
        }

        public static UserPool Instance
        {
            get
            {
                if (instance == null)
                    instance = new UserPool();
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
                UserList.Remove(user);
            }
        }

        public bool UserExist(string login)
        {
            return UserList.Where(u => u.login == login).FirstOrDefault() == null ? false : true;
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
