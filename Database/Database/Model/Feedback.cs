using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Feedback
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int Rating { get; set; }
        public int? RouteId { get; set; }
        public int? PlaceId { get; set; }
        public string Text { get; set; }

        public Place Place { get; set; }
        public Route Route { get; set; }
    }
}
