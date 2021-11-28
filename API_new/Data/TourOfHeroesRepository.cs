using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Interfaces;
using WebApplication1.Model;


namespace WebApplication1.Data
{
    public class TourOfHeroesRepository : ITourOfHeroesRepository
    {
        private readonly TourOfHeroesContexto _Context;

        public TourOfHeroesRepository(TourOfHeroesContexto context)
        {
            _Context = context;
        }


        public IEnumerable<Grupo> GetAllGrupos()
        {
            return _Context.Grupo.Include("Tipo").Include("GrupoHerois").ToList();
        }

        public Grupo GetGrupoById(long id)
        {
            return _Context.Grupo.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Heroi> GetHeroisById(IEnumerable<int> heroisIds)
        {
            var uniqueIds = new List<int>();
            foreach (var id in heroisIds)
                if (!uniqueIds.Contains(id))
                    uniqueIds.Add(id);
            var herois = new List<Heroi>();
            foreach (var id in uniqueIds)
            {
                var heroi = _Context.Heroi.Find(id);
                if (heroi is null)
                {
                    throw new EntityNotFoundException($"Missing heroi #{id}");
                }

                herois.Add(heroi);
            }

            return herois;
        }

        public Grupo GetGrupoByName(string name)
        {
            return _Context.Grupo.FirstOrDefault(g => g.Nome.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }

        public Grupo CreateGrupo(Grupo grupo)
        {
            _Context.Grupo.Add(grupo);
            return _Context.SaveChanges() > 0 ? grupo : null;
        }

        public void AssignHerosToGroup(IEnumerable<Heroi> herois, Grupo newGrupo)
        {
            var heroisGrupo = _Context.HeroiGrupo.Where(hg => hg.IdGrupo == newGrupo.Id).ToList();
            var changed = false;
            foreach (var heroi in herois)
            {
                if (heroisGrupo.FirstOrDefault(hg => hg.IdHero == heroi.Id) is { }) continue;
                _Context.HeroiGrupo.Add(new HeroiGrupo
                {
                    IdGrupo = newGrupo.Id,
                    IdHero = heroi.Id
                });
                changed = true;
            }

            if (changed)
                _Context.SaveChanges();
        }

        public void UpdateGrupo(Grupo grupo, IEnumerable<Heroi> herois)
        {
            AssignHerosToGroup(herois, grupo);
            _Context.Grupo.Update(grupo);
            _Context.SaveChanges();
        }

        public void DeleteGrupo(Grupo grupo)
        {
            _Context.Grupo.Remove(grupo);
            _Context.SaveChanges();
        }

        public IEnumerable<Heroi> GetHerois()
        {
            return _Context.Heroi.Include("Tipo").ToList();
        }

        public Heroi GetHeroiById(long id)
        {
            return _Context.Heroi.Include("Tipo").FirstOrDefault(h => h.Id == id);
        }

        public Heroi GetHeroiByName(string heroiNome)
        {
            return _Context.Heroi.Include("Tipo")
                .FirstOrDefault(h => h.Nome.Equals(heroiNome, StringComparison.CurrentCultureIgnoreCase));
        }

        public Heroi CreateHeroi(Heroi heroi)
        {
            _Context.Heroi.Add(heroi);
            return _Context.SaveChanges() > 0 ? heroi : null;
        }

        public void UpdateHeroi(Heroi updHeroi)
        {
            _Context.Heroi.Update(updHeroi);
            _Context.SaveChanges();
        }

        public IEnumerable<Grupo> GetGruposFromHeroi(long heroiId)
        {
            var grupoIds = _Context.HeroiGrupo
                .Where(hg => hg.IdHero == heroiId)
                .Select(hg => hg.IdGrupo)
                .ToArray();
            return GetGruposById(grupoIds);
        }

        public void DeleteHeroi(Heroi existentHero)
        {
            _Context.Heroi.Remove(existentHero);
            _Context.SaveChanges();
        }

        private IEnumerable<Grupo> GetGruposById(long[] grupoIds)
        {
            return _Context.Grupo.Where(g => grupoIds.Contains(g.Id)).ToList();
        }
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string s) : base(s)
        {
        }
    }
}