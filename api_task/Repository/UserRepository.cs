using api_task.DataAccess;
using api_task.Interface;
using api_task.Models;
using Microsoft.EntityFrameworkCore;

namespace api_task.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddUser(User newUser)
    {
        _dbContext.Users.Add(newUser);
        _dbContext.SaveChanges();
    }

    public IEnumerable<User>? GetUserList() => _dbContext.Users.OrderBy(u => u.Username);

    public void UpdateUser(User updatedUser)
    {
        _dbContext.Users.Attach(updatedUser);
        _dbContext.Entry(updatedUser).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public User FindUser(User user)
    {
        return _dbContext.Users.FirstOrDefault(u => u.UserId == user.UserId);
    }

    public void CleanTable()
    {
        foreach (var user in _dbContext.Users)
        {
            _dbContext.Users.Remove(user);
        }
        _dbContext.SaveChanges();
    }
}