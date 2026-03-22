using ApbdProject1.Models;

namespace ApbdProject1.Services;

public class RentalSerivce
{
    private readonly List<Rental> _rentals = new();
    private int _nextId = 1;

    public void NewRental(Rental rental)
    {
        rental.Id = _nextId++;
        _rentals.Add(rental);
    }
    
    public List<Rental> GetAllRentals() => _rentals;
    public Rental GetRentalById(int id) => _rentals.First(r => r.Id == id);
    
    public void ReturnEquipment(int id) => _rentals.First(r => r.Id == id).ReturnDate = DateTime.Now;
    
    
}