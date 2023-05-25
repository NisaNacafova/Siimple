namespace Siimple.Services.Abstracts
{
    public interface IEmailConfirm
    {
        public void SendMessage(string message,string subject,string to);
    }
}
