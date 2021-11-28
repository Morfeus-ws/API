using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class HeroGrupo
    {
        public int IdGrupo { get; set; }
        public int IdHero { get; set; }

        public HeroGrupo(int idGrupo, int idHero)
        {
            IdGrupo = idGrupo;
            IdHero = idHero;
        }
    }
}
