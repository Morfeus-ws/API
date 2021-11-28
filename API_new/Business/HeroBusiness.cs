using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WebApplication1.Model;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Application;

namespace WebApplication1.Business
{
    public class HeroBusiness
    {

        private TourOfHeroesRepositorio _tourOfHeroRepoitory;

        public HeroBusiness(TourOfHeroesRepositorio tourOfHeroesRepositorio)
        {
            _tourOfHeroRepoitory = tourOfHeroesRepositorio;
        }

        public List<Heroi> RetornaHeros()


        {
            return _tourOfHeroRepoitory.BuscarHerois();
        }

        public Heroi RetornaHeroId(int id)
        {
            var hero = _tourOfHeroRepoitory.BuscarHerois().FirstOrDefault(x => x.Id == id);
        
           return hero;
        }

        public void DeletaHero(int id)
        {
           // se passar id inexistente vai bugar
            List<HeroiGrupo> list = _tourOfHeroRepoitory.BuscarHeroiGrupo();
             Heroi heroi = _tourOfHeroRepoitory.BuscarHeroiId(id);


            // trocar esse codigo pelo metodo ValidaSeHeroiPossuiGrupo
            var testeid = false;
            if(!(list.Count == 0))
            {
                foreach (var item in list)
                {
                    if (item.IdHero == id)
                    {
                        testeid = true;

                    }

                }

                if ((testeid))
                {
                    throw new Exception("o Heroi não pode ser excluido, pois ele ja participa de um grupo!");
                }

            }
             
           

            _tourOfHeroRepoitory.DeletaHeroi(heroi);

 
       }

        // receber como parametro HeroDto em vez de Heroi
        public void AtualizaHero(Heroi heroi)
        {
            var heroAntigo = new Heroi(0, heroi.Nome, heroi.Identidade, heroi.Poder, heroi.Idtipo);

            // verificação desnecessaria
            if (heroAntigo == null)
            {
                throw new Exception("Heroi que está sendo atualizado não foi encontrado!");
            }

            //Tipo não pode ser alterado, pois heroi ja esta em um grupo
            //Identidade do Heroi não pode ser alterada!
            ValidaHeroi(heroi);

            if (_tourOfHeroRepoitory.BuscarHeroi(heroi.Id))
            {
                throw new Exception("Não existe Heroi");

            }


            _tourOfHeroRepoitory.AlteraHeroi(heroi);
           
        }


        // receber como parametro HeroDto em vez de Heroi
        public Heroi Criar(Heroi heroi)
        {

            ValidaHeroi(heroi);

            //var id = Data.Data.heroes.Count + 1;
            var hero = new Heroi(0, heroi.Nome, heroi.Identidade, heroi.Poder, heroi.Idtipo);

            _tourOfHeroRepoitory.AdicionarHeroi(hero);
            return hero;

         }

        // alterar metodo para nao referenciar classe Data, e chamar ele na hora de atualizar o heroi
        public bool ValidaSeHeroiPossuiGrupo(int id)
        {

            foreach (Grupo percoreGrupo in Data.Data.grupos)
            {
                var x = percoreGrupo.GrupoHerois.Where(y => y.IdHero == id).Count();
                if(x >= 1)
                {
                    return true;
                }

            }

            return false;
        }


        // alterar metodo para ele nao referenciar a classe Data
        public static void ValidaHeroi(Heroi heroi)
        {

            if (heroi.Identidade == "" || heroi.Nome == "" || heroi.Poder == "")
            {
                throw new Exception("Nome, identidade e Poder são dados obrigatorios!");
            }

            foreach (Heroi heroibusca in Data.Data.heroes)
            {
                if (heroibusca.Nome == heroi.Nome)
                {
                    throw new Exception("Já existe um Heroi adicionado com esse nome, digite novamente!");
                }

            }
        }
    }

    
   
}
