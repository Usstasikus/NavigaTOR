using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Feedback
    {
        public Feedback() { }
        public Feedback(int userId) => UserId = userId;
        public Feedback(int userId, int rating = 5, string pros = null, string cons = null, string text = null)
        {
            UserId = userId;
            Rating = rating;
            Pros = pros;
            Cons = cons;
            Text = text;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int Rating { get; set; }
        public int? RouteId { get; set; }
        public int? PlaceId { get; set; }
        public string Text { get; set; }
        public string Pros { get; set; }
        public string Cons { get; set; }

        public Place Place { get; set; }
        public Route Route { get; set; }
    }
}
