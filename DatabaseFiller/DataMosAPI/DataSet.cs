using System;
namespace DataMosAPI
{
    public class DataSet
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryCaption { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentCaption { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public bool ContainsGeodata { get; set; }
        public double VersionNumber { get; set; }
        public string VersionDate { get; set; }
        public int ItemsCount { get; set; }
    }
}
