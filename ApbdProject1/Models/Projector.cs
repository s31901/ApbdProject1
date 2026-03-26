namespace ApbdProject1.Models;

public class Projector : Equipment
{
    public int Brightness { get; set; }
    public string Resolution { get; set; } = string.Empty;
    
    public Projector(string name, string producer, int productionYear, int brightness, string resolution) : base( name, producer, productionYear, false)
    {
        Brightness = brightness;
        Resolution = resolution;
    }
    
    public override string ToString() => 
        $"ID: {Id} | [Projector] {Name} ({Producer}, {ProductionYear}) | Resolution: {Resolution}, Brightness: {Brightness} Lumens | {(IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")} | {(IsOutOfOrder ? "OUT OF ORDER" : "IN ORDER")}";

}