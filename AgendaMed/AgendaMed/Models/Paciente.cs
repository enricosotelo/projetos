using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaMed.Models
{
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }

        public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

        public Paciente() { }

        public Paciente(string Name, string Email, string Telefone, bool Ativo)
        {
            this.Name = Name;
            this.Email = Email;
            this.Ativo = Ativo;
            this.Telefone = Telefone;
            this.Agendamentos = new HashSet<Agendamento>();
        }
    }
}