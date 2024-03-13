namespace BookHub.Core.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
    }
}