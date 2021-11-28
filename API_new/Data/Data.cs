using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    // excluir essa classe
    public class Data
    {
        public static ICollection<Grupo> grupos = new Collection<Grupo>();
        public static ICollection<Heroi> heroes = new Collection<Heroi>();
    }
}
