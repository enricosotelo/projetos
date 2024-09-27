using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaMed.Models
{
    public class Medico
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Especialidade { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; }

        public Medico() { }

        public Medico(string Name, string Especialidade)
        {
            this.Name = Name;
            this.Especialidade = Especialidade;
            this.Agendamentos = new HashSet<Agendamento>();
        }
    }
}