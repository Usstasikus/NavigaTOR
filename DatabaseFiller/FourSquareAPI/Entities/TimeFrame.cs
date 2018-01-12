using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class TimeFrame : FourSquareEntity
    {
        /// <summary>
        /// Localized list of day names
        /// </summary>
        public string Days { get; set; }

        /// <summary>
        /// An array of times the venue is open on days within the timeframe.
        /// </summary>
        public bool IncludesToday { get; set; }

        /// <summary>
        /// An array of times the venue is open on days within the timeframe.
        /// </summary>
        public List<Open> Open { get; set; }
    }
}