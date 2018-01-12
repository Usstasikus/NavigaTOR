namespace FourSquareAPI.Entities
{
    public class Open : FourSquareEntity
    {
        /// <summary>
        /// A localized string describing the time the venue us open.
        /// </summary>
        public string RenderedTime { get; set; }
    }
}