using DataAnnotationsExtensions;
using gurizinho.models;
using System.ComponentModel.DataAnnotations;

namespace gurizinho.DTOs
{
    public class ProdutoCreateDTO
    {
        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [StringLength(50)]
        public string? Nome { get; set; }

        [Required(ErrorMessage ="Campo Descrição é obrigatório")]
        [StringLength(300,ErrorMessage = "deve ter no maximo 300 de tamanho")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Campo Preço é obrigatório")]
        public double Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Campo Estoque é obrigatório")]
        [Min(0, ErrorMessage = "Quantidade de estoque deve ser no minimo 0")]
        public float Estoque { get; set; }

        [Required(ErrorMessage = "Campo CategoriaId é obrigatório")]
        public int CategoriaID { get; set; }

    }
}
