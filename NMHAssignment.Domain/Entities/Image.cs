namespace NMHAssignment.Domain.Entities
{
    public class Image
    {
        public long Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Author Author { get; set; } = new Author();
        public long AuthorId { get; set; }
    }
}
