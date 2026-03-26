namespace ApbdProject1.Models;

public abstract class Equipment
{
    public int Id { get; set; }
    public string Name { get;} = string.Empty;
    public bool IsAvailable { get; set;} = true;
    public string Producer { get;} = string.Empty;
    public int ProductionYear { get; set;}
    public bool IsOutOfOrder { get; set;} = false;

    protected Equipment(
        string name,  string producer , int productionYear, bool isOutOfOrder)
    {
        Name = name;
        Producer = producer;
        ProductionYear = productionYear;
        IsOutOfOrder = isOutOfOrder;
    }
}
    