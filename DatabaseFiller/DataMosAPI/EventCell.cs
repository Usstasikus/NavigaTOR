using System;
namespace DataMosAPI
{
    public class EventCell
    {
        public int global_id { get; set; }

        public string EventName { get; set; }
        public string FinalName
        {
            get
            {
                return EventName;
            }
        }

        public string EventDate { get; set; }

        public string Description { get; set; }

        public string StartTime { get; set; }

        public string Address { get; set; }

        public GeoData<String> geoData { get; set; }

        public string Keywords { get; set; }
    }
}
