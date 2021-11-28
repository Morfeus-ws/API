namespace WebApplication1.Model
{
    public class HeroiGrupo
    {
        public long IdGrupo { get; set; }
        public long IdHero { get; set; }

        public Grupo Grupo { get; set; }
        public Heroi Heroi { get; set; }
        public HeroiGrupo(long idGrupo, long idHero)
        {
            IdGrupo = idGrupo;
            IdHero = idHero;
        }

        public HeroiGrupo() { }
    }
}
