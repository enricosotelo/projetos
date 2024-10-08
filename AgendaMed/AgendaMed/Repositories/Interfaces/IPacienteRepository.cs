﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaMed.Models;

namespace AgendaMed.Repositories.Interfaces
{
    public interface IPacienteRepository
    {
        Task<Paciente> GetAsync(string id);
        Task<Paciente> GetByEmailAsync(string email);
        Task ActivateAsync(string id);
        Task DeactivateAsync(string id);
        Task<IEnumerable<Paciente>> GetAsync();
        Task CreateAsync(Paciente paciente);
    }
}