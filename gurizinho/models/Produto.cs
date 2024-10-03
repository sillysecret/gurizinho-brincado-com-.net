using Microsoft.AspNetCore.Routing.Constraints;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gurizinho.models {

    [Table("Produtos")]
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        [Required]
        //[Column(TypeName ="decimal(10,2)")]
        public double Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }

        public int CategoriaID { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }

    }

}

