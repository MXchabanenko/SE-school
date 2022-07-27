namespace WebApplication2.IServices
{
    public interface ISubsriptionService
    {
        void Subscribe(string email);
        void SendEmails();
    }
}
