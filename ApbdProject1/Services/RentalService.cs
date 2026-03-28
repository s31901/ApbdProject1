using ApbdProject1.Config;
using ApbdProject1.Models;

namespace ApbdProject1.Services;

public class RentalService
{
    private readonly List<Rental> _rentals = new();
    private int _nextId = 1;

    public void NewRental(Rental rental)
    {
        if (!rental.Equipment.IsAvailable)
        {
            Console.WriteLine("Equipment is already rented");
        }
        else if (rental.Equipment.IsOutOfOrder)
        {
            Console.WriteLine("Equipment is out of order (being repaired)");
        }
        else if (!IsRentalPossibleForUser(rental.User))
        {
            Console.WriteLine("Rental limit for user reached");
        }
        else
        {
            rental.Equipment.IsAvailable = false;
            rental.Id = _nextId++;
            _rentals.Add(rental);
            Console.WriteLine($"Rental created: {rental.Id}, {rental.Equipment.Name}, {rental.User.Name}");
        }
    }
    
    public List<Rental> GetAllRentals() => _rentals;
    public Rental GetRentalById(int id) => _rentals.First(r => r.Id == id);

    public void ReturnEquipment(int id)
    {   
        var rental = _rentals.First(r => r.Id == id);
    
        if (rental.IsReturned)
        {
            Console.WriteLine("Equipment is already returned.");
            return;
        }

        rental.ReturnDate = DateTime.Now;
        rental.Equipment.IsAvailable = true;

        if (rental.CalculatedFine > 0)
            Console.WriteLine($"Rental is overdue. Fine: {rental.CalculatedFine:C}");
        else
            Console.WriteLine("Equipment returned on time.");

    } 
    
    public int GetActiveRentalsForUser(User user) => _rentals.Count(r => r.User.Id == user.Id && !r.IsReturned);

    public bool IsRentalPossibleForUser(User user)
    {
        int max = 0;
        switch (user.Usertype)
        {
            case UserType.Employee: 
                max = RentalPolicy.EmployeeMaxRentals;
                break;
            case UserType.Student: 
                max = RentalPolicy.StudentMaxRentals;
                break;
        }
        return GetActiveRentalsForUser(user) < max;
    }
    public int GetNumberOfRentals() => _rentals.Count;
    public int GetNumberOfActiveRentals() => _rentals.Count(r => !r.IsReturned);
    public int GetNumberOfOverdueRentals() => _rentals.Count(r => r.IsOverdue);
    public void ShiftRentalDateBack(int id, int daysAgo)
    {
        var rental = _rentals.First(r => r.Id == id);
        rental.RentalDate = DateTime.Now.AddDays(-daysAgo);
    }
}