using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaMed.Models
{
    public class Paciente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Ativo {  get; set; }


        private Paciente(){ }
        

        public Paciente(string Name, string Email, bool Ativo  )
        {

            this.Name = Name;
            this.Email = Email;
            this.Ativo = Ativo;
        }
    }
}
