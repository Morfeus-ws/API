using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Business;
using WebApplication1.Data;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeroController : ControllerBase
    {
        //nem eu sei oque fiz aqui, mas ta fucionando
        private HeroBusiness _heroBusiness = new HeroBusiness(new TourOfHeroesRepository(new TourOfHeroesContexto()));


        [HttpGet]
        public IEnumerable<Heroi> Get()
        {
            return _heroBusiness.RetornaHeros().ToArray();


        }

        [HttpGet("{id}")]
        public IActionResult GetHeroId(int id)
        {
            //return HeroBusiness.RetornaHeroId(id).ToArray();

            return Ok(_heroBusiness.RetornaHeroId(id));

        }

        [HttpPost]
        public ActionResult <Heroi>Post(Heroi heroi) 
        {
          
          var hero = _heroBusiness.Criar(heroi);
            return Ok(hero);

        }

        [HttpPut]
        public IActionResult Put(Heroi heroi)
        {


            _heroBusiness.AtualizaHero(heroi);
            return Ok();

        }

        [HttpDelete]
        public IActionResult DeletaHeroId(int id)
        {
            _heroBusiness.DeletaHero(id);
            return Ok();
        }

       

    }
}
