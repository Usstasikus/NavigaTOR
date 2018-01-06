using System;
using System.Collections.Generic;

namespace Database
{
    /// <summary>
    /// Class for Feedback entity
    /// </summary>
    public partial class Feedback
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public Feedback() { }
        /// <summary>
        /// General constructor by user id with default values
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="rating"></param>
        /// <param name="pros"></param>
        /// <param name="cons"></param>
        /// <param name="text"></param>
        public Feedback(int userId, int rating = 5, string pros = null, string cons = null, string text = null)
        {
            UserId = userId;
            Rating = rating;
            Pros = pros;
            Cons = cons;
            Text = text;
        }

        /// <summary>
        /// Feedback ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User ID who commited a feedback
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Time when feedback commited
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Rating for place/route
        /// </summary>
        public int Rating { get; set; }
        /// <summary>
        /// Route ID. If null, it is a place feedback
        /// </summary>
        public int? RouteId { get; set; }
        /// <summary>
        /// Place ID. If null, it is a route feedback
        /// </summary>
        public int? PlaceId { get; set; }
        /// <summary>
        /// General comment
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Pros of place/route
        /// </summary>
        public string Pros { get; set; }
        /// <summary>
        /// Cons of place/route
        /// </summary>
        public string Cons { get; set; }

        /// <summary>
        /// Place reference
        /// </summary>
        public Place Place { get; set; }
        /// <summary>
        /// Route reference
        /// </summary>
        public Route Route { get; set; }
    }
}
