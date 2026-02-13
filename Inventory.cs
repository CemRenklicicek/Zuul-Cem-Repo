using System;
using System.Collections.Generic;
using System.Linq;


class Inventory
{
    private int maxWeight;
    private Dictionary<string, Item> items;


public Inventory(int maxWeight)
{
    this.maxWeight = maxWeight;
    items = new Dictionary<string, Item>();
}

public bool CanPut(Item item)
    {
        return item.Weight <= FreeWeight();
    }


public bool Put(Item item)
{

if (item == null) return false;

if (item.Weight > FreeWeight()) return false;

string key = item.Name.ToLower();
items[key] = item;
return true;

}


public bool Remove(Item item)
    {
        if (item == null) return false;

        string key = item.Name.ToLower();
        return items.Remove(item.Name);
    }

public Item Get(string name)
{

if (string.IsNullOrWhiteSpace(name)) 
return null;

items.TryGetValue(name, out Item item);
return item;

}




public int TotalWeight()
{
return items.Values.Sum(i => i.Weight);
}

public int FreeWeight()
{
    return maxWeight - TotalWeight();
}




public string GetItemDescriptions()
{
    if (items.Count == 0)
    return "You are carrying nothing.";

    return string.Join(", ", items.Values.Select(i => i.GetUseDescription()));
}


public string GetInventoryStatus()
    {
        if (items.Count == 0)
        return $"Inventory ({TotalWeight()}/{maxWeight}); empty";


string list = string.Join(", ", items.Values.Select(i => i.GetUseDescription()));
return $"Inventory ({TotalWeight()}/{maxWeight}): {list}";

    }

}
