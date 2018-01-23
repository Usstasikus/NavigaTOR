using System;
using System.Collections.Generic;

namespace DataMosAPI
{
    public class GeneralCell<GeoDataClass, PhoneClass, EmailClass>
    {
        public GeneralCell()
        {
            geoData = new GeoData<GeoDataClass>();
        }
        public int global_id { get; set; }

        public string ObjectName { get; set; }
        public string Name { get; set; }
        public string NameWinter { get; set; }
        public string CommonName { get; set; }
        public string FullName { get; set; }
        public string NameOfReligiousOrganization { get; set; }
        public string EnsembleNameOnDoc { get; set; }

        public string FinalName
        {
            get
            {
                if (Name != null)
                    return Name;
                else if (ObjectName != null)
                    return ObjectName;

                else if (NameWinter != null)
                    return NameWinter;

                else if (CommonName != null)
                    return CommonName;

                else if (NameOfReligiousOrganization != null)
                    return NameOfReligiousOrganization;

                else if (EnsembleNameOnDoc != null)
                    return EnsembleNameOnDoc;

                else
                    return FullName;
            }
        }

        public List<WorkHours> WorkingHours { get; set; }

        public string Description { get; set; }

        public string WebSite { get; set; }

        public string Address { get; set; }

        //public string District { get; set; }
        public /*string*/PhoneClass PublicPhone { get; set; }
        //public string Fax { get; set; }
        public /*string*/ EmailClass /*List<EmailBox>*/ Email { get; set; }
        //public string AdmArea { get; set; }
        public string EntranceAdditionalInformation { get; set; }
        public GeoData<GeoDataClass> geoData { get; set; }

        public string Keywords { get; set; }
    }
}
