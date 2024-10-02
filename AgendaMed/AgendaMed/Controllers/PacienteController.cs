using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Services;
using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AgendaMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            var pacientes = await _pacienteService.GetPacientesAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPacienteById(string id)
        {
            return await _pacienteService.GetPacienteByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaciente([FromBody] PacienteDTO pacienteDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var paciente = await _pacienteService.CreatePacienteAsync(pacienteDTO);
            return CreatedAtAction(nameof(GetPacienteById), new { id = paciente.Id }, paciente);
        }
    }
}