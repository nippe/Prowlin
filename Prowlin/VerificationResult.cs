namespace Prowlin
{
    public class VerificationResult
    {
        public string ResultCode { get; set; }
        public int RemainingMessageCount { get; set; }
        public string TimeStamp { get; set; }
        public string ErrorMessage { get; set; }
    }
}