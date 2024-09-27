using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Repositories;

namespace AgendaMed.Services
{
    public class MedicoService : IMedicoService  // Ensure MedicoService implements IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;

        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<Medico> CreateMedicoAsync(MedicoDTO medicoDTO)
        {
            var medico = new Medico
            {
                Name = medicoDTO.Name,
                Especialidade = medicoDTO.Especialidade,

            };

            await _medicoRepository.CreateAsync(medico);
            return medico;
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

        public async Task<IEnumerable<Medico>> GetMedicosAsync()
        {
            return await _medicoRepository.GetAsync();
        }


        public async Task<Medico> UpdateMedicoAsync(string id, Medico medico)
        {
            var existingMedico = await _medicoRepository.GetAsync(id);
            if (existingMedico == null)
            {
                throw new Exception("Médico não encontrado");
            }

            existingMedico.Name = medico.Name;
            existingMedico.Especialidade = medico.Especialidade;

            await _medicoRepository.UpdateAsync(existingMedico);
            return existingMedico;
        }

        public async Task DeleteMedicoAsync(string id)
        {
            var existingMedico = await _medicoRepository.GetAsync(id);
            if (existingMedico == null)
            {
                throw new Exception("Médico não encontrado");
            }

            await _medicoRepository.DeleteAsync(existingMedico);
        }
    }
}
