using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WebApplication1.Model;
using WebApplication1.Date;

namespace WebApplication1.Business
{
    public class HeroBusiness
    {
        
        public static IEnumerable<Hero> RetornaHeros()
        {

            return Data.heroes;
        }

        public static Hero RetornaHeroId(int id)
        {
            var hero = Data.heroes.FirstOrDefault(x => x.Id == id);
        
           return hero;
        }

       public static void DeletaHero(int id)
       {

            if (ValidaSeHeroiPossuiGrupo(id))
            {
                throw new Exception("o Heroi não pode ser excluido, pois ele ja participa de um grupo!");
            }

           Data.heroes.Remove(Data.heroes.Where(x => x.Id == id).FirstOrDefault());
       }

        public static bool AtualizaHero(HeroDTO heroDTO)
        {
            var heroAntigo = Data.heroes.FirstOrDefault(x => x.Id == heroDTO.Id);
            if (heroAntigo == null)
            {
                throw new Exception("Heroi que está sendo atualizado não foi encontrado!");
            }

            if(heroAntigo.Tipo != heroDTO.Tipo)
            {
                if (ValidaSeHeroiPossuiGrupo(heroAntigo.Id))
                {
                    throw new Exception("Tipo não pode ser alterado, pois heroi ja esta em um grupo!");
                }

                heroAntigo.Tipo = heroDTO.Tipo;

            }

            if(heroAntigo.Identidade != heroDTO.Identidade)
            {
                throw new Exception("Identidade do Heroi não pode ser alterada!");
            }

            if(heroAntigo.Nome != heroDTO.Nome)
            {
                ValidaHeroi(heroDTO);
            }

            heroAntigo.Nome = heroDTO.Nome;
            heroAntigo.Poder = heroDTO.Poder;

            return true;
           
        }


        public static Hero Criar(HeroDTO heroDTO)
        {

            ValidaHeroi(heroDTO);

            var id = Data.heroes.Count + 1;
            var hero = new Hero(id, heroDTO.Nome, heroDTO.Tipo, heroDTO.Poder, heroDTO.Identidade );

            Data.heroes.Add(hero);
            return hero;

         }


        public static bool ValidaSeHeroiPossuiGrupo(int id)
        {

            foreach (Grupo percoreGrupo in Data.grupos)
            {
                var x = percoreGrupo.ListaHeroi.Where(y => y == id).Count();
                if(x >= 1)
                {
                    return true;
                }

            }

            return false;
        }



        public static void ValidaHeroi(HeroDTO heroDTO)
        {

            if (heroDTO.Identidade == "" || heroDTO.Nome == "" || heroDTO.Poder == "")
            {
                throw new Exception("Nome, identidade e Poder são dados obrigatorios!");
            }

            foreach (Hero heroi in Data.heroes)
            {
                if (heroDTO.Nome == heroi.Nome)
                {
                    throw new Exception("Já existe um Heroi adicionado com esse nome, digite novamente!");
                }

            }
        }
    }

    
   
}
