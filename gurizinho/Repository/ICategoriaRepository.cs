using gurizinho.models;
using gurizinho.Pagination;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using X.PagedList;

namespace gurizinho.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IPagedList<Categoria>> GetCategoriaPageAsync(PageCategoriaParameters categoriaParams);

        Task<IPagedList<Categoria>> GetCategoriaPorNomeAsync(FiltroCategoriaPorNome categoriaParams);
    }
}
