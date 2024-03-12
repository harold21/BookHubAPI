namespace BookHub.Core.Entities
{
    public class Book
    {
        public required int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}