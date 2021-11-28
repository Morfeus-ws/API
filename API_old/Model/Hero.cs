using System.Text.Json.Serialization;
using WebApplication1.Model;

namespace WebApplication1.Business
{
    public class Hero
    {
        public Hero(int id, string nome, Tipo tipo, string poder, string identidade)
        {

            Id = id;
            Nome = nome;
            Tipo = tipo;
            Poder = poder;
            Identidade = identidade;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public Tipo Tipo { get; set; }
        public string Poder { get; set; }
        public string Identidade { get; set; }
    }
}
