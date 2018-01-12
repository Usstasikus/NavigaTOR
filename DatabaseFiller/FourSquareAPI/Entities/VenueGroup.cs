namespace FourSquareAPI.Entities
{
    public class VenueGroup : FourSquareEntity
    {
        /// <summary>
        ///  A unique identifier for this venue group.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the venue group.
        /// </summary>
        public string Name { get; set; }

        public FourSquareEntityItems<Venue> Venues { get; set; }
    }
}