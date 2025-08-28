using API_Arquitetura.Application.DTOs.Alunos;

namespace API_Arquitetura.Application.Constructors.Services
{
    public interface IAlunoService
    {
        public Task<AlunoDTO?> AddAsync(CreateAlunoDTO dto);
        public Task<AlunoDTO?> UpdateAsync(UpdateAlunoDTO dto);
        public Task<bool> DeleteAsync(Guid id);
        public Task<AlunoDTO?> GetByIdAsync(Guid id);
        public Task<List<AlunoDTO>> GetAllAsync();

    }
}
