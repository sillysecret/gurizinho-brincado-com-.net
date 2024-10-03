using NuGet.Versioning;

namespace gurizinho.Pagination
{
    public class FiltoDeProdutoPorPreco : QueryStringParameters
    {
        public double? Preco { get; set; }
        public string? PrecoCriterio { get; set; } // "maior", "menor" ou "igual"


    }
}
