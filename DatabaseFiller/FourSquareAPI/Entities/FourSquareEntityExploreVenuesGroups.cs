using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class FourSquareEntityExoploreVenuesGroups<T> : FourSquareEntity where T : FourSquareEntity
    {
        public int SuggestedRadius { get; set; }

        public string HeaderLocation { get; set; }

        public string HeaderFullLocation { get; set; }

        public string HeaderLocationGranularity { get; set; }

        public int TotalResults { get; set; }

        public List<FourSquareEntityItems<T>> Groups { get; set; }
    }
}