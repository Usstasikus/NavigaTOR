using FourSquareAPI.Entities;

namespace FourSquareAPI.Core
{
    public class FourSquareMultipleVenuesResponse<T> : FourSquareResponse where T : FourSquareEntity
    {
        public VenueResponse<T> response
        {
            get;
            set;
        }
    }
}