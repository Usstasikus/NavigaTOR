using System;
using System.Linq;
using FourSquareAPI.Core;
using System.Collections.Generic;
using System.IO;
using FourSquareAPI.Entities;

namespace Forsquare
{
    class Program
    {
        private static Rect Moscow = Rect.GetRect(37.324053, 55.492018, 37.966067, 55.957060);
        //Примерно 500 метров
        private const double radius1 = 0.0045;
        //Примерно 1000 метров
        private const double radius = 0.009;
        private const string clientId = "BWVOT3CVKFPWHYN3VE23GIYE0KAMMC1HU3WZVLJEU1L0OKYB";
        private const string clientSecret = "0LRAHA4ND2DSQA0OL3SKLTGNX2NTOBL2S3D52A01SK4MBCM4";
        static void Main(string[] args)
        {
            FourSquare f = new FourSquare(clientId, clientSecret);
            var r = (radius * 111_000).ToString();
            foreach (var point in new RectangleParser(Moscow, radius))
            {
                foreach (var category in f.GetVenueCategories())
                {
                    var s = string.Format("{0},{1}", point.Y, point.X);
                    var venues = f.SearchVenues(new Dictionary<string, string>
                    {
                        { "ll", s},
                        { "radius", r },
                        { "categoryid", category.id },
                    });
                    var venuesc = venues.Select(x => new { name = x.name, categoryes = x.categories, location = string.Format("{0},{1}", x.location.lat, x.location.lng) });
                }
            }
        }
    }
}
