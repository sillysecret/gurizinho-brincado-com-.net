using gurizinho.models;
using gurizinho.Pagination;
using X.PagedList;

namespace gurizinho.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
        Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(FiltoDeProdutoPorPreco produtosFiltroParams);
        Task<IPagedList<Produto>> GetProdutosPageAsync(PageProdutoParameters produtosParams);
        
    }
}
