using AgendaMed.Models;
using AgendaMed.DTO;

namespace AgendaMed.Services.Interfaces
{
    public interface IMedicoService
    {
        Task<Medico> GetMedicoByIdAsync(string id);
        Task<IEnumerable<Medico>> GetMedicosByEspecialidadeAsync(string especialidade);
        Task<bool> VerifyMedicoAvailabilityAsync(string id, DateTime data);
        Task<IEnumerable<Medico>> GetMedicosAsync();
        Task<Medico> CreateMedicoAsync(MedicoDTO medicoDTO);
        Task<Medico> UpdateMedicoAsync(string id, Medico medico);
        Task DeleteMedicoAsync(string id);
    }
}
