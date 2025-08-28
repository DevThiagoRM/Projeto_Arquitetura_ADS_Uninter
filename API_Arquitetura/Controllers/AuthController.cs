using API_Arquitetura.Application.Constructors.Services;
using API_Arquitetura.Application.DTOs.Alunos;
using Microsoft.AspNetCore.Mvc;

namespace API_Arquitetura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var aluno = await _authService.LoginAsync(dto.Email, dto.Senha);
            if (aluno == null)
                return Unauthorized("Email ou senha inválidos.");

            return Ok(aluno);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if (!result)
                return BadRequest("Não foi possível realizar logout.");

            return NoContent();
        }
    }

    public class LoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
