namespace AgendaMed.Models
{
    public class Medico
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Especialidade { get; set; }


        private Medico(){ }
        public Medico(string Name, string Especialidade)
        {
            this.Name = Name;
            this.Especialidade = Especialidade;
        }
    }
}
