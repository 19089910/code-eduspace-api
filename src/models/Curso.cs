using System.Text.Json.Serialization;

namespace code_eduspace_api.Models
{

    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        
        [JsonIgnore]
        public ICollection<Matricula> Matriculas { get; set; }
    }
}