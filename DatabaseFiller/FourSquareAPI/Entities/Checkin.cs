namespace FourSquareAPI.Entities
{
    public class Checkin : FourSquareEntity
    {
        /// <summary>
        /// A unique identifier for this checkin.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Seconds since epoch when this checkin was created.
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// One of checkin, shout, or venueless.
        /// </summary>
        public string Type { get; set; }

        public string IsMayor { get; set; }

        /// <summary>
        /// optional String representation of the time zone where this check-in occurred.
        /// </summary>
        public string TimeZone { get; set; }

        /// <summary>
        /// optional If the user is not clear from context, then a compact user is present.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// optional If the venue is not clear from context, and this checkin was at a venue, then a compact venue is present.
        /// </summary>
        public Venue Venue { get; set; }

        /// <summary>
        /// optionalIf the type of this checkin is shout or venueless, this field may be present and may contains a lat, lng pair and/or a name, providing unstructured information about the user's current location.
        /// </summary>
        public Location Location { get; set; }

        public Category Categories { get; set; }

        public string Verified { get; set; }

        public Stat Stats { get; set; }

        public FourSquareEntityItems<Todo> Todos { get; set; }

        /// <summary>
        /// optional If present, the name and url for the application that created this checkin.
        /// </summary>
        public Source Source { get; set; }

        public string distance { get; set; }

        /// <summary>
        /// count and items for photos on this checkin. All items may not be present.
        /// </summary>
        public FourSquareEntityItems<Photo> Photos { get; set; }

        /// <summary>
        /// count and items for comments on this checkin. All items may not be present.
        /// </summary>
        public FourSquareEntityItems<Comment> Comments { get; set; }

        /// <summary>
        /// optional Message from check-in, if present and visible to the acting user.
        /// </summary>
        public string Shout { get; set; }
    }
}