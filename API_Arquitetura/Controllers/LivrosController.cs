using API_Arquitetura.Application.Constructors.Services;
using API_Arquitetura.Application.DTOs.Livros;
using Microsoft.AspNetCore.Mvc;

namespace API_Arquitetura.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivrosController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLivroDTO dto)
        {
            var livro = await _livroService.AddAsync(dto);
            if (livro == null)
                return BadRequest("Não foi possível criar o livro.");

            return CreatedAtAction(nameof(GetById), new { id = livro.Id }, livro);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var livro = await _livroService.GetByIdAsync(id);
            if (livro == null)
                return NotFound("Livro não encontrado.");

            return Ok(livro);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _livroService.GetAllAsync();
            return Ok(livros);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateLivroDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID inválido.");

            var livro = await _livroService.UpdateAsync(dto);
            if (livro == null)
                return BadRequest("Não foi possível atualizar o livro.");

            return Ok(livro);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _livroService.DeleteAsync(id);
            if (!result)
                return NotFound("Livro não encontrado ou não foi possível excluir.");

            return NoContent();
        }
    }
}
