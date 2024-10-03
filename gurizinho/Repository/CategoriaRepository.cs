using gurizinho.Context;
using gurizinho.models;
using gurizinho.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace gurizinho.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApiContext context) : base(context)
        {
        }

        public PagedList<Categoria> GetCategoriaPage(PageCategoriaParameters categoriaParameters)
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
            var categorias = GetAll().OrderBy(p => p.CategoriaId).AsQueryable();

            var categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias,
                       categoriaParameters.PageNumber, categoriaParameters.PageSize);

            return categoriasOrdenados;
        }

        public PagedList<Categoria> GetCategoriaPorNome(FiltroCategoriaPorNome categoriaParams)
        {
            var categorias = GetAll().AsQueryable();

            if (!string.IsNullOrEmpty(categoriaParams.Nome)) 
            {
                categorias = categorias.Where(categoria => categoria.Nome.ToLower().Contains(categoriaParams.Nome.ToLower()));
            }


            return PagedList<Categoria>.ToPagedList(categorias, categoriaParams.PageNumber, categoriaParams.PageSize);
        }
    }
}
