using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface IHeroBusiness
    {
        IEnumerable<Heroi> GetHerois();
        Heroi GetHeroyById(long id);
        Heroi CreateHero(Heroi heroi);
        void UpdateHero(Heroi heroi);
        void DeleteHero(long id);
    }
}