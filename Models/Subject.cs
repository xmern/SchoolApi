namespace SchoolApi.Models
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal PassingGrade { get; set; }
        public decimal MaxGrade { get; set; }
    }
}
