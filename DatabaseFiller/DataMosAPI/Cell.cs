using System;
namespace DataMosAPI
{
    public class Cell
    {

        public int global_id { get; set; }
        public string ObjectName { get; set; }
        public string WebSite { get; set; }
        public string District { get; set; }
        public string Address { get; set; }
        public string PublicPhone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string AdmArea { get; set; }
        public GeoData<String> geoData { get; set; }
    }
}
