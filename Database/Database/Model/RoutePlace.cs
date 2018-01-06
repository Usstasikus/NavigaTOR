using System.ComponentModel;

namespace Database
{
    /// <summary>
    /// Helping class to represent connection of Route and Place
    /// </summary>
    [Browsable(false)]
    public partial class RoutePlace
    {
        /// <summary>
        /// Route ID
        /// </summary>
        public int RouteId { get; set; }
        /// <summary>
        /// Place ID
        /// </summary>
        public int PlaceId { get; set; }

        /// <summary>
        /// Place reference
        /// </summary>
        public Place Place { get; set; }
        /// <summary>
        /// Route reference
        /// </summary>
        public Route Route { get; set; }
    }
}
