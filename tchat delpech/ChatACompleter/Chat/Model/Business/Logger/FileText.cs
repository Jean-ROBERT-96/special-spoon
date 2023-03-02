using ChatServer.Model.Business;
using System;
using System.IO;

namespace Chat.Model.Business.Logger
{
    public class FileText : ILogger
    {
        private User user;
        private string path = DateTime.Now.ToString("yyyyMMdd") + "-chat-log.txt";

        public FileText(User user)
        {
            this.user = user;
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        public void Update()
        {
        }
    }
}
