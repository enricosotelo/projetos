using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgendaMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly IMedicoService _medicoService;

        public MedicoController(IMedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            var medicos = await _medicoService.GetMedicosAsync();
            return Ok(medicos);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetMedicoById(string id)
        {
            return await _medicoService.GetMedicoByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedico([FromBody] MedicoDTO medicoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medico = await _medicoService.CreateMedicoAsync(medicoDTO);
            return CreatedAtAction(nameof(GetMedicoById), new { id = medico.Id }, medico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Medico>> UpdateMedico(string id, Medico medico)
        {
            return await _medicoService.UpdateMedicoAsync(id, medico);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMedico(string id)
        {
            await _medicoService.DeleteMedicoAsync(id);
            return NoContent();
        }
    }
}