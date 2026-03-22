using ApbdProject1.Config;

namespace ApbdProject1.Models;

public class Rental
{
    public int Id { get; }
    public Equipment Equipment { get; private set; }
    public User User { get; private set; }

    public DateTime RentalDate { get; private set; }
    public int RentalDurationDays { get; private set; }
    public DateTime DueDate => RentalDate.AddDays(RentalDurationDays);
    public DateTime? ReturnDate { get; set; }

    public bool IsReturned => ReturnDate.HasValue;
    public bool IsOverdue => !IsReturned && DateTime.Now > DueDate;

    public decimal CalculatedFine
    {
        get
        {
            if (!IsReturned || ReturnDate!.Value <= DueDate) return 0;
            return (ReturnDate.Value - DueDate).Days * RentalPolicy.DailyFineRate;
        }
    }

    public Rental(int id, Equipment equipment, User user, DateTime rentalDate, int durationDays)
    {
        Id = id;
        Equipment = equipment ?? throw new ArgumentNullException(nameof(equipment));
        User = user ?? throw new ArgumentNullException(nameof(user));
        RentalDate = rentalDate;
        RentalDurationDays = durationDays > 0
            ? durationDays
            : throw new ArgumentException("Duration must be positive", nameof(durationDays));
    }
}
