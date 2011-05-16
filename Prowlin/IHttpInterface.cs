namespace Prowlin
{
    public interface IHttpInterface
    {
        int SendNotification(INotification notification);
        void SendVerification();
    }
}