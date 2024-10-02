using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgendaMed.Controllers
{
    [ApiController]
    [Route("api/agendamentos")]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentoController(IAgendamentoService agendamentoService)
        {
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAgendamentos()
        {
            var agendamentos = await _agendamentoService.GetAgendamentosAsync();
            var result = agendamentos.Select(a => new
            {
                a.Id,
                a.PacienteId,
                PacienteNome = a.Paciente.Name,
                a.MedicoId,
                MedicoNome = a.Medico.Name,
                MedicoEspecialidade = a.Medico.Especialidade,
                a.Date
            });
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAgendamentoById(string id)
        {
            var agendamento = await _agendamentoService.GetAgendamentoByIdAsync(id);

            if (agendamento == null)
            {
                return NotFound();

            }
            return Ok(agendamento);
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
        public async Task<ActionResult<Agendamento>> UpdateAgendamento(string id, [FromBody] Agendamento agendamento)
        {
            var updatedAgendamento = await _agendamentoService.UpdateAgendamentoAsync(id, agendamento);
            if (updatedAgendamento == null)
            {
                return NotFound();
            }
            return Ok(updatedAgendamento);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAgendamento(string id)
        {
            var agendamento = await _agendamentoService.GetAgendamentoByIdAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            await _agendamentoService.DeleteAgendamentoAsync(id);
            return NoContent();
        }

        [HttpPost("especialidades/{nomeEspecialidade}")]
        public async Task<IActionResult> AgendarPorEspecialidade(string nomeEspecialidade, [FromBody] AgendamentoEspecialidadeDTO request)
        {
            try
            {
                var agendamento = await _agendamentoService.AgendarMedicoAleatoriamente(nomeEspecialidade, request);
                return Ok(agendamento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
