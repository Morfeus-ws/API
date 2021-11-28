namespace WebApplication1.Model
{
    public class Tipo
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public Tipo(long id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public Tipo() { }
    }
}
