using System.Collections.Generic;
using FourSquareAPI.Entities;

namespace FourSquareAPI.Core
{
    public class FourSquareSingleResponse<T> : FourSquareResponse where T : FourSquareEntity
    {
        public Dictionary<string, T> response
        {
            get;
            set;
        }
    }
}