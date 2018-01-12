using System.Collections.Generic;

namespace Database
{
    /// <summary>
    /// Class for User entity
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public User()
        {
            Routes = new HashSet<Route>();
            UserPlaces = new HashSet<UserPlace>();
        }
        
        /// <summary>
        /// User ID (Do not fill!)
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// User login (Required)
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// User password (Required)
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User last name
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// User age
        /// </summary>
        public int? Age { get; set; }
        /// <summary>
        /// User favourite tags
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// User favourite routes. Not used
        /// </summary>
        public ICollection<Route> Routes { get; set; }
        /// <summary>
        /// User favourite places. Not used
        /// </summary>
        public ICollection<UserPlace> UserPlaces { get; set; }
    }
}
