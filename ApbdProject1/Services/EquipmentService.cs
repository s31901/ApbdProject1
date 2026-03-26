using ApbdProject1.Models;

namespace ApbdProject1.Services;

public class EquipmentService
{
    private readonly List<Equipment> _equipment = new();
    private int _nextId = 1;
    
    public void AddEquipment(Equipment equipment)
    {
        equipment.Id = _nextId++;
        _equipment.Add(equipment);
    }
    
    public List<Equipment> GetAllEquipment() => _equipment;
    public Equipment GetEquipmentById(int id) => _equipment.First(e => e.Id == id);
    public List<Equipment> GetAvailableEquipment() => _equipment.Where(e => e.IsAvailable).ToList();
    public void MarkEquipmentAsNotOutOfOrder(int id) => _equipment.First(e => e.Id == id).IsOutOfOrder = false;
    public void MarkEquipmentAsOutOfOrder(int id)
    {
        if (_equipment.First(e => e.Id == id).IsAvailable) _equipment.First(e => e.Id == id).IsOutOfOrder = true;
        else Console.WriteLine("Equipment is rented. Cannot mark as out of order.");
        
    } 
    public int GetNumberOfEquipment() => _equipment.Count;
    public int GetNumberOfAvailableEquipment() => _equipment.Count(e => e.IsAvailable);
}