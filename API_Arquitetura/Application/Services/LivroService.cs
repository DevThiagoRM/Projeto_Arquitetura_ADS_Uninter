using API_Arquitetura.Application.Constructors.Repositories;
using API_Arquitetura.Application.Constructors.Services;
using API_Arquitetura.Application.DTOs.Livros;
using API_Arquitetura.Domain.Entities;

namespace API_Arquitetura.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly ILogger<LivroService> _logger;

        public LivroService(ILivroRepository livroRepository, ILogger<LivroService> logger)
        {
            _livroRepository = livroRepository;
            _logger = logger;
        }

        public async Task<LivroDTO?> AddAsync(CreateLivroDTO dto)
        {
            try
            {
                _logger.LogInformation($"{nameof(AddAsync)} iniciado.");

                if (dto == null)
                {
                    _logger.LogInformation("livro não contém dados");
                    return null;
                }

                _logger.LogInformation("Criando novo livro");
                var novoLivro = new Livro
                {
                    Id = Guid.NewGuid(),
                    Autor = dto.Autor,
                    Titulo = dto.Titulo,
                    LocalDePublicacao = dto.LocalDePublicacao,
                    Editora = dto.Editora,
                    Ano = dto.Ano
                };

                _logger.LogInformation("Adicionando novo livro ao banco");
                await _livroRepository.AddAsync(novoLivro);
                _logger.LogInformation($"Livro '{novoLivro.Titulo}' criado com sucesso.");

                var livroDTO = new LivroDTO
                {
                    Id = novoLivro.Id,
                    Autor = novoLivro.Autor,
                    Titulo = novoLivro.Titulo,
                    LocalDePublicacao = novoLivro.LocalDePublicacao,
                    Editora = novoLivro.Editora,
                    Ano = novoLivro.Ano
                };
                return livroDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(AddAsync)}' em LivroService");
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteAsync)} iniciado.");
                var livro = await _livroRepository.GetByIdAsync(id);
                if (livro == null)
                {
                    _logger.LogInformation($"Nenhum livro encontrado para excluir com id {id}");
                    return false;
                }

                _logger.LogInformation("Excluindo livro");
                await _livroRepository.DeleteAsync(id);

                _logger.LogInformation($"Livro com id {id} excluído com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(DeleteAsync)}' em LivroService");
                return false;
            }
        }

        public async Task<List<LivroDTO>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation($"{nameof(GetAllAsync)} iniciado.");

                var livros = await _livroRepository.GetAllAsync();
                if (livros == null || !livros.Any())
                {
                    _logger.LogInformation($"Nenhum livro encontrado.");
                    return new List<LivroDTO>();
                }

                var livrosDTO = livros.Select(l => new LivroDTO
                {
                    Id = l.Id,
                    Autor = l.Autor,
                    Titulo = l.Titulo,
                    LocalDePublicacao = l.LocalDePublicacao,
                    Editora = l.Editora,
                    Ano = l.Ano
                }).ToList();

                return livrosDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(GetAllAsync)}' em LivroService");
                return new List<LivroDTO>();
            }
        }

        public async Task<LivroDTO?> GetByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetByIdAsync)} iniciado.");

                var livro = await _livroRepository.GetByIdAsync(id);
                if (livro == null)
                {
                    _logger.LogInformation($"Nenhum livro encontrado.");
                    return null;
                }

                var livroDTO = new LivroDTO()
                {
                    Id = livro.Id,
                    Autor = livro.Autor,
                    Titulo = livro.Titulo,
                    LocalDePublicacao = livro.LocalDePublicacao,
                    Editora = livro.Editora,
                    Ano = livro.Ano
                };

                return livroDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(GetByIdAsync)}' em LivroService");
                return null;
            }
        }

        public async Task<LivroDTO?> UpdateAsync(UpdateLivroDTO dto)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateAsync)} iniciado.");

                if (dto == null)
                {
                    _logger.LogInformation("livro não contém dados");
                    return null;
                }

                _logger.LogInformation("Criando livro atualizado");
                var livroAtualizado = new Livro
                {
                    Autor = dto.Autor,
                    Titulo = dto.Titulo,
                    LocalDePublicacao = dto.LocalDePublicacao,
                    Editora = dto.Editora,
                    Ano = dto.Ano
                };

                _logger.LogInformation("Atualizando livro no banco");
                await _livroRepository.UpdateAsync(livroAtualizado);
                _logger.LogInformation($"Livro '{livroAtualizado.Titulo}' atualizado com sucesso.");

                var livroDTO = new LivroDTO
                {
                    Id = livroAtualizado.Id,
                    Autor = livroAtualizado.Autor,
                    Titulo = livroAtualizado.Titulo,
                    LocalDePublicacao = livroAtualizado.LocalDePublicacao,
                    Editora = livroAtualizado.Editora,
                    Ano = livroAtualizado.Ano
                };
                return livroDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro no método '{nameof(UpdateAsync)}' em LivroService");
                return null;
            }
        }
    }
}
