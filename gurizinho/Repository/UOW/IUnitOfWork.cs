namespace gurizinho.Repository.UFW
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; } 
        ICategoriaRepository CategoriaRepository { get; }

        Task commitAsync();
    }
}
