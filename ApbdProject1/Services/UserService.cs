using ApbdProject1.Models;

namespace ApbdProject1.Services;

public class UserService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public void AddUser(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
    }
    public List<User> GetAllUsers() => _users;
    public User GetUserById(int id) => _users.First(u => u.Id == id);
}