public class Item
{
    public int Weight { get; }

    public bool IsCarryable { get; private set; }

    public string Name { get; }

    public string Description { get; }

    public string ShortDescription { get; private set; }



    public int Uses { get; private set; }
    public int HealAmount { get; private set; }

    public Item(int weight, string name, string description, bool isCarryable = true)
    {
        Weight = weight;
        Name = name;
        Description = description;
        IsCarryable = isCarryable;
        Uses = 0;
        HealAmount = 0;
    }

public Item(int weight, string name, string shortDescription, int uses, int healAmount)
: this(weight, name, shortDescription)
    {
        ShortDescription = shortDescription;
        Uses = uses;
        HealAmount = healAmount;
    }


public bool IsConsumable() => Uses > 0;
      


public int Consume()
    {
        if (!IsConsumable())
        return 0;

        Uses--;
        return HealAmount;
    }



public string GetUseDescription()
{
if (!IsConsumable()) 
return Description.Trim();


string useWord = Uses == 1 ? "use" : "uses";
return $"{Description.Trim()} ({Uses} {useWord} left)";
}








}
