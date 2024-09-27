using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Services;
using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AgendaMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(AgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentos()
        {
            var agendamentos = await _agendamentoService.GetAgendamentosAsync();
            return Ok(agendamentos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamentoById(string id)
        {
            return await _agendamentoService.GetAgendamentoByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAgendamento([FromBody] AgendamentoDTO agendamentoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agendamento = await _agendamentoService.CreateAgendamentoAsync(agendamentoDTO);
            return CreatedAtAction(nameof(GetAgendamentoById), new { id = agendamento.Id }, agendamento);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Agendamento>> UpdateAgendamento(string id, Agendamento agendamento)
        {
            return await _agendamentoService.UpdateAgendamentoAsync(id, agendamento);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAgendamento(string id)
        {
            await _agendamentoService.DeleteAgendamentoAsync(id);
            return NoContent();
        }
    }
}