using System;
using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class Venue : FourSquareEntity
    {
        /// <summary>
        /// A unique string identifier for this venue.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The best known name for this venue.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An object containing none, some, or all of twitter, phone, and formattedPhone. All are strings.
        /// </summary>
        public Contact Contact { get; set; }

        /// <summary>
        ///  An object containing none, some, or all of address (street address), formattedAddress, crossStreet, city, state, postalCode, country, lat, lng, and distance. All fields are strings, except for lat, lng, and distance. Distance is measured in meters. 
        ///  Some venues have their locations intentionally hidden for privacy reasons (such as private residences). If this is the case, the parameter isFuzzed will be set to true, and the lat/lng parameters will have reduced precision. 
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// An array, possibly empty, of categories that have been applied to this venue. One of the categories will have a field primary indicating that it is the primary category for the venue. For the complete set of categories, see venues/categories. 
        /// </summary>
        public List<Category> Categories { get; set; }

        /// <summary>
        /// Boolean indicating whether the owner of this business has claimed it and verified the information.
        /// </summary>
        public bool Verified { get; set; }

        public bool Restricted { get; set; }

        /// <summary>
        /// Contains checkinsCount (total checkins ever here) and usersCount (total users who have ever checked in here). 
        /// </summary>
        public Stat Stats { get; set; }

        /// <summary>
        ///  URL of the venue's website, typically provided by the venue manager. 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///  Contains the hours during the week that the venue is open along with any named hours segments in a human-readable format. For machine readable hours see venues/hours
        /// </summary>
        public Hour Hours { get; set; }

        /// <summary>
        /// Optional Contains the hours during the week when people usually go to the venue. For machine readable hours see venues/hours. 
        /// </summary>
        public Hour Popular { get; set; }

        /// <summary>
        ///  An object containing url and mobileUrl that display the menu information for this venue. 
        /// </summary>
        public Menu Menu { get; set; }

        /// <summary>
        ///  An object containing the price tier from 1 (least pricey) - 4 (most pricey) and a message describing the price tier. 
        /// </summary>
        public Price Price { get; set; }

        /// <summary>
        /// If v >= 20120121, a dictionary containing count and items array of specials at this venue. Otherwise, an array, possibly empty, of specials at this venue. 
        /// </summary>
        public FourSquareEntityItems<Special> Specials { get; set; }

        /// <summary>
        /// Optional. Information about who is here now. If present, there is always a count, the number of people here. If viewing details and there is a logged-in user, there is also a groups field with friends and others as types. 
        /// </summary>
        public FourSquareEntityGroups<User> HereNow { get; set; }

        /// <summary>
        /// The manager's internal identifier for the venue. 
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// Optional. Description of the venue provided by venue owner. 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Seconds since epoch when the venue was created.  
        /// </summary>
        public Int64 CreatedAt { get; set; }

        /// <summary>
        /// user is the compact user who is the mayor (absent if there is no mayor), and count is the number of times they have checked in within the last 60 days.
        /// </summary>
        public FourSquareEntityUsers Mayor { get; set; }

        /// <summary>
        /// Contains the total count of tips and groups with friends and others as groupTypes. Groups may change over time.
        /// </summary>
        public FourSquareEntityGroups<Tip> Tips { get; set; }

        /// <summary>
        /// A grouped response of lists that contain this venue. Contains a summary string representing the acting user's relationship to these lists. If an acting user is present, groups may include todos, created, edited, followed, friends, and others. If this venue is on the acting user's todo list, those items will be included in the todos group. 
        /// </summary>
        public FourSquareEntityGroups<List> Listed { get; set; }

        /// <summary>
        /// An array of string tags applied to this venue.
        /// </summary>
        public List<string> Tags { get; set; }

        /// <summary>
        ///  Contains count of the number of times the acting user has been here. Absent if there is no acting user.
        /// </summary>
        public FourSquareEntityItems<User> BeenHere { get; set; }

        /// <summary>
        /// A short URL for this venue, e.g. http://4sq.com/Ab123D
        /// </summary>
        public string ShortUrl { get; set; }

        /// <summary>
        /// The canonical URL for this venue, e.g. https://foursquare.com/v/foursquare-hq/4ab7e57cf964a5205f7b20e3
        /// </summary>
        public string CanonicalUrl { get; set; }

        /// <summary>
        /// An array of specials near this venue.
        /// </summary>
        public List<Special> SpecialsNearby { get; set; }

        /// <summary>
        /// A count and groups of photos for this venue. Group types are checkin and venue. Not all items will be present.
        /// </summary>
        public FourSquareEntityGroups<Photo> Photos { get; set; }

        /// <summary>
        /// A count and groups of photos for this venue. Group types are checkin and venue. Not all items will be present.
        /// </summary>
        public FourSquareEntityGroups<User> Likes { get; set; }


        /// <summary>
        /// Indicates if the current user has liked this venue.  
        /// </summary>
        public bool Like { get; set; }

        /// <summary>
        /// Indicates if the current user has disliked this venue. 
        /// </summary>
        public bool Dislike { get; set; }

        /// <summary>
        /// Time zone, e.g. America/New_York 
        /// </summary>
        public string TimeZone { get; set; }

        public double Rating { get; set; }

        /// <summary>
        ///  Present if and only if the current user has at least one assigned role for this venue. The value is a list of all of the current user's assigned roles for this venue. Possible values for each element of the list are manager and employee. Subject to change as additional roles may be defined.
        /// </summary>
        public List<string> Roles { get; set; }

        /// <summary>
        ///  Optional user is the branded page associated with the venue. If the venue is part of a chain, this will be a user object referring to the chain. For venues that are being managed and not part of a chain, this will contain a user object that uniquely refers to this venue. 
        /// </summary>
        // public Int64 page
        // {
        //    get;
        //    set;
        // }
    }
}