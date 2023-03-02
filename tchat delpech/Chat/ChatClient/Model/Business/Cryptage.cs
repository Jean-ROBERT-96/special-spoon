using ChatClient.Model.Business.Security;

namespace ChatClient.Model.Business
{
    public class Cryptage
    {
        ISecurity _strategy;
        public ISecurity Strategy { set { _strategy = value; } }

        public Cryptage()
        {
            Strategy = new ClearText();
        }

        public void SetStrategy(string strategySecurity)
        {
            switch (strategySecurity)
            {
                case "CYPHER":
                    Strategy = new Base64Cypher();
                    break;
                default:
                    Strategy = new ClearText();
                    break;
            }
        }

        public string GetStrategyName()
        {
            return _strategy.GetName();
        }

        public string Encrypt(string message)
        {
            return _strategy.Encrypt(message);
        }

        public string Decrypt(string message)
        {
            return _strategy.Decrypt(message);
        }
    }
}
