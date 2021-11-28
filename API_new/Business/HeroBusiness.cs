using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Model;
using WebApplication1.Interfaces;

namespace WebApplication1.Business
{
    public class HeroBusiness : IHeroBusiness
    {
        private readonly ITourOfHeroesRepository _repository;

        public HeroBusiness(ITourOfHeroesRepository tourOfHeroesRepository)
        {
            _repository = tourOfHeroesRepository;
        }


        public IEnumerable<Heroi> GetHerois()
        {
            return _repository.GetHerois();
        }

        public Heroi GetHeroyById(long id)
        {
            return _repository.GetHeroiById(id);
        }

        public Heroi CreateHero(Heroi heroi)
        {
            ValidateNewHero(heroi);
            var newHeroi = _repository.CreateHeroi(new Heroi
            {
                Identidade = heroi.Identidade,
                Idtipo = heroi.Idtipo,
                Nome = heroi.Nome,
                Poder = heroi.Poder
            });
            return newHeroi;
        }

        private void ValidateNewHero(Heroi heroi)
        {
            //* [X] Se nome, nome de heroi, poder, estão preenchidos.
            if (string.IsNullOrWhiteSpace(heroi.Nome) ||
                string.IsNullOrWhiteSpace(heroi.Identidade) ||
                string.IsNullOrWhiteSpace(heroi.Poder))
                throw new HeroiValidationException("Nome, identidade e Poder são dados obrigatorios!");
            //* [X] Se nome de heroi não está repetido.
            var existentHero = _repository.GetHeroiByName(heroi.Nome);
            if (existentHero != null)
                throw new HeroiValidationException($"Já existe um herói com o nome '{heroi.Nome}'");
        }

        public void UpdateHero(Heroi heroi)
        {
            var updHeroi = ValidateUpdateHero(heroi);
            // * [ ] só deixar alterar as seguintes informações: Nome de Heroi, poder e tipo.
            updHeroi.Nome = heroi.Nome;
            updHeroi.Poder = heroi.Poder;
            updHeroi.Idtipo = heroi.Idtipo;
            _repository.UpdateHeroi(updHeroi);
        }


        private Heroi ValidateUpdateHero(Heroi heroi)
        {
            var existentHero = _repository.GetHeroiById(heroi.Id);
            if (existentHero is null)
                throw new HeroiValidationException($"Heroi #{heroi.Id} inexistente");

            // * [X] Só deixar alterar tipo do heroi se ele não estiver vinculado a nenhum grupo.
            if (heroi.Idtipo != existentHero.Idtipo && _repository.GetGruposFromHeroi(heroi.Id).Any())
                throw new HeroiValidationException("Herói não pode alterar tipo se estiver vinculado a um grupo");

            // * [X] Ao alterar nome do heroi, verificar se nome já não existe.
            if (heroi.Nome.Equals(existentHero.Nome, StringComparison.CurrentCultureIgnoreCase)) return existentHero;
            var sameNameHero = _repository.GetHeroiByName(heroi.Nome);
            if (sameNameHero != null)
                throw new HeroiValidationException($"Já existe um herói com o nome '{sameNameHero.Nome}'");

            return existentHero;
        }

        public void DeleteHero(long id)
        {
            var existentHero = ValidateDeleteHero(id);
            _repository.DeleteHeroi(existentHero);
        }

        private Heroi ValidateDeleteHero(long heroiId)
        {
            var existentHero = _repository.GetHeroiById(heroiId);
            if (existentHero is null)
                throw new HeroiValidationException($"Heroi #{heroiId} inexistente");
            // * [X] Se heroi participa de algum grupo, não deixar excluir.
            if (_repository.GetGruposFromHeroi(heroiId).Any())
                throw new HeroiValidationException("Impossível excluir herói participante de grupo");
            return existentHero;
        }
    }

    internal class HeroiValidationException : Exception
    {
        public HeroiValidationException(string message) : base(message)
        {
        }
    }
}