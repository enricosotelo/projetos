using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaMed.Models
{
    public class Agendamento
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }

        public string PacienteId { get; set; }
        public Paciente Paciente { get; set; }

        public string MedicoId { get; set; }
        public Medico Medico { get; set; }

        public DateTime Date { get; set; }

        private Agendamento() { }

        public Agendamento(string pacienteId, string medicoId, DateTime date)
        {
            this.PacienteId = pacienteId;
            this.MedicoId = medicoId;
            this.Date = date;
        }
    }
}