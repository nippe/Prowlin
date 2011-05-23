namespace Prowlin
{

    /*
     <?xml version="1.0" encoding="UTF-8"?>
<prowl>
	<success code="200" remaining="REMAINING" resetdate="UNIX_TIMESTAMP" />
	<retrieve token="TOKEN" url="URL" />
</prowl>
     */
    public class RetrieveTokenResult
    {
        public string ResultCode { get; set; }
        public int RemainingMessageCount { get; set; }
        public string TimeStamp { get; set; }
        public string Token { get; set; }
        public string Url { get; set; }
    }
}