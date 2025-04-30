using System.Text.Json.Serialization;

namespace code_eduspace_api.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        
        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
