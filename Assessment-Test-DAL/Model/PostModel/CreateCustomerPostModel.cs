
namespace Assessment_Test_DAL.Model.PostModel;

public class CreateCustomerPostModel
{
    public string FullName { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public int UserId { get; set; }
}
