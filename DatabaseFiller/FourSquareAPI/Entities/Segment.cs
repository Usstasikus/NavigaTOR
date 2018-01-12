namespace FourSquareAPI.Entities
{
    public class Segment : FourSquareEntity
    {
        /// <summary>
        /// The name of the named segment.
        /// </summary>
        public string Lable { get; set; }

        /// <summary>
        /// The time as HHMM (24hr) at which the segment begins.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// The time as HHMM (24hr) at which the segment ends.
        /// </summary>
        public string End { get; set; }
    }
}