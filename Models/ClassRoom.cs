namespace SchoolApi.Models
{
    public class ClassRoom
    {
        public Guid Id { get; set; }
        public Guid ClassId { get; set; }
        public required Class Class { get; set; }
        public required string Name { get; set; }
        public required Guid TeacherId {  get; set; }

    }
}
