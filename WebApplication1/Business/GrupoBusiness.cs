using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WebApplication1.Application;
using WebApplication1.Model;
using WebApplication1.Data;
using WebApplication1.Business;



namespace WebApplication1.Business
{
    public class GrupoBusiness
    {
        private  TourOfHeroesRepositorio _tourOfHeroRepoitory; 

        public GrupoBusiness(TourOfHeroesRepositorio tourOfHeroRepoitory)
        {
            _tourOfHeroRepoitory = tourOfHeroRepoitory;

        }

        public  List<Grupo> RetornaGrupos()
        {

            return _tourOfHeroRepoitory.BuscarGrupos();
        }

        public Grupo RetornaGrupoId(int id)
        {

            var grupo = _tourOfHeroRepoitory.BuscarGrupos().FirstOrDefault(x => x.Id == id);

            return grupo;
        }

        public void DeletaGrupo(int id)
        {

            //if (ValidaSeHeroiPossuiGrupo(id))
            //{
            //    throw new Exception("o Heroi não pode ser excluido, pois ele ja participa de um grupo!");
            //}


            _tourOfHeroRepoitory.ExcluirGrupo(id);
        }

        public void AtualizaGrupo(GrupoPostDTO grupoDTO)
        {
            // nao ta validando se grupo está vazio para poder alterar o tipo dele

            if (!_tourOfHeroRepoitory.BuscarGrupo(grupoDTO.Id))
            {
                throw new Exception("Grupo não existe");

            }

            // nao ta validando se o tipo dos herois adicionados é o mesmo tipo do grupo
            List<HeroiGrupo> lista = new List<HeroiGrupo>();

            grupoDTO.Lista.ForEach(x =>
            {
                var heroigrupo = new HeroiGrupo(grupoDTO.Id, x);

                lista.Add(heroigrupo);
            });

            var grupo = new Grupo(grupoDTO.Id, grupoDTO.Nome, grupoDTO.Idtipo, lista);

            
            _tourOfHeroRepoitory.AlteraGrupo(grupo);



        }




        public Grupo CriarGrupo(GrupoPostDTO grupoDTO)
        {
 

            // Remover referencia a classe Data
            var id = Data.Data.grupos.Count + 1;
            List<int> heroGrupo = new List<int>();

            ValidaGrupo(grupoDTO);

            if (!ValidaSeIdHeroIgualIDGrupo(grupoDTO))
            {
                throw new Exception("Tipo do Heroi adicionado não corresponde ao tipo do Grupo!");
            }

            

            if (ValidaIdHeroRepeteNoGrupo(grupoDTO)){
                throw new Exception("Heroi já esta incluido no grupo!");
            }

            List<HeroiGrupo> lista = new List<HeroiGrupo>();
            grupoDTO.Lista.ForEach(idHeroi => lista.Add(new HeroiGrupo(id, idHeroi)));
           

            var grupo = new Grupo(0, grupoDTO.Nome, grupoDTO.Idtipo, lista);

            _tourOfHeroRepoitory.AdicionarGrupo(grupo);
    

            return grupo;
        }


        // remover referencia a classe Data
        public void ValidaGrupo(GrupoPostDTO grupoPostDTO)
        {
            if (grupoPostDTO.Nome == "")
            {
                throw new Exception("Nome não esta preenchido!");
            }
            foreach(Grupo grupo in Data.Data.grupos)
            {
                if(grupoPostDTO.Nome == grupo.Nome)
                {
                    throw new Exception("Nome do grupo já existe");
                }
            
            }
        }



        public bool ValidaIdHeroRepeteNoGrupo(GrupoPostDTO grupoPostDTO)
        {
            for(int i = 1; i < grupoPostDTO.Lista.Count; i++)
            {
                var x = grupoPostDTO.Lista.Where(y => y == grupoPostDTO.Lista[i]).Count();
                if (x > 1)
                {
                    return true;
                }

            }
                
            return false;
        }

        // Esse metodo deveria estar sendo usado no Adicionar e no Alterar, pra verificar se todos os herois informados como
        // integrantes do grupo são herois validos
        private bool ValidaHeros(List<int> idHerois)
        {
            var Hero = new HeroBusiness(_tourOfHeroRepoitory);
            foreach (var idHeroi in idHerois)
            {

                var hero = Hero.RetornaHeroId(idHeroi);
                if (hero == null)
                {
                    throw new Exception("Heroi adicionado no Grupo, não encontrado!");
                }

            }

            return true;

        }

        // alterar metodo para remover referencia à classe Data e comparar o tipo do heroi com o tipo do grupo
        // trocar nome do metodo para ficar mais correto e facil de entender o que o metodo faz
        public bool ValidaSeIdHeroIgualIDGrupo(GrupoPostDTO grupoPostDTO)
        {
            foreach(Heroi PercoreHeros in Data.Data.heroes)
            {


               foreach(int PercoreHeroID in grupoPostDTO.Lista)
               {
                    if(PercoreHeros.Id == PercoreHeroID)
                    {
                    
                    }
               }
            }
            return true;

        }
         

     }

}

