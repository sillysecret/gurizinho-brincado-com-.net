using gurizinho.Context;
using gurizinho.models;
using gurizinho.Pagination;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System;
using X.PagedList;

namespace gurizinho.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApiContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();
                
            return produtos.Where(c => c.CategoriaID == id);
        }

        public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(FiltoDeProdutoPorPreco produtosFiltroParams)
        {
            var produtos = await GetAllAsync();

            if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
            {
                if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco > produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco < produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
                else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                {
                    produtos = produtos.Where(p => p.Preco == produtosFiltroParams.Preco.Value).OrderBy(p => p.Preco);
                }
            }
            var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

            return produtosFiltrados;
        }


        public async Task<IPagedList<Produto>> GetProdutosPageAsync(PageProdutoParameters produtosParameters)
        {
            //IQueryable<T> é apropriado quando você deseja realizar consultas de forma
            //eficiente em uma fonte de dados que pode ser consultada diretamente, como
            //um banco de dados. Ele suporta a consulta diferida e permite que as
            //consultas sejam traduzidas em consultas SQL eficientes quando você está
            //trabalhando com um provedor de banco de dados, como o Entity Framework.
            //------------------------------------------------------------------------
            //IEnumerable<T> é uma interface mais geral que representa uma coleção de
            //objetos em memória. Ela não oferece suporte a consultas diferidas ou tradução
            //de consultas SQL. Isso significa que, ao usar IEnumerable, você primeiro traz
            //todos os dados para a memória e, em seguida, aplica consultas, o que pode ser
            //menos eficiente para grandes conjuntos de dados.
            var produtos = await GetAllAsync();

            var orderProdutos = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

            var produtosOrdenados = await orderProdutos.ToPagedListAsync(produtosParameters.PageNumber, produtosParameters.PageSize);

            return produtosOrdenados;
        }

    }
}
