using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Application;
using WebApplication1.Business;
using WebApplication1.Model;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrupoController : ControllerBase
    {

        private GrupoBusiness _grupoBusiness = new GrupoBusiness(new TourOfHeroesRepositorio( new TourOfHeroesContexto()));

        [HttpGet]
        public IEnumerable<Grupo> Get()
        {
            return _grupoBusiness.RetornaGrupos().ToArray();


        }

        [HttpGet("{id}")]
        public IActionResult GetGrupoId(int id)
        {
            //return HeroBusiness.RetornaHeroId(id).ToArray();

            return base.Ok(_grupoBusiness.RetornaGrupoId(id));

        }

        [HttpPost]
        public ActionResult <Grupo> Post(GrupoPostDTO grupoDTO)
        {

            
            var grupo = _grupoBusiness.CriarGrupo(grupoDTO);
            return Ok(grupo);


        }

        [HttpPut]
        public IActionResult Put(GrupoPostDTO grupoDTO)
        {

            _grupoBusiness.AtualizaGrupo(grupoDTO);
            return Ok();

        }

        [HttpDelete]
        public IActionResult DeletaGrupoId(int id)
        {
            _grupoBusiness.DeletaGrupo(id);
            return Ok();
        }


    }
}
