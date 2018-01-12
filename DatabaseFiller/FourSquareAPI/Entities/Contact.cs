namespace FourSquareAPI.Entities
{
    public class Contact : FourSquareEntity
    {
        public string Phone { get; set; }

        public string FormattedPhone { get; set; }

        public string Email { get; set; }

        public string Twitter { get; set; }

        public string Facebook { get; set; }

        public string TwitterSource { get; set; }

        public string Fbid { get; set; }

        public string Name { get; set; }
    }
}