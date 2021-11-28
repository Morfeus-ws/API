using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Business;

namespace WebApplication1.Model
{
    public class Grupo
    {
        public Grupo(int id, List<int> listaHeroi, string nome, Tipo tipo)
        {
            Id = id;
            ListaHeroi = listaHeroi;
            Nome = nome;
            Tipo = tipo;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public Tipo Tipo { get; set; }
        public List<int> ListaHeroi { get; set; }

      
     
    }
}   
