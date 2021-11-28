using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Business;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            return HeroBusiness.RetornaHeros().ToArray();


        }

        [HttpGet("{id}")]
        public IActionResult GetHeroId(int id)
        {
            //return HeroBusiness.RetornaHeroId(id).ToArray();

            return Ok(HeroBusiness.RetornaHeroId(id));

        }

        [HttpPost]
        public ActionResult <Hero>Post(HeroDTO heroDTO) 
        {
          
          var hero =  HeroBusiness.Criar(heroDTO);
            return Ok(hero);

        }

        [HttpPut]
        public IActionResult Put(HeroDTO heroDTO)
        {
          
            HeroBusiness.AtualizaHero(heroDTO);
            return Ok();

        }


        [HttpDelete]
        public IActionResult DeletaHeroId(int id)
        {
            HeroBusiness.DeletaHero(id);
            return Ok();
        }

       

    }
}
