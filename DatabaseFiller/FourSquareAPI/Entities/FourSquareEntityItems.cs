using System;
using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class FourSquareEntityItems<T> : FourSquareEntity where T : FourSquareEntity
    {
        public string Type { get; set; }

        public string Name { get; set; }

        public Int64 Count { get; set; }

        public List<T> Items { get; set; }
    }
}