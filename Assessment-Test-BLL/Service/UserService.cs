
using Assessment_Test_BLL.IService;
using Assessment_Test_BLL.Security;
using Assessment_Test_DAL.Model.Entities;
using Assessment_Test_DAL.Utils;
using Microsoft.EntityFrameworkCore;

namespace Assessment_Test_BLL.Service;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _db;
    private readonly IJwtService _jwt;

    public UserService(ApplicationDbContext db, IJwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public Task<string> AuthenticateAsync(string username, string password)
    {
        var user = _db.Users.SingleOrDefault(u => u.Username == username);
        if (user == null)
            return Task.FromResult<string>(null);

        if (user.PasswordHash != password)
            return Task.FromResult<string>(null);

        var token = _jwt.GenerateToken(user.Id, username);
        return Task.FromResult(token);
    }

    public async Task<User> GetProfile(string username, string password)
    {
        var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == username);
        return user;
    }
}
