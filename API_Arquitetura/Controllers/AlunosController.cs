using API_Arquitetura.Application.Constructors.Services;
using API_Arquitetura.Application.DTOs.Alunos;
using Microsoft.AspNetCore.Mvc;

namespace API_Arquitetura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateAlunoDTO dto)
        {
            var aluno = await _alunoService.AddAsync(dto);
            if (aluno == null)
                return BadRequest("Não foi possível criar o aluno.");

            return CreatedAtAction(nameof(GetById), new { id = aluno.Email }, aluno);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var aluno = await _alunoService.GetByIdAsync(id);
            if (aluno == null)
                return NotFound("Aluno não encontrado.");

            return Ok(aluno);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAlunoDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID inválido.");

            var aluno = await _alunoService.UpdateAsync(dto);
            if (aluno == null)
                return BadRequest("Não foi possível atualizar o aluno.");

            return Ok(aluno);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _alunoService.DeleteAsync(id);
            if (!result)
                return NotFound("Aluno não encontrado ou não foi possível excluir.");

            return NoContent();
        }
    }
}
