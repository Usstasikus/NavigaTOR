using System;

namespace FourSquareAPI.Entities
{
    public class FourSquareEntityUsers : FourSquareEntity
    {
        public Int64 Count { get; set; }

        public User User { get; set; }
    }
}
