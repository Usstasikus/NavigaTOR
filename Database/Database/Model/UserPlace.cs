using System.ComponentModel;

namespace Database
{
    /// <summary>
    /// Helping class to represent connection of User and Place
    /// </summary>
    [Browsable(false)]
    public partial class UserPlace
    {
        /// <summary>
        /// User ID
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Place ID
        /// </summary>
        public int PlaceId { get; set; }

        /// <summary>
        /// Place reference
        /// </summary>
        public Place Place { get; set; }
        /// <summary>
        /// User reference
        /// </summary>
        public User User { get; set; }
    }
}
