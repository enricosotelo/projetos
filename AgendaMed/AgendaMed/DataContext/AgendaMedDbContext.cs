using AgendaMed.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendaMed.DataContext;

public partial class AgendaMedDbContext : DbContext
{
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Agendamento> Agendamentos { get; set; }

    public AgendaMedDbContext(DbContextOptions<AgendaMedDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Paciente)
            .WithMany(p => p.Agendamentos)
            .HasForeignKey(a => a.PacienteId);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Medico)
            .WithMany(m => m.Agendamentos)
            .HasForeignKey(a => a.MedicoId);

        modelBuilder.Entity<Paciente>()
            .HasMany(p => p.Agendamentos)
            .WithOne(a => a.Paciente)
            .HasForeignKey(a => a.PacienteId);

        modelBuilder.Entity<Medico>()
            .HasMany(m => m.Agendamentos)
            .WithOne(a => a.Medico)
            .HasForeignKey(a => a.MedicoId);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
