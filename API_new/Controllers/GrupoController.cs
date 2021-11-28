using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Application;
using WebApplication1.Model;
using WebApplication1.Interfaces;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GrupoController : ControllerBase
    {
        private readonly IGrupoBusiness _grupoBusiness;

        public GrupoController(IGrupoBusiness grupoBusiness)
        {
            _grupoBusiness = grupoBusiness;
        }

        [HttpGet]
        public IEnumerable<Grupo> Get()
        {
            return _grupoBusiness.GetGrupos();
        }

        [HttpGet("{id:int}")]
        public ActionResult<Grupo> GetGrupoId(int id)
        {
            var grupo = _grupoBusiness.GetGrupoById(id);
            if (grupo is null)
            {
                return NotFound();
            }

            return Ok(grupo);
        }

        [HttpPost]
        public ActionResult<Grupo> Post(GrupoPostDTO grupoDto)
        {
            var grupo = _grupoBusiness.CreateGrupo(grupoDto);
            return Ok(grupo);
        }

        [HttpPut]
        public IActionResult Put(GrupoPostDTO grupoDto)
        {
            _grupoBusiness.UpdateGrupo(grupoDto);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletaGrupoId(int id)
        {
            _grupoBusiness.DeleteGrupo(id);
            return Ok();
        }
    }
}