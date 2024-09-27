using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Repositories;

namespace AgendaMed.Services
{
    public class AgendamentoService : IAgendamentoService // Make sure AgendamentoService implements IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IPacienteService _pacienteService; // Use the interface instead of the concrete implementation
        private readonly IMedicoService _medicoService;

        public AgendamentoService(
            IAgendamentoRepository agendamentoRepository,
            IPacienteService pacienteService, // Use interface here
            IMedicoService medicoService)
        {
            _agendamentoRepository = agendamentoRepository;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
        }
        public async Task<Agendamento> CreateAgendamentoAsync(AgendamentoDTO agendamentoDTO)
        {
            var agendamento = new Agendamento
            {
                PacienteId = agendamentoDTO.PacienteId,
                MedicoId = agendamentoDTO.MedicoId,
                Date = agendamentoDTO.Date
            };

            await _agendamentoRepository.CreateAsync(agendamento);
            return agendamento;
        }
        public async Task<IEnumerable<Agendamento>> GetAgendamentosAsync()
        {
            return await _agendamentoRepository.GetAsync();
        }

        public async Task<Agendamento> GetAgendamentoByIdAsync(string id)
        {
            return await _agendamentoRepository.GetAsync(id);
        }

        public async Task<Agendamento> CreateAgendamentoAsync(string idMedico, string idPaciente, DateTime data)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(idPaciente);
            if (paciente == null || !paciente.Ativo)
            {
                throw new Exception("Paciente não encontrado ou não ativo");
            }

            var medico = await _medicoService.GetMedicoByIdAsync(idMedico);
            if (medico == null)
            {
                throw new Exception("Médico não encontrado");
            }

            if (!await _medicoService.VerifyMedicoAvailabilityAsync(idMedico, data))
            {
                throw new Exception("Médico não disponível na data especificada");
            }

            var agendamento = new Agendamento(idPaciente, idMedico, data);
            return await _agendamentoRepository.CreateAsync(agendamento);
        }

        public async Task<Agendamento> CreateAgendamentoWithRandomMedicoAsync(string especialidade, string idPaciente, DateTime data)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(idPaciente);
            if (paciente == null || !paciente.Ativo)
            {
                throw new Exception("Paciente não encontrado ou não ativo");
            }

            var medicos = await _medicoService.GetMedicosByEspecialidadeAsync(especialidade);
            if (!medicos.Any())
            {
                throw new Exception("Nenhum médico encontrado com a especialidade especificada");
            }

            var randomMedico = medicos.ElementAt(new Random().Next(medicos.Count()));
            if (!await _medicoService.VerifyMedicoAvailabilityAsync(randomMedico.Id, data))
            {
                throw new Exception("Médico não disponível na data especificada");
            }

            var agendamento = new Agendamento(idPaciente, randomMedico.Id, data);
            return await _agendamentoRepository.CreateAsync(agendamento);
        }



        public async Task<Agendamento> UpdateAgendamentoAsync(string id, Agendamento agendamento)
        {
            var existingAgendamento = await _agendamentoRepository.GetAsync(id);
            if (existingAgendamento == null)
            {
                throw new Exception("Agendamento not found");
            }

            existingAgendamento.PacienteId = agendamento.PacienteId;
            existingAgendamento.MedicoId = agendamento.MedicoId;
            existingAgendamento.Date = agendamento.Date;

            return await _agendamentoRepository.UpdateAsync(existingAgendamento);
        }

        public async Task DeleteAgendamentoAsync(string id)
        {
            await _agendamentoRepository.DeleteAsync(id);
        }
    }
}
