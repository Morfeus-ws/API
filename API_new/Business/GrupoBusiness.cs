using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Application;
using WebApplication1.Model;
using WebApplication1.Interfaces;


namespace WebApplication1.Business
{
    public class GrupoBusiness : IGrupoBusiness
    {
        private readonly ITourOfHeroesRepository _repository;

        public GrupoBusiness(ITourOfHeroesRepository repository)
        {
            _repository = repository;
        }

        
        public IEnumerable<Grupo> GetGrupos()
        {
            return _repository.GetAllGrupos();
        }

        public Grupo GetGrupoById(int id)
        {
            return _repository.GetGrupoById(id);
        }

        /// <summary>
        /// Criar grupo
        /// </summary>
        /// <param name="grupoDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public Grupo CreateGrupo(GrupoPostDTO grupoDto)
        {
            var herois = ValidateNewGrupo(grupoDto);
            var newGrupo = _repository.CreateGrupo(new Grupo
            {
                Idtipo = grupoDto.Idtipo,
                Nome = grupoDto.Nome,
            });
            if (newGrupo is null)
            {
                throw new Exception("Grupo não foi criado adequadamente");
            }

            _repository.AssignHerosToGroup(herois, newGrupo);

            return _repository.GetGrupoById(newGrupo.Id);
        }

        public void UpdateGrupo(GrupoPostDTO grupoDto)
        {
            var (grupo, herois) = ValidateUpdateGrupo(grupoDto);
            grupo.Nome = grupoDto.Nome;
            grupo.Idtipo = grupoDto.Idtipo;
            var enumerable = herois as Heroi[] ?? herois.ToArray();
            _repository.UpdateGrupo(grupo, enumerable);
            _repository.AssignHerosToGroup(enumerable, grupo);
        }

        public void DeleteGrupo(int id)
        {
            var grupo = _repository.GetGrupoById(id);
            if (grupo.GrupoHerois.Any())
                throw new Exception("Grupo não pode ser excluído, pois ele ainda contem heróis");

            _repository.DeleteGrupo(grupo);
        }


        private (Grupo, IEnumerable<Heroi>) ValidateUpdateGrupo(GrupoPostDTO grupoDto)
        {
            var grupo = _repository.GetGrupoById(grupoDto.Id);
            if (grupo is null)
            {
                throw new GrupoValidationException("Group not found");
            }

            //* [X] Só deixar alterar o tipo do grupo se não tiver nenhum integrante.
            if (grupo.Idtipo != grupoDto.Idtipo && grupo.GrupoHerois.Count > 0)
            {
                throw new GrupoValidationException("Group must be empty for change type");
            }

            // * [X] Ao alterar o nome do grupo, verificar se não já não existe.            
            if (!grupo.Nome.Equals(grupoDto.Nome, StringComparison.CurrentCultureIgnoreCase))
            {
                var anotherGroup = _repository.GetGrupoByName(grupoDto.Nome);
                if (anotherGroup != null)
                {
                    throw new GrupoValidationException($"Group with name '{anotherGroup.Nome}' exists");
                }
            }

            // * [ ] Verificar se ids dos herois informados existem e se não estão repetidos
            var herois = _repository.GetHeroisById(grupoDto.Lista);
            return (grupo, herois);
        }

        private IEnumerable<Heroi> ValidateNewGrupo(GrupoPostDTO grupoDto)
        {
            // * [X] Nome e devem estar preenchidos
            if (string.IsNullOrWhiteSpace(grupoDto.Nome) ||
                grupoDto.Idtipo < 0)
            {
                throw new GrupoValidationException("Missing data: nome, idtipo");
            }

            // * [X] os integrantes devem ter mesmo tipo que o grupo.
            var herois = _repository.GetHeroisById(grupoDto.Lista);

            foreach (var heroi in herois)
            {
                if (heroi.Idtipo != grupoDto.Idtipo)
                {
                    throw new GrupoValidationException(
                        $"Expected tipo '{grupoDto.Idtipo}' for heroi #{heroi.Id} = '{heroi.Idtipo}'");
                }
            }

            // * [ ] Verificar se ids dos herois informados existem e se não estão repetidos


            // * [X] Nome do grupo não pode ser repetido.
            var grupo = _repository.GetGrupoByName(grupoDto.Nome);
            if (grupo != null)
            {
                throw new GrupoValidationException($"Group '{grupoDto.Nome}' just exists");
            }

            return herois;
        }
    }

    internal class GrupoValidationException : Exception
    {
        public GrupoValidationException(string message) : base(message)
        {
        }
    }
}