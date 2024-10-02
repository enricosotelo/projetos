using AgendaMed.DataContext;
using AgendaMed.Repositories.Interfaces;
using AgendaMed.Repositories;
using AgendaMed.Services.Interfaces;
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

// Inje��o de depend�ncia para os servi�os e reposit�rios
builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();

builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IMedicoRepository, MedicoRepository>();


builder.Services.AddDbContext<AgendaMedDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("AgendaMedDb"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("AgendaMedDb"))
    )
);

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
