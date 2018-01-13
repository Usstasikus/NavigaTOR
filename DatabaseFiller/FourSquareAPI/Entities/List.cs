using System;

namespace FourSquareAPI.Entities
{
    public class List : FourSquareEntity
    {
        /// <summary>
        /// A unique string identifier for this photo.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The user-entered name for this list.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user-entered list description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The compact user who created this list.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Boolean indicating whether the acting user is following this list. Absent if there is no acting user.
        /// </summary>
        public bool Following { get; set; }

        /// <summary>
        /// Count and items of users who follow this list. All items may not be present.
        /// </summary>
        public FourSquareEntityItems<User> Followers { get; set; }

        /// <summary>
        /// Boolean indicating whether the acting user can edit this list. Absent if there is no acting user.
        /// </summary>
        public bool Editable { get; set; }

        /// <summary>
        /// Boolean indicating whether this list is editable by the owner's friends.
        /// </summary>
        public bool Collaborative { get; set; }

        /// <summary>
        ///  Count and items of users who have edited this list. All items may not be present.
        /// </summary>
        public FourSquareEntityItems<User> Collaborators { get; set; }

        /// <summary>
        /// The canonical URL for this list, e.g. https://foursquare.com/dens/list/a-brief-history-of-foursquare
        /// </summary>
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// A photo for this list.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// The number of unique venues on the list.
        /// </summary>
        public Int64 VenueCount { get; set; }
        /// <summary>
        /// The number of venues on the list visited by the acting user. Absent if there is no acting user.
        /// </summary>
        public Int64 VisitedCount { get; set; }

        /// <summary>
        /// Count and items of list items on this list.
        /// </summary>
        public FourSquareEntityItems<Item> ListItems { get; set; }

    }
}
