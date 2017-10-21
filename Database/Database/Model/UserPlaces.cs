using System;
using System.Collections.Generic;

namespace Database
{
    public partial class UserPlaces
    {
        public int UserId { get; set; }
        public int PlaceId { get; set; }

        public Place Place { get; set; }
        public User User { get; set; }
    }
}
