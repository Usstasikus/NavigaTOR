using System;
using System.Collections.Generic;

namespace Database
{
    /// <summary>
    /// Class for Place entity
    /// </summary>
    public partial class Place
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Place()
        {
            Feedbacks = new HashSet<Feedback>();
            RoutePlaces = new HashSet<RoutePlace>();
            UserPlaces = new HashSet<UserPlace>();
        }
        /// <summary>
        /// Place ID (Do not fill!)
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Place title (Required)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Place address
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Place coordinates (Required)
        /// </summary>
        public string Coordinates { get; set; }
        /// <summary>
        /// Source of info about place
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Place tags
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// Place description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Contacting info
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        /// Place rating (Required)
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Place limitations
        /// </summary>
        public string Limitations { get; set; }
        /// <summary>
        /// DateTime of adding the place to DB
        /// </summary>
        public DateTime? DateTime { get; set; }

        /// <summary>
        /// Collection of feedbacks
        /// </summary>
        public ICollection<Feedback> Feedbacks { get; set; }
        /// <summary>
        /// Collection of routes connections
        /// </summary>
        public ICollection<RoutePlace> RoutePlaces { get; set; }
        /// <summary>
        /// Collection of users connections
        /// </summary>
        public ICollection<UserPlace> UserPlaces { get; set; }
    }
}
