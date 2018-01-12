namespace FourSquareAPI.Entities
{
    public class Link : FourSquareEntity
    {
        /// <summary>
        /// For now just contains an id of a provider, e.g. nymag.
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        /// optional If present, a URL for additional information about this venue from this provider.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// optional If present, the identifer used by this provider to identify this venue.
        /// </summary>
        public string LinkedId { get; set; }
    }
}