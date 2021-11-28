using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WebApplication1.Application;
using WebApplication1.Model;
using WebApplication1.Date;


namespace WebApplication1.Business
{
    public class GrupoBusiness
    {


        public static IEnumerable<Grupo> RetornaGrupos()
        {

            return Data.grupos;
        }

        public static Grupo RetornaGrupoId(int id)
        {
            var grupo = Data.grupos.FirstOrDefault(x => x.Id == id);

            return grupo;
        }

        public static void DeletaGrupo(int id)
        {
            var x = Data.grupos.Remove(Data.grupos.Where(x => x.Id == id).FirstOrDefault());


        }

        public static bool AtualizaGrupo(GrupoPostDTO grupoDTO)
        {

            var grupoAntigo = Data.grupos.FirstOrDefault(x => x.Id == grupoDTO.Id);
            if (grupoAntigo == null)
            {
                throw new Exception("Grupo não encontrado!");
            }

            ValidaHeros(grupoDTO.Lista);

            if (grupoAntigo.Nome != grupoDTO.Nome)
            {
                ValidaGrupo(grupoDTO);

                grupoAntigo.Nome = grupoDTO.Nome;

            }

            if( grupoAntigo.Tipo != grupoDTO.Tipo ) 
            {

                if (grupoAntigo.ListaHeroi.Any())
                {
                    throw new Exception("O tipo do grupo não pode ser alterado, pois ja possui integrantes!");
                }
            }

            if(ValidaIdHeroRepeteNoGrupo(grupoDTO))
            {
                throw new Exception("Integrante já está no grupo");
            }
          

          
            grupoAntigo.ListaHeroi = grupoDTO.Lista;
            grupoAntigo.Tipo = grupoDTO.Tipo;


            return true;

        }


        public static Grupo CriarGrupo(GrupoPostDTO grupoDTO)
        {
            

            var id = Data.grupos.Count + 1;
            List<int> heroGrupo = new List<int>();

            ValidaGrupo(grupoDTO);

            if (!ValidaSeIdHeroIgualIDGrupo(grupoDTO))
            {
                throw new Exception("Tipo do Heroi adicionado não corresponde ao tipo do Grupo!");
            }

            if (grupoDTO.Lista.Count > 0)
            {
                ValidaHeros(grupoDTO.Lista);
                heroGrupo = grupoDTO.Lista;

            }
            

            if (ValidaIdHeroRepeteNoGrupo(grupoDTO)){
                throw new Exception("Heroi já esta incluido no grupo!");
            }


            var grupo = new Grupo(id, heroGrupo, grupoDTO.Nome, grupoDTO.Tipo);
            Data.grupos.Add(grupo);

            return grupo;
        }


        // verifica se os herois foram cadastrados

        private static bool ValidaHeros(List<int> idHerois)
        {
            foreach (var idHeroi in idHerois)
            {

                var hero = HeroBusiness.RetornaHeroId(idHeroi);
                if (hero == null)
                {
                    throw new Exception("Heroi adicionado no Grupo não encontrado!");
                }
     
            }

            return true;

        }


        public static void ValidaGrupo(GrupoPostDTO grupoPostDTO)
        {
            if (grupoPostDTO.Nome == "")
            {
                throw new Exception("Nome não esta preenchido!");
            }
            foreach(Grupo grupo in Data.grupos)
            {
                if(grupoPostDTO.Nome == grupo.Nome)
                {
                    throw new Exception("Nome do grupo já existe");
                }
            
            }
        }



        public static bool ValidaIdHeroRepeteNoGrupo(GrupoPostDTO grupoPostDTO)
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


        public static bool ValidaSeIdHeroIgualIDGrupo(GrupoPostDTO grupoPostDTO)
        {
            foreach(Hero PercoreHeros in Data.heroes)
            {


               foreach(int PercoreHeroID in grupoPostDTO.Lista)
               {
                    if(PercoreHeros.Id == PercoreHeroID)
                    {
                       if(PercoreHeros.Tipo != grupoPostDTO.Tipo)
                       {
                          return false;
                       }
                    }
               }
            }
            return true;

        }
         

     }

}

