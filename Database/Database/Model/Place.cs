﻿using System;
using System.Collections.Generic;

namespace Database
{
    public partial class Place
    {
        public Place()
        {
            Feedbacks = new HashSet<Feedback>();
            RoutePlaces = new HashSet<RoutePlaces>();
            UserPlaces = new HashSet<UserPlaces>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string Coordinates { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public string Description { get; set; }
        public string Contacts { get; set; }
        public double Rating { get; set; }
        public string Limitations { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<RoutePlaces> RoutePlaces { get; set; }
        public ICollection<UserPlaces> UserPlaces { get; set; }
    }
}
