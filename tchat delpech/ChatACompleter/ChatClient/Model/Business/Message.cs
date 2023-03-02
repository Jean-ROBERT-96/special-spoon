using System.Windows.Media;

namespace ChatClient.Model.Business
{
    public class Message
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public Brush Color { get; set; }
    }
}
