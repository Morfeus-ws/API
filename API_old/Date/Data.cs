using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;
using WebApplication1.Model;

namespace WebApplication1.Date
{
    public class Data
    {
        public static ICollection<Grupo> grupos = new Collection<Grupo>();
        public static ICollection<Hero> heroes = new Collection<Hero>();
    }
}
