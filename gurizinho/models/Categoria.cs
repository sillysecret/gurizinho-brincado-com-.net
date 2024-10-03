using System.Collections.Generic; 
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace gurizinho.models;

//opcional | mas acho melhor desse jeito ja que eu posso fazer marções para o orm sem alterar o context, ao meu ver 
//isso é melhor para ficar mais escalavel depois ou prenivir que tu esqueca de colar no context
[Table("Categorias")]
public class Categoria
{
    public Categoria()
    {
        produtos = new Collection<Produto>();
    }

    [Key]//mesmos pontos do [Table("")] posso customizar as chaves (escrever variavel com maiusculo ta dando agonia)
    public int CategoriaId { get; set; }

    [Required]
    [StringLength(50)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(300)]
    public string? ImageUrl { get; set; }

    [JsonIgnore]
    public ICollection<Produto> produtos { get; set; } 
}
    

