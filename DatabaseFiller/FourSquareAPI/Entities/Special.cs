using System;
using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class Special : FourSquareEntity
    {
        /// <summary>
        /// A unique identifier for this special.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// One of mayor, count, frequency, regular, friends, swarm, flash or other.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// A message describing the special.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// A description of how to unlock the special.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///  The specific rules for this special.
        /// </summary>
        public string FinePrint { get; set; }

        /// <summary>
        ///  True or false indicating if this special is unlocked for the acting user.
        /// </summary>
        public bool Unlocked { get; set; }

        /// <summary>
        ///  The name of the icon to use: http://foursquare.com/img/specials/icon.png
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The header text describing the type of special.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///	Possible values:
        /// unlocked -> the special is unlocked (all types)
        /// before start -> the time after which the special may be unlocked is in the future (flash specials)
        /// in progress -> the special is locked but could be unlocked if you check in (flash specials), or the special is locked but could be unlocked if enough of your friends check in (friends specials)
        /// taken -> the maximum number of people have unlocked the special for the day (flash and swarm specials)
        /// locked -> the special is locked (all other types)
        /// </summary>
        public string State { get; set; }

        /// <summary>
        ///	A description of how close you are to unlocking the special. Either the number of people who have already unlocked the special (flash and swarm specials), or the number of your friends who have already checked in (friends specials) 
        /// </summary>

        public string Progress { get; set; }

        /// <summary>
        ///	A label describing what the number in the progress field means.
        /// </summary>

        public string ProgressDescription { get; set; }

        /// <summary>
        /// Minutes remaining until the special can be unlocked (flash special only).
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// A number indicating the maximum (swarm, flash) or minimum (friends) number of people allowed to unlock the special.
        /// </summary>
        public Int64 Target { get; set; }

        /// <summary>
        /// A list of friends currently checked in, as compact user objects (friends special only).
        /// </summary>
        public List<User> FriendsHere { get; set; }
    }
}