namespace ApbdProject1.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; } = string.Empty;
    public string Surname { get; } = string.Empty;
    public UserType Usertype { get; set; }
    
    public User(string name, string surname, UserType usertype)
    {
        Name = name;
        Surname = surname;
        Usertype = usertype;
    }
    
    public override string ToString() => $"ID: {Id} | {Name} {Surname} | {Usertype}";
}