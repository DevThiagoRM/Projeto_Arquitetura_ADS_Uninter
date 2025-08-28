using API_Arquitetura.Application.Constructors.Services;
using API_Arquitetura.Application.DTOs.Alunos;
using API_Arquitetura.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace API_Arquitetura.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Aluno> _userManager;
        private readonly SignInManager<Aluno> _signInManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            UserManager<Aluno> userManager,
            SignInManager<Aluno> signInManager,
            ILogger<AuthService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<AlunoDTO?> LoginAsync(string email, string senha)
        {
            try
            {
                _logger.LogInformation($"Login iniciado para {email}");

                var aluno = await _userManager.FindByEmailAsync(email);
                if (aluno == null)
                {
                    _logger.LogWarning($"Aluno com email {email} não encontrado.");
                    return null;
                }

                var result = await _signInManager.CheckPasswordSignInAsync(aluno, senha, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Senha incorreta para {email}");
                    return null;
                }

                _logger.LogInformation($"Aluno {email} autenticado com sucesso.");

                return new AlunoDTO
                {
                    Nome = aluno.Nome,
                    Email = aluno.Email,
                    Telefone = aluno.PhoneNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(LoginAsync)}' em AuthService");
                return null;
            }
        }

        public async Task<bool> LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                _logger.LogInformation("Logout realizado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(LogoutAsync)}' em AuthService");
                return false;
            }
        }
    }
}