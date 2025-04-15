namespace SchoolApi.Models
{
    public class ResultSheet
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid StudentClassRecordId { get; set; }
        public required StudentClassRecord StudentClassRecord { get; set; }
        public List<SubjectRecord> SubjectRecords { get; set; } = new();
        

    }
}
