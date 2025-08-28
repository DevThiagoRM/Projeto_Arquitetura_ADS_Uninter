using Microsoft.AspNetCore.Identity;

namespace API_Arquitetura.Domain.Entities
{
    public class Aluno : IdentityUser<Guid>
    {
        public string Nome { get; set; } = string.Empty;
        public string RU { get; set; } = string.Empty;
    }
}
