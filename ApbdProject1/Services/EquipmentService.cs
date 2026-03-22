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
    public void MarkEquipmentAsUnavailable(int id) => _equipment.First(e => e.Id == id).IsAvailable = false;
    public void MarkEquipmentAsAvailable(int id) => _equipment.First(e => e.Id == id).IsAvailable = true;
}