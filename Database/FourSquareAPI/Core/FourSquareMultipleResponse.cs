using System.Collections.Generic;
using FourSquareAPI.Entities;

namespace FourSquareAPI.Core
{
    public class FourSquareMultipleResponse<T> : FourSquareResponse where T : FourSquareEntity
    {
        public Dictionary<string, List<T>> response
        {
            get;
            set;
        }
    }
}