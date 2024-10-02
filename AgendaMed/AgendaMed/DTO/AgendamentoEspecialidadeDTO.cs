using System.Text.Json.Serialization;

namespace AgendaMed.DTO
{
    public class AgendamentoEspecialidadeDTO
    {
        public string PacienteId { get; set; }

        public DateTime Date { get; set; }
    }
}
