using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class HeroDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Tipo Tipo { get; set; }
        public string Poder { get; set; }
        public string Identidade { get; set; }

    }


}
