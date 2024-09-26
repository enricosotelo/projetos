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
        private readonly PacienteService _pacienteService;

        public PacienteController(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _pacienteService.GetPacientesAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPacienteById(string id)
        {
            return await _pacienteService.GetPacienteByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente(Paciente paciente)
        {
            return await _pacienteService.CreatePacienteAsync(paciente);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Paciente>> UpdatePaciente(string id, Paciente paciente)
        {
            return await _pacienteService.UpdatePacienteAsync(id, paciente);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePaciente(string id)
        {
            await _pacienteService.DeletePacienteAsync(id);
            return NoContent();
        }
    }
}