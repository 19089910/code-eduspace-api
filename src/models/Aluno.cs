namespace code_eduspace_api.Models
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }

        public ICollection<Matricula> Matriculas { get; set; }

        public bool EhMaiorDeIdade()
        {
            return (DateTime.Today.Year - DataNascimento.Year) >= 18;
        }
    }
}