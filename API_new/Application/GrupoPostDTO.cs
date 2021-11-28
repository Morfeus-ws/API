using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Application
{
    public class GrupoPostDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public int Idtipo { get; set; }
        public List<int> Lista { get; set; }


    }
}
