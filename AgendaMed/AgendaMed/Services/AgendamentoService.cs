using System;
using System.Threading.Tasks;
using AgendaMed.Models;
using AgendaMed.Repositories;
using Microsoft.EntityFrameworkCore;
using AgendaMed.Models;


public class AgendamentoService
{
    private readonly IAgendamentoRepository _agendamentoRepository;
    private readonly PacienteService _pacienteService;
    private readonly MedicoService _medicoService;

    public AgendamentoService(
        IAgendamentoRepository agendamentoRepository,
        PacienteService pacienteService,
        MedicoService medicoService)
    {
        _agendamentoRepository = agendamentoRepository;
        _pacienteService = pacienteService;
        _medicoService = medicoService;
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
}