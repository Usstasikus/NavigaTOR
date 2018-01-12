namespace FourSquareAPI.Entities
{
    public class Icon : FourSquareEntity
    {
        /// <summary>
        /// url of the image withouth the suffix
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Suffix of the image, example .png .ico etc
        /// </summary> 
        public string Suffix { get; set; }

    }
}
