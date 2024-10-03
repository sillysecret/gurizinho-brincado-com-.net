using gurizinho.models;
using gurizinho.Pagination;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace gurizinho.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategoriaPage(PageCategoriaParameters categoriaParams);

        PagedList<Categoria> GetCategoriaPorNome(FiltroCategoriaPorNome categoriaParams);
    }
}
