using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Model
{
    public class HeroDTO
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public long IdTipo { get; set; }
        public string Poder { get; set; }
        public string Identidade { get; set; }

    }


}
