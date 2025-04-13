namespace SchoolApi.Models
{
    public class Class
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<ClassRoom> ClassRooms { get; set; } = new();
    }
}
