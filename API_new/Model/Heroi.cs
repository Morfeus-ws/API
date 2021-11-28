namespace WebApplication1.Model
{
    public class Heroi
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Identidade { get; set; }
        public string Poder { get; set; }
        public long Idtipo { get; set; }

        public Tipo Tipo { get; set; }
        public Heroi(long idheroi, string nome, string identidade, string poder, long idtipo)
        {
            Id = idheroi;
            Nome = nome;
            Identidade = identidade;
            Poder = poder;
            Idtipo = idtipo;
        }

        public Heroi() { }
        
    }
}
