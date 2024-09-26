using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.Models;
using AgendaMed.Repositories;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.Repositories;

public class MedicoService
{
    private readonly IMedicoRepository _medicoRepository;

    public MedicoService(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    public async Task<Medico> GetMedicoByIdAsync(string id)
    {
        return await _medicoRepository.GetAsync(id);
    }

    public async Task<IEnumerable<Medico>> GetMedicosByEspecialidadeAsync(string especialidade)
    {
        return await _medicoRepository.GetByEspecialidadeAsync(especialidade);
    }

    public async Task<bool> VerifyMedicoAvailabilityAsync(string id, DateTime data)
    {
        return await _medicoRepository.VerifyAvailabilityAsync(id, data);
    }
}