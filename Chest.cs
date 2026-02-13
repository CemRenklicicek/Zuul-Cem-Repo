using System;
using System.Collections.Generic;
using System.Linq;

class Chest
{
private List<Item> items;

public Chest()
{
items = new List<Item>();
}


    public void AddItem(Item item)
    {
        if (item == null) return;
        items.Add(item);
    }

public Item RemoveItem(string name)
{
  if (string.IsNullOrWhiteSpace(name) || items == null || items.Count == 0)
  return null;

Item found = items.FirstOrDefault(i => i != null && 
!string.IsNullOrWhiteSpace(i.Name) &&
i.Name.Equals(name, StringComparison.OrdinalIgnoreCase)

);

  if (found != null)
  items.Remove(found);

   return found;

}

public string GetItemDescriptions()
{
  if (items == null || items.Count == 0)
  return "empty";

  return string.Join(", ", items.Where(i => i != null).Select(i => i.Description));
}


public List<Item> GetItems()
  {
    return items;
  }

}
