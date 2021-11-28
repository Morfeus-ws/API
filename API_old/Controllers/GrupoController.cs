using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Application;
using WebApplication1.Business;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrupoController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<Grupo> Get()
        {
            return GrupoBusiness.RetornaGrupos().ToArray();


        }

        [HttpGet("{id}")]
        public IActionResult GetGrupoId(int id)
        {
            //return HeroBusiness.RetornaHeroId(id).ToArray();

            return Ok(GrupoBusiness.RetornaGrupoId(id));

        }

        [HttpPost]
        public ActionResult <Grupo> Post(GrupoPostDTO grupoDTO)
        {

            
            var grupo = GrupoBusiness.CriarGrupo(grupoDTO);
            return Ok(grupo);


        }

        [HttpPut]
        public IActionResult Put(GrupoPostDTO grupoDTO)
        {

            GrupoBusiness.AtualizaGrupo(grupoDTO);
            return Ok();

        }

        [HttpDelete]
        public IActionResult DeletaGrupoId(int id)
        {
            GrupoBusiness.DeletaGrupo(id);
            return Ok();
        }


    }
}
