using System;
namespace DataMosAPI
{
    public class DataSetVersion
    {
        public int VersionNumber { get; set; }
        public int ReleaseNumber { get; set; }
        public string Source { get; set; }
        public string Created { get; set; }
        public string Provenance { get; set; }
        public string Valid { get; set; }
        public string Structure { get; set; }
    }
}
