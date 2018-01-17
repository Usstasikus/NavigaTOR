using System;
using System.Collections.Generic;
namespace DataMosAPI
{

    public class Category
    {
        public int Id { get; set; }
        public double VersionNumber { get; set; }
        public double ReleaseNumber { get; set; }
        public string Caption { get; set; }
        public int CategoryId { get; set; }
        public int DepartmentId { get; set; }
        public string PublishDate { get; set; }
        public string FullDescription { get; set; }
        public string Keywords { get; set; }
        public bool ContainsGeodata { get; set; }
        public bool ContainsAccEnvData { get; set; }
        public bool IsForeign { get; set; }
        public bool IsSeasonal { get; set; }
        public int Season { get; set; }
        public bool IsArchive { get; set; }
        public bool IsNew { get; set; }
        public string LastUpdateDate { get; set; }
        public string SefUrl { get; set; }
        public string IdentificationNumber { get; set; }
    }

}