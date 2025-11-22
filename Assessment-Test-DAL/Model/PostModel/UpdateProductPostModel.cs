
namespace Assessment_Test_DAL.Model.PostModel;

public class UpdateProductPostModel
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public string? Sku { get; set; }
    public string? Category { get; set; }
    public bool? IsActive { get; set; }
    // optional image URL
    public string? ImageUrl { get; set; }
}
