using System.Collections.Generic;
using WebApplication1.Application;
using WebApplication1.Model;

namespace WebApplication1.Interfaces
{
    public interface IGrupoBusiness
    {
        IEnumerable<Grupo> GetGrupos();
        Grupo GetGrupoById(int id);
        Grupo CreateGrupo(GrupoPostDTO grupoDto);
        void UpdateGrupo(GrupoPostDTO grupoDto);
        void DeleteGrupo(int id);
    }
}