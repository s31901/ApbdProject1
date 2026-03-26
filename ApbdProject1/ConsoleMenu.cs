using ApbdProject1.Services;

namespace ApbdProject1;

public class ConsoleMenu
{
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;
    private readonly RentalSerivce _rentalService;
    public ConsoleMenu(EquipmentService equipmentService, UserService userService, RentalSerivce rentalService)
    {
        _equipmentService = equipmentService;
        _userService = userService;
        _rentalService = rentalService;
    }
    
    public void ShowAllEquipment()
    {
        Console.WriteLine("\nAll equipment:");
        foreach (var e in _equipmentService.GetAllEquipment())
        {
            Console.WriteLine(e);
        }
    }
    
    public void ShowAvailableEquipment()
    {   
        Console.WriteLine("\nAvailable equipment:");
        foreach (var e in _equipmentService.GetAvailableEquipment())
        {
            Console.WriteLine(e);
        }
    }

    public void ShowAllUsers()
    {
        Console.WriteLine("\nAll users:");
        foreach (var u in _userService.GetAllUsers())
        {
            Console.WriteLine(u);
        }
    }
    public void ShowRentals()
    {
        Console.WriteLine("\nAll rentals:");
        foreach (var r in _rentalService.GetAllRentals())
        {   Console.WriteLine();
            Console.WriteLine(r);
            Console.WriteLine("**************");
        }
    }
    
    public void ShowUserActiveRentals(int userId)
    {
        Console.WriteLine($"\nActive rentals for user id: {userId}:");
        foreach (var r in _rentalService.GetAllRentals().Where(r => r.User.Id == userId && !r.IsReturned))
        {   
            Console.WriteLine();
            Console.WriteLine(r);
            Console.WriteLine("**************");
        }
    }

    public void ShowOverdueActiveRentals()
    {
        Console.WriteLine("Active rentals that are overdue:");
        foreach (var r in _rentalService.GetAllRentals().Where(r => r.IsOverdue))
        {
            Console.WriteLine();
            Console.WriteLine(r);
            Console.WriteLine("**************");
        }
    }
    
    public void GenerateReport()
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
    
}