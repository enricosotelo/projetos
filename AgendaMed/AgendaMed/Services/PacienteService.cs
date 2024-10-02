using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Repositories.Interfaces;

namespace AgendaMed.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;

        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }


        public async Task<Paciente> CreatePacienteAsync(PacienteDTO pacienteDTO)
        {
            var existingPaciente = await _pacienteRepository.GetByEmailAsync(pacienteDTO.Email);
            if (existingPaciente != null)
            {
                throw new Exception("Paciente já existe");
            }

            var paciente = new Paciente
            {
                Name = pacienteDTO.Name,
                Email = pacienteDTO.Email,
                Telefone = pacienteDTO.Telefone,
                Ativo = pacienteDTO.Ativo
            };

            await _pacienteRepository.CreateAsync(paciente);
            return paciente;
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

        public async Task<IEnumerable<Paciente>> GetPacientesAsync()
        {
            return await _pacienteRepository.GetAsync();
        }


    }
}
