namespace ApbdProject1.Models;

public abstract class Equipment
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsAvailable { get; set; }
    public string Producer { get; set; } = string.Empty;
    public int ProductionYear { get; set; }
}
    