﻿using System;
using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class Item : FourSquareEntity
    {
        /// <summary>
        /// A unique identifier for this item. Note: item ids are only valid within the context of a list.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The compact user who added this item to the current list. Only present when viewing a specific list and when the user is the not same as the list owner.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// A photo for this item. If no photo has been explicitly set on this item, but the tip contains a photo it will be returned in this field.
        /// </summary>
        public Photo Photo { get; set; }

        /// <summary>
        /// The compact venue this item is for, unless the venue is clear from context.
        /// </summary>
        public Venue Venue { get; set; }

        /// <summary>
        /// If this item contains a tip, then a compact tip is present.
        /// </summary>
        public Tip Tip { get; set; }

        /// <summary>
        /// Text entered by the user when creating this item. This field is private and only returned to the author.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Seconds since epoch when this item was added to the list.
        /// </summary>
        public Int64 CreatedAt { get; set; }

        /// <summary>
        /// Information about what other lists this item appears on. If present, an array of compact lists.
        /// </summary>
        public List<List> Listed { get; set; }
    }
}