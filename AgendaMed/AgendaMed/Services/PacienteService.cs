using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Repositories;

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

        public async Task<Paciente> UpdatePacienteAsync(string id, Paciente paciente)
        {
            var existingPaciente = await _pacienteRepository.GetAsync(id);
            if (existingPaciente == null)
            {
                throw new Exception("Paciente não encontrado");
            }

            existingPaciente.Name = paciente.Name;
            existingPaciente.Email = paciente.Email;
            existingPaciente.Telefone = paciente.Telefone;

            await _pacienteRepository.UpdateAsync(existingPaciente);
            return existingPaciente;
        }

        public async Task DeletePacienteAsync(string id)
        {
            var existingPaciente = await _pacienteRepository.GetAsync(id);
            if (existingPaciente == null)
            {
                throw new Exception("Paciente não encontrado");
            }

            await _pacienteRepository.DeleteAsync(existingPaciente);
        }
    }
}
