
namespace Assessment_Test_DAL.Model.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Sku { get; set; } = null!;
    public string Category { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    // optional image URL (nullable)
    public string? ImageUrl { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
}
