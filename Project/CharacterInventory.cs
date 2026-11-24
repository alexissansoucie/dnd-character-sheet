public class CharacterInventory : IListed
{
    public int Id { get; set; }

    public int YourRecordId { get; set; }

    public string Name { get; private set; }

    public string Type { get; private set; }

    public double Weight { get; private set; }

    public string Description { get; private set; }

    public string Damage { get; private set; }

    public int Quantity { get; private set; }

    public double TotalWeight => Weight * Quantity;

    public string Kind => "INVENTORY";

    public string Line() => $"{Name} ({Type}) x{Quantity} {TotalWeight}";

    public CharacterInventory(string name)
    {
        Name = name;
        Type = "Misc";
        Weight = 0.0;
        Description = string.Empty;
        Damage = "1d6";
        Quantity = 1;
    }

    public CharacterInventory(
        string name,
        string type,
        double weight,
        string description,
        string damage,
        int quantity)
    {
        Name = name;
        Type = type;
        Weight = weight;
        Description = description;
        Damage = damage;
        Quantity = quantity;
    }
}