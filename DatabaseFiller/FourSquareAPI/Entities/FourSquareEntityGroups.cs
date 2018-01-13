using System;
using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class FourSquareEntityGroups<T> : FourSquareEntity where T : FourSquareEntity
    {
        public Int64 Count { get; set; }

        public string Summary { get; set; }

        public List<FourSquareEntityItems<T>> Groups { get; set; }
    }
}