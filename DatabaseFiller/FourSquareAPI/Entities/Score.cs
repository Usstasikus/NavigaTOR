namespace FourSquareAPI.Entities
{
    public class Score : FourSquareEntity
    {
        public string Recent { get; set; }

        public string Max { get; set; }

        public string Goal { get; set; }

        public string CheckinsCount { get; set; }
    }
}