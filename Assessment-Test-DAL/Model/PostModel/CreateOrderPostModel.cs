
namespace Assessment_Test_DAL.Model.PostModel;

public class CreateOrderPostModel
{
    public int CustomerId { get; set; }
    public ICollection<CreateOrderItemPostModel> Items { get; set; } = new List<CreateOrderItemPostModel>();
}
