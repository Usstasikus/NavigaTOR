using System;
using System.Collections.Generic;

namespace DataMosAPI
{
    public class GeoData<T>
    {
        public GeoData()
        {
            type = "";
            coordinatesField = null;
            coordinates = null;
        }
        public string type { get; set; }
        List<T> coordinatesField;
        public List<T> coordinates
        {
            get
            {
                return coordinatesField;
            }
            set
            {
                try
                {
                    coordinatesField = value;
                }
                catch (Exception)
                {
                    coordinatesField = null;
                }

            }
        }

    }
}
