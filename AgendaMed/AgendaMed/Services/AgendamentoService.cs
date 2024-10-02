using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgendaMed.DTO;
using AgendaMed.Models;
using AgendaMed.Repositories.Interfaces;
using AgendaMed.Services.Interfaces;

namespace AgendaMed.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IPacienteService _pacienteService;
        private readonly IMedicoService _medicoService;

        public AgendamentoService(
            IAgendamentoRepository agendamentoRepository,
            IPacienteService pacienteService,
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





        public async Task<Agendamento> AgendarMedicoAleatoriamente(string especialidade, AgendamentoEspecialidadeDTO request)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(request.PacienteId);
            if (paciente == null || !paciente.Ativo)
            {
                throw new Exception("Paciente não encontrado ou inativo");
            }
            List<Medico> medicosDisponiveis = (List<Medico>)await _medicoService.GetMedicosByEspecialidadeAsync(especialidade);

            if (medicosDisponiveis.Count == 0)
            {
                throw new Exception($"Nenhum médico disponível com a especialidade {especialidade}");
            }

            List<Medico> medicosDisponiveisNaData = medicosDisponiveis
                .Where(medico => _medicoService.VerifyMedicoAvailabilityAsync(medico.Id, request.Date).Result)
                .ToList();

            if (medicosDisponiveisNaData.Count == 0)
            {
                throw new Exception($"Nenhum médico disponível com a especialidade {especialidade} para a data {request.Date.ToShortDateString()}");
            }

            var random = new Random();
            var medicoSelecionado = medicosDisponiveisNaData.ElementAt(random.Next(medicosDisponiveisNaData.Count));

            Agendamento agendamento = new Agendamento
            {
                PacienteId = request.PacienteId,
                MedicoId = medicoSelecionado.Id,
                Date = request.Date
            };


            await _agendamentoRepository.CreateAsync(agendamento);



            return agendamento;
        }

    }
}