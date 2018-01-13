using System;

namespace FourSquareAPI.Entities
{
    public class Stat : FourSquareEntity
    {
        /// <summary>
        /// Total checkins ever here.
        /// </summary>
        public Int64 CheckinsCount { get; set; }

        /// <summary>
        /// Total users who have ever checked in here.
        /// </summary>
        public Int64 UsersCount { get; set; }

        /// <summary>
        /// Number of tips here.
        /// </summary>
        public Int64 TipCount { get; set; }
    }
}