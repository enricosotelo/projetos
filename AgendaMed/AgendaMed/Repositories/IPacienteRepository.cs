using System.Threading.Tasks;
using AgendaMed.Models;
using AgendaMed.Models;

public interface IPacienteRepository
{
    Task<Paciente> GetAsync(string id);
    Task<Paciente> GetByEmailAsync(string email);
    Task ActivateAsync(string id);
    Task DeactivateAsync(string id);
}