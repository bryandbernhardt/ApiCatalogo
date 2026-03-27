namespace ApiCatalogo.Pagination;

public class ProductsFilterByPrice : PaginationParameters
{
    public decimal? Price { get; set; }
    public string? CriterionPrice { get; set; } // greater smaller equal
}