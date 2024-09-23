namespace AgendaMed.Models
{
    public class Agendamento
    {
        public string Id { get; set; }

        public Paciente Paciente { get; set; }

        public Medico Medico { get; set; }

        public DateTime Date { get; set; }

        private Agendamento(){  }
    }
}
