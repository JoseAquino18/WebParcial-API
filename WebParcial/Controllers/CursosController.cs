using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebParcial.Data;
using WebParcial.Models;

namespace WebParcial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CursosController(AppDbContext context)
        {
            _context = context;
        }

        // Método para listar todos los cursos (renombrado a GetAllCursos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetAllCursos()
        {
            // Obtiene todos los cursos e incluye la información del docente asociado
            var cursos = await _context.Cursos.Include(c => c.Docente).ToListAsync();
            return Ok(cursos);
        }

        // Método para obtener un curso por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
            var curso = await _context.Cursos.Include(c => c.Docente).FirstOrDefaultAsync(c => c.Id == id);
            if (curso == null)
                return NotFound();
            return curso;
        }

        // Método para obtener cursos por ciclo
        [HttpGet("ciclo/{ciclo}")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosPorCiclo(string ciclo)
        {
            var cursos = await _context.Cursos.Include(c => c.Docente).Where(c => c.Ciclo == ciclo).ToListAsync();
            return cursos;
        }

        // Método para agregar un nuevo curso
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            if (curso == null)
            {
                return BadRequest("El objeto curso no puede ser nulo.");
            }

            // Asegúrate de que el IdDocente sea válido
            if (!_context.Docentes.Any(d => d.Id == curso.IdDocente))
            {
                return BadRequest("Docente no encontrado.");
            }

            _context.Cursos.Add(curso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCurso), new { id = curso.Id }, curso);
        }


        // Método para actualizar un curso
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            if (id != curso.Id)
                return BadRequest();

            _context.Entry(curso).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método para eliminar un curso
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurso(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso == null)
                return NotFound();

            _context.Cursos.Remove(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
