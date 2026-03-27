using ApbdProject1.Services;
using ApbdProject1.Models;

namespace ApbdProject1.UI;

public class ConsoleMenu
{
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    private readonly RentalService _rentalService;

    public ConsoleMenu(EquipmentService equipmentService, UserService userService, RentalService rentalService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
    }

    public void Run()
    {
        ShowMenu();
        while (true)
        {
            Console.Write("\n> ");
            var input = Console.ReadLine();
            switch (input)
            {
                case "0": return;
                case "1": AddUser(); break;
                case "2": AddEquipment(); break;
                case "3": ShowAllEquipment(); break;
                case "4": ShowAvailableEquipment(); break;
                case "5": NewRental(); break;
                case "6": ReturnEquipment(); break;
                case "7": MarkOutOfOrder(); break;
                case "8": MarkAvailable(); break;
                case "9": ShowActiveRentalsForUser(); break;
                case "10": ShowOverdueActiveRentals(); break;
                case "11": GenerateReport(); break;
                default: Console.WriteLine("Unknown option"); break;
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("Initializing service...");
        Console.WriteLine("Welcome in RENTAL SYSTEM");
        Console.WriteLine("Choose action:");
        Console.WriteLine("0. Close application");
        Console.WriteLine("1. Add new user");
        Console.WriteLine("2. Add new equipment");
        Console.WriteLine("3. Show all equipment");
        Console.WriteLine("4. Show available equipment");
        Console.WriteLine("5. New rental");
        Console.WriteLine("6. Return equipment");
        Console.WriteLine("7. Mark equipment as out of order");
        Console.WriteLine("8. Mark equipment as not out of order");
        Console.WriteLine("9. Show active rentals for a user");
        Console.WriteLine("10. Show current overdue rentals");
        Console.WriteLine("11. Generate report");
    }

    private void ShowAllEquipment()
    {
        Console.WriteLine("\nAll equipment:");
        foreach (var e in _equipmentService.GetAllEquipment())
        {
            Console.WriteLine(e);
        }
    }

    private void ShowAvailableEquipment()
    {
        Console.WriteLine("\nAvailable equipment:");
        foreach (var e in _equipmentService.GetAvailableEquipment())
        {
            Console.WriteLine(e);
        }
    }

    private void ShowAllUsers()
    {
        Console.WriteLine("\nAll users:");
        foreach (var u in _userService.GetAllUsers())
        {
            Console.WriteLine(u);
        }
    }

    private void ShowRentals()
    {
        Console.WriteLine("\nAll rentals:");
        foreach (var r in _rentalService.GetAllRentals())
        {
            Console.WriteLine();
            Console.WriteLine(r);
            Console.WriteLine("**************");
        }
    }

    private void ShowUserActiveRentals(int userId)
    {
        Console.WriteLine($"\nActive rentals for user id: {userId}:");
        foreach (var r in _rentalService.GetAllRentals().Where(r => r.User.Id == userId && !r.IsReturned))
        {
            Console.WriteLine();
            Console.WriteLine(r);
            Console.WriteLine("**************");
        }
    }

    private void ShowOverdueActiveRentals()
    {
        Console.WriteLine("Active rentals that are overdue:");
        foreach (var r in _rentalService.GetAllRentals().Where(r => r.IsOverdue))
        {
            Console.WriteLine();
            Console.WriteLine(r);
            Console.WriteLine("**************");
        }
    }

    private void GenerateReport()
    {
        Console.WriteLine("\n*******************");
        Console.WriteLine("*SUMMARY REPORT*");
        Console.WriteLine($"Total number of equipment: {_equipmentService.GetNumberOfEquipment()}");
        Console.WriteLine($"Number of available equipment: {_equipmentService.GetNumberOfAvailableEquipment()}");
        Console.WriteLine($"Number of users: {_userService.GetNumberOfUsers()}");
        Console.WriteLine($"Total number of rentals: {_rentalService.GetNumberOfRentals()}");
        Console.WriteLine($"Total number of active rentals: {_rentalService.GetNumberOfActiveRentals()}");
        Console.WriteLine($"Total number of overdue rentals: {_rentalService.GetNumberOfOverdueRentals()}");
        Console.WriteLine("*******************\n");
    }

    private void AddUser()
    {
        Console.Write("Name: ");
        var name = Console.ReadLine()!;
        Console.Write("Surname: ");
        var surname = Console.ReadLine()!;
        Console.Write("Type (1 - Student, 2 - Employee): ");
        var type = Console.ReadLine();

        User user = type == "1"
            ? new User(name, surname, UserType.Student)
            : new User(name, surname, UserType.Employee);

        _userService.AddUser(user);
        Console.WriteLine("User added.");
    }

    private void AddEquipment()
    {
        Console.Write("Type (1 - Laptop, 2 - Projector, 3 - Camera): ");
        var type = Console.ReadLine();
        Console.Write("Name: ");
        var name = Console.ReadLine()!;
        Console.Write("Producer: ");
        var producer = Console.ReadLine()!;
        Console.Write("Production year: ");
        var year = int.Parse(Console.ReadLine()!);

        Equipment equipment = type switch
        {
            "1" => CreateLaptop(name, producer, year),
            "2" => CreateProjector(name, producer, year),
            "3" => CreateCamera(name, producer, year),
            _ => throw new ArgumentException("Unknown type")
        };

        _equipmentService.AddEquipment(equipment);
        Console.WriteLine("Equipment added.");
    }

    private Laptop CreateLaptop(string name, string producer, int year)
    {
        Console.Write("RAM (GB): ");
        var ram = int.Parse(Console.ReadLine()!);
        Console.Write("Screen size: ");
        var screen = double.Parse(Console.ReadLine()!);
        return new Laptop(name, producer, year, ram, screen);
    }

    private Projector CreateProjector(string name, string producer, int year)
    {
        Console.Write("Brightness (lm): ");
        var brightness = int.Parse(Console.ReadLine()!);
        Console.Write("Resolution: ");
        var resolution = Console.ReadLine()!;
        return new Projector(name, producer, year, brightness, resolution);
    }

    private Camera CreateCamera(string name, string producer, int year)
    {
        Console.Write("Resolution (MP): ");
        var mp = double.Parse(Console.ReadLine()!);
        Console.Write("Includes SD card? (y/n): ");
        var sd = Console.ReadLine() == "y";
        return new Camera(name, producer, year, mp, sd);
    }
    private void NewRental()
    {
        ShowAllUsers();
        Console.Write("User ID: ");
        var userId = int.Parse(Console.ReadLine()!);
        var user = _userService.GetUserById(userId);

        ShowAvailableEquipment();
        Console.Write("Equipment ID: ");
        var equipId = int.Parse(Console.ReadLine()!);
        var equipment = _equipmentService.GetEquipmentById(equipId);

        Console.Write("Duration (days): ");
        var days = int.Parse(Console.ReadLine()!);

        _rentalService.NewRental(new Rental(equipment, user, DateTime.Now, days));
    }

    private void ReturnEquipment()
    {
        ShowRentals();
        Console.Write("Rental ID: ");
        var id = int.Parse(Console.ReadLine()!);
        _rentalService.ReturnEquipment(id);
    }

    private void MarkOutOfOrder()
    {
        ShowAllEquipment();
        Console.Write("Equipment ID: ");
        var id = int.Parse(Console.ReadLine()!);
        _equipmentService.MarkEquipmentAsOutOfOrder(id);
    }

    private void MarkAvailable()
    {
        ShowAllEquipment();
        Console.Write("Equipment ID: ");
        var id = int.Parse(Console.ReadLine()!);
        _equipmentService.MarkEquipmentAsNotOutOfOrder(id);
    }

    private void ShowActiveRentalsForUser()
    {
        ShowAllUsers();
        Console.Write("User ID: ");
        var id = int.Parse(Console.ReadLine()!);
        ShowUserActiveRentals(id);
    }
}