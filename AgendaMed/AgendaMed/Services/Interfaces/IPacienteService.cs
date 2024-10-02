using AgendaMed.Models;
using AgendaMed.DTO;

namespace AgendaMed.Services
{
    public interface IPacienteService
    {
        Task<Paciente> GetPacienteByIdAsync(string id);
        Task<Paciente> GetPacienteByEmailAsync(string email);
        Task ActivatePacienteAsync(string id);
        Task DeactivatePacienteAsync(string id);
        Task<IEnumerable<Paciente>> GetPacientesAsync();
        Task<Paciente> CreatePacienteAsync(PacienteDTO pacienteDTO);
    }
}
