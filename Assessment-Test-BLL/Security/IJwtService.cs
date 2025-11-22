
namespace Assessment_Test_BLL.Security;

public interface IJwtService
{
    string GenerateToken(int userId, string username);
}
