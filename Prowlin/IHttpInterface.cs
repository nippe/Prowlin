namespace Prowlin
{
    public interface IHttpInterface
    {
        int SendNotification(INotification notification);
        VerificationResult SendVerification(IVerification verification);
    }
}