using gurizinho.models;
using gurizinho.Pagination;

namespace gurizinho.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
        PagedList<Produto> GetProdutosFiltroPreco(FiltoDeProdutoPorPreco produtosFiltroParams);
        PagedList<Produto> GetProdutosPage(PageProdutoParameters produtosParams);
        //IEnumerable<Produto> GetProdutosPage(PageProdutoParameters prm);
    }
}
