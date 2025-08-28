using API_Arquitetura.Domain.Entities;

namespace API_Arquitetura.Application.Constructors.Repositories
{
    public interface ILivroRepository
    {
        public Task<List<Livro>> GetAllAsync();
        public Task<Livro?> GetByIdAsync(Guid id);
        public Task<Livro> AddAsync(Livro livro);
        public Task<Livro> UpdateAsync(Livro livro);
        public Task<bool> DeleteAsync(Guid id);
    }
}
