using System;
using System.Collections.Generic;

namespace NMHAssignment.Domain.Entities
{
    public class Site
    {
        public long Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public ICollection<Article> Article { get; set; } = new List<Article>();
    }
}
