namespace SchoolApi.Models
{
    public class SubjectRecord
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public decimal FirstCA { get; set; }
        public decimal SecondCA { get; set; }
        public decimal ExamScore { get; set; }
        public required ResultSheet ResultSheet { get; set; }
        public Guid ResultSheetId { get; set; }



    }
}
