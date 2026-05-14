
namespace SecurityCheckStatusLoader
{
    public class XlsxRecordPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class XlsxRecord
    {
        public Guid Guid { get; set; }
        public string Link { get; set; }
        public DateTime? CreatedAt { get; set; }
        public XlsxRecordPerson Author { get; set; }
        public string CreationTemplate { get; set; }
        public XlsxRecordPerson CurrendVersionAuthor { get; set; }
        public DateTime? CurrentVersionCreatedAt { get; set; }
        public Guid ContractorsGuid { get; set; }
        public string Correspondent { get; set; }
    }
}
