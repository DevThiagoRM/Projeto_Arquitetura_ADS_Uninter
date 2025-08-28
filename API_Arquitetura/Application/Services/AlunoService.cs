using API_Arquitetura.Application.Constructors.Services;
using API_Arquitetura.Application.DTOs.Alunos;
using API_Arquitetura.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API_Arquitetura.Application.Services
{
    public class AlunoService : IAlunoService
    {
        private readonly ILogger<AlunoService> _logger;
        private readonly UserManager<Aluno> _userManager;

        public AlunoService(ILogger<AlunoService> logger, UserManager<Aluno> userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<AlunoDTO?> AddAsync(CreateAlunoDTO dto)
        {
            try
            {
                _logger.LogInformation($"{nameof(AddAsync)} iniciado.");

                if (dto == null)
                {
                    _logger.LogInformation("Aluno não contém dados.");
                    return null;
                }

                var novoAluno = new Aluno()
                {
                    Id = Guid.NewGuid(),
                    Nome = dto.Nome,
                    PhoneNumber = dto.Telefone,
                    Email = dto.Email,
                    UserName = dto.Email,
                    EmailConfirmed = true
                };

                _logger.LogInformation("Criando novo aluno");
                var result = await _userManager.CreateAsync(novoAluno, dto.RU);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Falha ao criar aluno: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return null;
                }
                _logger.LogInformation("Aluno criado com sucesso");

                var alunoDTO = new AlunoDTO()
                {
                    Nome = novoAluno.Nome,
                    Email = novoAluno.Email,
                    Telefone = novoAluno.PhoneNumber
                };

                return alunoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(AddAsync)}' em AlunoService");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteAsync)} iniciado.");

                _logger.LogInformation("Verificando se aluno existe.");
                var aluno = await _userManager.FindByIdAsync(id.ToString());
                if (aluno == null)
                {
                    _logger.LogInformation("Aluno não existe no banco de dados");
                    return false;
                }

                _logger.LogInformation($"Excluindo aluno com id {id}");
                var result = await _userManager.DeleteAsync(aluno);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Falha ao excluir aluno: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return false;
                }

                _logger.LogInformation($"aluno com id {id} excluído com sucesso");
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(DeleteAsync)}' em AlunoService");
                return false;
            }
        }

        public async Task<List<AlunoDTO>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAllAsync)} iniciado.");

                var alunos = await _userManager.Users.ToListAsync();
                if (alunos == null || !alunos.Any())
                {
                    _logger.LogInformation("Nenhum aluno encontrado.");
                    return new List<AlunoDTO>();
                }

                var alunosDTO = alunos.Select(a => new AlunoDTO
                {
                    Nome = a.Nome,
                    Email = a.Email,
                    Telefone = a.PhoneNumber
                }).ToList();

                return alunosDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(GetAllAsync)}' em AlunoService");
                return new List<AlunoDTO>();
            }
        }

        public async Task<AlunoDTO?> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetByIdAsync)} iniciado para id {id}.");

                var aluno = await _userManager.FindByIdAsync(id.ToString());
                if (aluno == null)
                {
                    _logger.LogInformation($"Aluno com id {id} não encontrado.");
                    return null;
                }

                var alunoDTO = new AlunoDTO
                {
                    Nome = aluno.Nome,
                    Email = aluno.Email,
                    Telefone = aluno.PhoneNumber
                };

                return alunoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(GetByIdAsync)}' em AlunoService");
                return null;
            }
        }

        public async Task<AlunoDTO?> UpdateAsync(UpdateAlunoDTO dto)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateAsync)} iniciado.");

                if (dto == null)
                {
                    _logger.LogInformation("Aluno não contém dados.");
                    return null;
                }

                var alunoAtualizado = new Aluno()
                {
                    Nome = dto.Nome,
                    PhoneNumber = dto.Telefone,
                    Email = dto.Email,
                    UserName = dto.Email
                };

                _logger.LogInformation("Atualizando dados do aluno");
                var result = await _userManager.UpdateAsync(alunoAtualizado);
                if (!result.Succeeded)
                {
                    _logger.LogWarning($"Falha ao atualizar aluno: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    return null;
                }
                _logger.LogInformation("Aluno criado com sucesso");

                var alunoDTO = new AlunoDTO()
                {
                    Nome = alunoAtualizado.Nome,
                    Email = alunoAtualizado.Email,
                    Telefone = alunoAtualizado.PhoneNumber
                };

                return alunoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(UpdateAsync)}' em AlunoService");
                return null;
            }
        }
    }
}
