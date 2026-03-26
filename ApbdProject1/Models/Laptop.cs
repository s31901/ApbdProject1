namespace ApbdProject1.Models;

public class Laptop : Equipment
{
    public int RamSizeGb { get; set; }
    public double ScreenSize { get;}
    
    public Laptop(string name, string producer, int productionYear, int ramSizeGb, double screenSize) : base(name, producer, productionYear, false)
    {
        RamSizeGb = ramSizeGb;
        ScreenSize = screenSize;
    }
    public override string ToString() => 
        $"ID: {Id} | [Laptop] {Name} ({Producer}, {ProductionYear}) | RAM: {RamSizeGb}GB, {ScreenSize}\" | {(IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")} | {(IsOutOfOrder ? "OUT OF ORDER" : "IN ORDER")}";
    
}