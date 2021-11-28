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


        #region Heroi

        public void AdicionarHeroi(Heroi heroi)
        {
            using var db = new TourOfHeroesContexto();
            db.Heroi.Add(heroi);
            db.SaveChanges();
        }

        public bool BuscarHeroi(long id)
        {
            var returno = _Context.HeroiGrupo.Any(x => x.IdHero == id);
            return returno;
        }

        public bool GrupoTemHerois(long idGrupo)
        {
            return _Context.HeroiGrupo.Any(hg => hg.IdGrupo == idGrupo);
        }


        public Heroi BuscarHeroiId(long id)
        {
            var returno = _Context.Heroi.SingleOrDefault(x => x.Id == id);
            return returno;
        }

        public List<Heroi> BuscarHerois()
        {
            return _Context.Heroi.Include("Tipo").ToList();
        }

        public void DeletaHeroi(Heroi heroi)
        {
            using (var db = new TourOfHeroesContexto())
            {
                db.Heroi.Remove(heroi);
                db.SaveChanges();
            }
        }

        public void AlteraHeroi(Heroi heroi)
        {
            using (var db = new TourOfHeroesContexto())
            {
                var entidade = db.Heroi.FirstOrDefault(x => x.Id == heroi.Id);

                entidade.Nome = heroi.Nome;
                entidade.Identidade = heroi.Identidade;
                entidade.Idtipo = heroi.Idtipo;
                entidade.Poder = heroi.Poder;

                db.SaveChanges();
            }
        }

        #endregion

        #region Grupo

        public Grupo BuscarGrupoId(long id)
        {
            using (var db = new TourOfHeroesContexto())
            {
                var returno = _Context.Grupo.Where(x => x.Id == id).Single();
                return returno;
            }
        }

        public List<Grupo> BuscarGrupos()
        {
            using (var db = new TourOfHeroesContexto())
            {
                return db.Grupo.Include("Tipo").Include("GrupoHerois").ToList();
            }
        }

        public bool BuscarGrupo(long id)
        {
            using (var db = new TourOfHeroesContexto())
            {
                var returno = db.Grupo.Any(x => x.Id == id);
                return returno;
            }
        }

        public List<HeroiGrupo> BuscarHeroiGrupo()
        {
            using (var db = new TourOfHeroesContexto())
            {
                var list = db.HeroiGrupo.ToList();
                if (list == null)
                {
                    return new List<HeroiGrupo>();
                }

                return list;
            }
        }


        //public void AlterarNomeGrupo(Grupo grupo)
        //{
        //    using (var db = new TourOfHeroesContexto())
        //    {

        //        db.Grupo.Add(grupo);
        //        db.SaveChanges();

        //    }
        //}

        public void ExcluirGrupo(int idGrupo)
        {
            using var db = new TourOfHeroesContexto();
            var entidade = db.Grupo.FirstOrDefault(x => x.Id == idGrupo);
            db.Grupo.Remove(entidade);
            db.SaveChanges();
        }

        public void AdicionarGrupo(Grupo grupo)
        {
            using (var db = new TourOfHeroesContexto())
            {
                db.Grupo.Add(grupo);
                db.SaveChanges();
            }
        }


        public void AlteraGrupo(Grupo grupo)
        {
            using (var db = new TourOfHeroesContexto())
            {
                var entidadeheroigrupo = db.HeroiGrupo.Where(x => x.IdGrupo == grupo.Id).ToList();
                entidadeheroigrupo.ForEach(x =>
                {
                    var h = db.HeroiGrupo.FirstOrDefault(y => y.IdGrupo == x.IdGrupo && y.IdHero == x.IdHero);


                    db.HeroiGrupo.Remove(h);
                });
                db.SaveChanges();

                var entidade = db.Grupo.FirstOrDefault(x => x.Id == grupo.Id);
                entidade.Nome = grupo.Nome;
                entidade.Idtipo = grupo.Idtipo;
                entidade.GrupoHerois = grupo.GrupoHerois;
                db.SaveChanges();
            }
        }

        #endregion

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
    }

    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string s) : base(s)
        {
        }
    }
}