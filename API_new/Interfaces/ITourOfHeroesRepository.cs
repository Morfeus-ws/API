using System.Collections.Generic;
using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface ITourOfHeroesRepository
    {
        IEnumerable<Grupo> GetAllGrupos();
        Grupo GetGrupoById(long id);
        IEnumerable<Heroi> GetHeroisById(IEnumerable<int> heroisIds);
        Grupo GetGrupoByName(string grupoDtoNome);
        Grupo CreateGrupo(Grupo grupo);
        void AssignHerosToGroup(IEnumerable<Heroi> herois, Grupo newGrupo);
        void UpdateGrupo(Grupo grupo, IEnumerable<Heroi> herois);
        void DeleteGrupo(Grupo grupo);
        IEnumerable<Heroi> GetHerois();
        Heroi GetHeroiById(long id);
        Heroi GetHeroiByName(string heroiNome);
        Heroi CreateHeroi(Heroi heroi);
        void UpdateHeroi(Heroi updHeroi);
        IEnumerable<Grupo> GetGruposFromHeroi(long heroiId);
        void DeleteHeroi(Heroi existentHero);
    }
}