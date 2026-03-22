namespace ApbdProject1.Models;

public class Projector : Equipment
{
    public int Brightness { get; set; }
    public string Resolution { get; set; } = string.Empty;
    
    public override string ToString() => 
        $"ID: {Id} | [Projector] {Name} ({Producer}, {ProductionYear}) | Resolution: {Resolution}, Brightness: {Brightness} Lumens | {(IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")}";

}