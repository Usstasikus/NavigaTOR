namespace FourSquareAPI.Entities
{
    public class Mayorship : FourSquareEntity
    {
        public string Type { get; set; }
        
        public string Checkins { get; set; }

        public User User { get; set; }
    }
}