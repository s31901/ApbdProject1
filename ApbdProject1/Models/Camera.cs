namespace ApbdProject1.Models;

public class Camera : Equipment
{
    public double ResolutionMegapixels { get; set; }
    public bool IncludesSdCard { get; set; }
    
    public override string ToString() => 
        $"ID: {Id} | [Camera] {Name} ({Producer}, {ProductionYear}) | Resolution: {ResolutionMegapixels}MP, With SD card: {(IncludesSdCard ? "YES" : "NO")} | {(IsAvailable ? "AVAILABLE" : "NOT AVAILABLE")}";

}