
namespace Assessment_Test_DAL.Model.PostModel;

public class CreateProductPostModel
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Sku { get; set; } = null!;
    public string Category { get; set; } = null!;
    // optional image URL
    public string? ImageUrl { get; set; }
}
