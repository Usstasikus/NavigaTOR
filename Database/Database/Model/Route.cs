using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Route
    {
        public Route()
        {
            Feedbacks = new HashSet<Feedback>();
            RoutePlaces = new HashSet<RoutePlace>();
        }

        public int Id { get; private set; }
        public double Rating { get; set; }
        public string Feedback { get; set; }
        public string Access { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<RoutePlace> RoutePlaces { get; set; }
    }
}
