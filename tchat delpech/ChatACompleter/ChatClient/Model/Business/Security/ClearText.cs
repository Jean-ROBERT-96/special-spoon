namespace ChatClient.Model.Business.Security
{
    public class ClearText : ISecurity
    {
        private const string name = "NONE";
        public string Decrypt(string message)
        {
            return message;
        }

        public string Encrypt(string message)
        {
            return message;
        }

        public string GetName()
        {
            return name;
        }
    }
}
