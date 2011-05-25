namespace Prowlin
{
    public abstract class ResultBase
    {
        public virtual string ResultCode { get; set; }
        public virtual int RemainingMessageCount { get; set; }
        public virtual string TimeStamp { get; set; }
        public virtual string ErrorMessage { get; set; }
    }
}