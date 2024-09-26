using System.Threading.Tasks;
using AgendaMed.Models;
using AgendaMed.Repositories;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;
using AgendaMed.Repositories;

public class PacienteService
{
    private readonly IPacienteRepository _pacienteRepository;

    public PacienteService(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    public async Task<Paciente> GetPacienteByIdAsync(string id)
    {
        return await _pacienteRepository.GetAsync(id);
    }

    public async Task<Paciente> GetPacienteByEmailAsync(string email)
    {
        return await _pacienteRepository.GetByEmailAsync(email);
    }

    public async Task ActivatePacienteAsync(string id)
    {
        await _pacienteRepository.ActivateAsync(id);
    }

    public async Task DeactivatePacienteAsync(string id)
    {
        await _pacienteRepository.DeactivateAsync(id);
    }
}