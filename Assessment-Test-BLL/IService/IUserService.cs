
using Assessment_Test_DAL.Model.Entities;

namespace Assessment_Test_BLL.IService;

public interface IUserService
{
    Task<string> AuthenticateAsync(string username, string password);
    Task<User> GetProfile(string username, string password);
}
