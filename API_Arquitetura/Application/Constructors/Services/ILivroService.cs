using API_Arquitetura.Application.DTOs.Livros;
using API_Arquitetura.Domain.Entities;

namespace API_Arquitetura.Application.Constructors.Services
{
    public interface ILivroService
    {
        public Task<List<LivroDTO>> GetAllAsync();
        public Task<LivroDTO?> GetByIdAsync(Guid id);
        public Task<LivroDTO?> AddAsync(CreateLivroDTO dto);
        public Task<LivroDTO?> UpdateAsync(UpdateLivroDTO dto);
        public Task<bool> DeleteAsync(Guid id);
    }
}
