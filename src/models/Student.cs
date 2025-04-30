using System.Text.Json.Serialization;

namespace code_eduspace_api.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public ICollection<Enrollment> Enrollments { get; set; }

        public bool IsOfLegalAge()
        {
            return (DateTime.Today.Year - BirthDate.Year) >= 18;
        }
    }
}
