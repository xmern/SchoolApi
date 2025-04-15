using SchoolApi.Authentication;

namespace SchoolApi.Models
{
    public class StudentClassRecord
    {
        public Guid Id { get; set; }
        public required Guid ClassId { get; set; }        
        public required Guid StudentId { get; set; }        
        public required Guid ClassRoomId { get; set; }
        public required string ClassRoomName { get; set; }= string.Empty;        
        public bool Completed { get; set; } = false;
        public List<ResultSheet> ResultSheets { get; set; } = new();

    }
}
