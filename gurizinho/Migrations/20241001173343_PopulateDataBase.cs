using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gurizinho.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "CategoriaId", "Nome", "ImageUrl" },
                values: new object[,]
                {
            { 1, "Eletrônicos", "https://example.com/images/eletronicos.jpg" },
            { 2, "Roupas", "https://example.com/images/roupas.jpg" },
            { 3, "Móveis", "https://example.com/images/moveis.jpg" },
            { 4, "Brinquedos", "https://example.com/images/brinquedos.jpg" },
            { 5, "Alimentos", "https://example.com/images/alimentos.jpg" }
                }
            );

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "ProdutoId", "Nome", "Descricao", "Preco", "ImageUrl", "Estoque", "DataCadastro", "CategoriaID" },
                values: new object[,]
                {
                    { 1, "Smartphone", "Smartphone com tela AMOLED de 6.5 polegadas", 2999.99, "https://example.com/images/smartphone.jpg", 50, DateTime.UtcNow, 1 },
                    { 2, "Camiseta", "Camiseta de algodão 100%", 59.90, "https://example.com/images/camiseta.jpg", 150, DateTime.UtcNow, 2 },
                    { 3, "Sofá", "Sofá de 3 lugares com revestimento de couro", 1999.90, "https://example.com/images/sofa.jpg", 20, DateTime.UtcNow, 3 },
                    { 4, "Boneca", "Boneca com articulações e acessórios", 89.90, "https://example.com/images/boneca.jpg", 100, DateTime.UtcNow, 4 },
                    { 5, "Arroz 5kg", "Pacote de arroz tipo 1", 25.50, "https://example.com/images/arroz.jpg", 300, DateTime.UtcNow, 5 }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DeleteData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValues: new object[] { 1, 2, 3, 4, 5 }
            );

            
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "CategoriaId",
                keyValues: new object[] { 1, 2, 3, 4, 5 }
            );
        }
    }
}
