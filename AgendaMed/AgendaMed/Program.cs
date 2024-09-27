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
        // Ignorar refer�ncias circulares na serializa��o JSON
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Configura��o do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inje��o de depend�ncia para o servi�o de Paciente
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

// Inje��o de depend�ncia para o servi�o de Agendamento
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

// Inje��o de depend�ncia para o servi�o de M�dico
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();

// Configura��o do DbContext com MySQL
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

// Middleware de autoriza��o
app.UseAuthorization();

// Mapeia os controladores
app.MapControllers();

// Inicia a aplica��o
app.Run();
