using System;
using System.Collections.Generic;

namespace Database
{
    /// <summary>
    /// Class for Route entity
    /// </summary>
    public partial class Route
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Route()
        {
            Feedbacks = new HashSet<Feedback>();
            RoutePlaces = new HashSet<RoutePlace>();
        }

        /// <summary>
        /// Route ID (Do not fill!)
        /// </summary>
        public int Id { get; private set; }
        /// <summary>
        /// Route rating (Required)
        /// </summary>
        public double Rating { get; set; }
        /// <summary>
        /// Is route public/private
        /// </summary>
        public string Access { get; set; }
        /// <summary>
        /// Time of route creation (Required)
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Route description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Route tags
        /// </summary>
        public string Tags { get; set; }
        /// <summary>
        /// ID of creator user (Required)
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// User reference
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Collection of feedbacks on route
        /// </summary>
        public ICollection<Feedback> Feedbacks { get; set; }
        /// <summary>
        /// Collection of places connections
        /// </summary>
        public ICollection<RoutePlace> RoutePlaces { get; set; }
    }
}
