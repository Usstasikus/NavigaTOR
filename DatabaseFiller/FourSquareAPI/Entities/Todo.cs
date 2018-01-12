namespace FourSquareAPI.Entities
{
    public class Todo : FourSquareEntity
    {
        public string Id { get; set; }

        public string CreatedAt { get; set; }

        public string Tip { get; set; }
    }
}