using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class Image : FourSquareEntity
    {
        public string Prefix { get; set; }

        public List<string> Sizes { get; set; }

        public string Name { get; set; }
    }
}