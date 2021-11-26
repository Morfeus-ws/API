using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;

namespace WebApplication1.Model
{
    public class Grupo
    {
        public long Id { get; set; }
        public string Nome { get; set; }

        public long Idtipo { get; set; }

        public Tipo Tipo { get; set; }
        public ICollection<HeroiGrupo> GrupoHerois { get; set; }

        public Grupo(long id, string nome, int idtipo, ICollection<HeroiGrupo> grupoHerois)
        {
            Id = id;
            Nome = nome;
            Idtipo = idtipo;
            GrupoHerois = grupoHerois;
        }

        public Grupo() { }
    }
}   
