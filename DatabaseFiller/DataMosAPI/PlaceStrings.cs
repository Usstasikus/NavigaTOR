using System;
namespace DataMosAPI
{
    public class PlaceStrings<GeoDataClass, PhoneClass, EmailClass>
    {
        //public int categoryID;
        //public PlaceStrings(int id){
        //    categoryID = id;
        //}
        public int global_id { get; set; }
        public int Number { get; set; }
        public GeneralCell<GeoDataClass, PhoneClass, EmailClass> Cells { get; set; }
    }
}
