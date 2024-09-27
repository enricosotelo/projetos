using AgendaMed.DataContext;
using AgendaMed.Repositories;
using AgendaMed.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignorar referências circulares na serialização JSON
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência para o serviço de Paciente
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

// Injeção de dependência para o serviço de Agendamento
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

// Injeção de dependência para o serviço de Médico
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();

// Configuração do DbContext com MySQL
builder.Services.AddDbContext<AgendaMedDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("AgendaMedDb"),
                      ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("AgendaMedDb"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Middleware de autorização
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia a aplicação
app.Run();
