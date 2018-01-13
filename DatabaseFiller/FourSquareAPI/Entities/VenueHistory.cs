namespace FourSquareAPI.Entities
{
    public class VenueHistory : FourSquareEntity
    {
        public string BeenHere { get; set; }

        public string LastHereAt { get; set; }

        public Venue Venue { get; set; }
    }
}