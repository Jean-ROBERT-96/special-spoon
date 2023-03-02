namespace ChatClient.Model.Business.Security
{
    public interface ISecurity
    {
        string Encrypt(string message);
        string Decrypt(string message);
        string GetName();
    }
}
