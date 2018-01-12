using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class Hour : FourSquareEntity
    {
        public string Status { get; set; }

        public bool IsOpen { get; set; }

        public List<TimeFrame> Timeframes { get; set; }

        public List<Segment> Segments { get; set; }
    }
}