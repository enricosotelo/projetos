using AgendaMed.Models;
using AgendaMed.Services;
using Microsoft.AspNetCore.Mvc;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AgendaMed.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly MedicoService _medicoService;

        public MedicoController(MedicoService medicoService)
        {
            _medicoService = medicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            return await _medicoService.GetMedicosAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medico>> GetMedicoById(string id)
        {
            return await _medicoService.GetMedicoByIdAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Medico>> CreateMedico(Medico medico)
        {
            return await _medicoService.CreateMedicoAsync(medico);
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