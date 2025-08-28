using API_Arquitetura.Application.Constructors.Repositories;
using API_Arquitetura.Domain.Entities;
using API_Arquitetura.Infrastructure.Persisntece.Data;
using Microsoft.EntityFrameworkCore;

namespace API_Arquitetura.Infrastructure.Persisntece.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Livro> AddAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
            return livro;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var livro = await GetByIdAsync(id);
            if (livro == null)
                return false;

            _context.Livros.Remove(livro);
            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<List<Livro>> GetAllAsync()
        {
            return await _context.Livros.ToListAsync();
        }

        public async Task<Livro?> GetByIdAsync(Guid id)
        {
            return await _context.Livros.FindAsync(id);
        }

        public async Task<Livro> UpdateAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }
    }
}
