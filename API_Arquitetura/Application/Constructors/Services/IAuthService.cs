using API_Arquitetura.Application.DTOs.Alunos;

namespace API_Arquitetura.Application.Constructors.Services
{
    public interface IAuthService
    {
        Task<AlunoDTO?> LoginAsync(string email, string senha);
        Task<bool> LogoutAsync();
    }
}
