namespace ApbdProject1.Models;

public class Laptop : Equipment
{
    public int RamSizeGb { get; set; }
    public double ScreenSize { get; set; }
    
    public override string ToString() => 
        $"ID: {Id} | [Laptop] {Name} ({Producer}, {ProductionYear}) | RAM: {RamSizeGb}GB, {ScreenSize}\" | {(IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")}";
    
}