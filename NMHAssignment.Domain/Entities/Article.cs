using System.Collections.Generic;

namespace NMHAssignment.Domain.Entities
{
    public class Article
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public virtual ICollection<Author> Author { get; set; } = new List<Author>();
        public virtual Site Site { get; set; } = new Site();
        public long SiteId { get; set; }
    }
}
