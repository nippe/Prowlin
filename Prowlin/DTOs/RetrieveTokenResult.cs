namespace Prowlin
{

    /*
     <?xml version="1.0" encoding="UTF-8"?>
<prowl>
	<success code="200" remaining="REMAINING" resetdate="UNIX_TIMESTAMP" />
	<retrieve token="TOKEN" url="URL" />
</prowl>
     */
    public class RetrieveTokenResult : ResultBase
    {
       public string Token { get; set; }
        public string Url { get; set; }
    }
}