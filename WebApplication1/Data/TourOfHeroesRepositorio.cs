using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Model;



namespace WebApplication1.Data
{
    public class TourOfHeroesRepositorio
    {
        private TourOfHeroesContexto _TourOfHeroesContexto;

        public TourOfHeroesRepositorio(TourOfHeroesContexto tourOfHeroesContexto)
        {
            _TourOfHeroesContexto = tourOfHeroesContexto;
        }
    

        #region Heroi

        public void AdicionarHeroi(Heroi heroi)
        {
            using (var db = new TourOfHeroesContexto())
            {
                db.Heroi.Add(heroi);
                db.SaveChanges();
               
            }
        }

        public bool BuscarHeroi(long id)
        {
            using (var db = new TourOfHeroesContexto())
            {
                var returno = _TourOfHeroesContexto.HeroiGrupo.Any(x => x.IdHero == id);
                return returno;
            }
               
        }

        public Heroi BuscarHeroiId(long id)
        {

            using (var db = new TourOfHeroesContexto())
            {
                var returno = _TourOfHeroesContexto.Heroi.Where(x => x.Id == id).Single();
                return returno;
            }
           
        }

        public List<Heroi> BuscarHerois()
        {
            using ( var db = new TourOfHeroesContexto())
            {
                return db.Heroi.Include("Tipo").ToList();
            }
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
                var returno = _TourOfHeroesContexto.Grupo.Where(x => x.Id == id).Single();
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
            using (var db = new TourOfHeroesContexto())
            {
                var entidade = db.Grupo.FirstOrDefault(x => x.Id == idGrupo);
                db.Grupo.Remove(entidade);
                db.SaveChanges();
            }
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
                entidadeheroigrupo.ForEach(x => {
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
    }
}
