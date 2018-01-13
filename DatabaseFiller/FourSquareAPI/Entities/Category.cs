﻿using System.Collections.Generic;

namespace FourSquareAPI.Entities
{
    public class Category : FourSquareEntity
    {
        /// <summary>
        /// A unique identifier for this category.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Pluralized version of the category name.
        /// </summary>
        public string PluralName { get; set; }

        /// <summary>
        /// Shorter version of the category name.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Pieces needed to construct category icons at various sizes. 
        /// Combine prefix with a size (32, 44, 64, and 88 are available) and suffix, e.g. https://foursquare.com/img/categories/food/default_64.png. 
        /// To get an image with a gray background, use bg_ before the size, e.g. https://foursquare.com/img/categories_v2/food/icecream_bg_32.png.
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// If this is the primary category for parent venue object.
        /// </summary>
        public List<string> Parents { get; set; }

        /// <summary>
        /// If this is the primary category for parent venue object.
        /// </summary>
        public bool Primary { get; set; }

        /// <summary>
        /// (only present in venues/categories response). A list of categories that are descendants of this category in the hierarchy.
        /// </summary>
        public List<Category> Categories { get; set; }
    }
}