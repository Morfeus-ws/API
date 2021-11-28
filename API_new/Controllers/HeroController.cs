using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.Interfaces;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroController : ControllerBase
    {
        private readonly IHeroBusiness _heroBusiness;

        public HeroController(IHeroBusiness heroBusiness)
        {
            _heroBusiness = heroBusiness;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Heroi>> Get()
        {
            return Ok(_heroBusiness.GetHerois());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetHeroId(int id)
        {
            return Ok(_heroBusiness.GetHeroyById(id));
        }

        [HttpPost]
        public ActionResult<Heroi> Post(Heroi heroi)
        {
            var hero = _heroBusiness.CreateHero(heroi);
            return Ok(hero);
        }

        [HttpPut]
        public IActionResult Put(Heroi heroi)
        {
            _heroBusiness.UpdateHero(heroi);
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeletaHeroId(int id)
        {
            _heroBusiness.DeleteHero(id);
            return Ok();
        }
    }
}