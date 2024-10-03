using gurizinho.Context;

namespace gurizinho.Repository.UFW
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository produtoRepository;

        private ICategoriaRepository categoriaRepository;

        public ApiContext apiContext;

        public UnitOfWork(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        public IProdutoRepository ProdutoRepository 
        {
            get 
            {
                return produtoRepository = produtoRepository ?? new ProdutoRepository(apiContext); 
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get 
            {
                return categoriaRepository = categoriaRepository ?? new CategoriaRepository(apiContext);
            }
        }

        public void commit()
        {
            apiContext.SaveChanges();
        }
        public void dispose() 
        {
            apiContext.Dispose();
        }
    }
}
