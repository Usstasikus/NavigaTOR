﻿using System;
using System.Collections.Generic;

namespace Database
{
    public partial class RoutePlace
    {
        public int RouteId { get; set; }
        public int PlaceId { get; set; }

        public Place Place { get; set; }
        public Route Route { get; set; }
    }
}
