namespace FourSquareAPI.Entities
{
    public class VenueExplore : FourSquareEntity
    {
        public FourSquareEntityItems<Reasons> Reasons { get; set; }

        public Venue Venue { get; set; }
    }
}