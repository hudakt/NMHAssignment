namespace NMHAssignment.Domain.Entities
{
    public class Author
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual Image Image { get; set; } = new Image();
    }
}
