using DomainEntities.Commons;

namespace DomainEntities.TransactionFileDetailAggregate
{
    public class Type : Entity<short>
    {
        public string Title { get; set; }
        public string Extension { get; set; }
        public string Content { get; set; }
        public string Separation { get; set; }
    }
}