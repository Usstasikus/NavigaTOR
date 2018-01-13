namespace FourSquareAPI.Entities
{
    public class Setting : FourSquareEntity
    {
        public string ReceivePings { get; set; }

        public string ReceiveCommentPings { get; set; }

        public string SendToTwitter { get; set; }

        public string SendToFacebook { get; set; }

        public string ForeignConsent { get; set; }
    }
}