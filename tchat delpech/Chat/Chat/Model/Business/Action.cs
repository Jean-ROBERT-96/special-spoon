namespace Chat.Model.Business
{
    public enum ActionType { SendMessage, Connection, Answer }

    public class UserAction
    {
        public ActionType Type { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
    }
}