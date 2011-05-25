namespace Prowlin.Interfaces
{
    public interface IHttpInterface
    {
        NotificationResult SendNotification(INotification notification);
        VerificationResult SendVerification(IVerification verification);
        RetrieveTokenResult RetrieveToken(RetrieveToken retrieveToken);
        RetrieveApikeyResult RetrieveApikey(RetrieveApikey retrieveApikey);
    }
}