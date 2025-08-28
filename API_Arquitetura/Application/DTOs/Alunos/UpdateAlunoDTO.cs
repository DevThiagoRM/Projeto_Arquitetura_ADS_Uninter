namespace API_Arquitetura.Application.DTOs.Alunos
{
    public class UpdateAlunoDTO
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
