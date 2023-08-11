using api_task.Models;

namespace api_task.Interface;

public interface IUserRepository
{
    IEnumerable<User>? GetUserList();

    void AddUser(User newUser);

    void UpdateUser(User updatedUser);

    User FindUser(User user);

    void CleanTable();
}