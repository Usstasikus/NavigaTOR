namespace FourSquareAPI.Entities
{
    public class Menu : FourSquareEntity
    {
        /// <summary>
        /// A name for this menu.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A unique string identifier for this menu.
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// More information describing this menu.
        /// </summary>
        public string Description { get; set; }

        public string Type { get; set; }

        public string Label { get; set; }

        public string Anchor { get; set; }

        public string Url { get; set; }

        public string MobileUrl { get; set; }
    }
}