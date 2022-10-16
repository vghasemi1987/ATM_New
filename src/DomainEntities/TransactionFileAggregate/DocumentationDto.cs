namespace DomainEntities.TransactionFileAggregate
{
    public class DocumentationDto
    {
        public int Id { get; set; }
        public int BranchCode { get; set; }
        public string BranchName { get; set; }
        public int AccountNo { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }
        public bool? IsDocumentPrinted { get; set; }
        public string TransactionNumber { get; set; }
    }
}
