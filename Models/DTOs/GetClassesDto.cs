namespace SchoolApi.Models.DTOs
{
    public class GetClassesDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
