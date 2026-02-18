using System;
using System.Collections.Generic;
using System.Linq;

class Room
{
	// Private fields
	

	private string description;
	public Chest Chest { get; private set; }

	    private List<Item> items;


	private Dictionary<string, Room> exits;// stores exits of this room.
    private Dictionary<string, Room> hiddenExits;




	public bool IsWinningRoom { get; set; } 

	public bool Visited { get; set; } = false;

	public NPC Npc { get; private set; }


	public string Description => description;


	

 
public Room(string description)
{
	this.description = description;
exits = new Dictionary<string, Room>();
hiddenExits = new Dictionary<string, Room>();

items = new List<Item>();

IsWinningRoom = false;
Chest = null;

}



	// Define an exit for this room.
	public void AddExit(string direction, Room room)
	{
		exits[direction.ToLower()] = room;
	}
	
	
	public void AddHiddenExit(string codeWord, Room room)
{
	hiddenExits[codeWord.ToLower()] = room;
}

	// Return the description of the room.
	public string GetShortDescription() => description;


	
public void GetLongDescriptionWithExits()
{

Console.Write("You are in ");
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine(this.Description);
Console.ResetColor();

if (Chest != null)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("\nThere is a chest here");
			Console.ResetColor();
		}



    // Items in the room
    if (items.Count > 0)
		{
			Console.WriteLine("\nItems here:");
			foreach (var item in items)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(" - " + item.Description);
				Console.ResetColor();
				if (!string.IsNullOrWhiteSpace(item.ShortDescription))
				Console.WriteLine();
				else
				Console.WriteLine();
			}
		}

    // NPC in the room
    if (Npc != null)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"\nYou see {Npc.Name} here.");
			Console.ResetColor();
		}

    // Exits with destinations
    if (exits.Count > 0)
    {
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Magenta;
		Console.WriteLine("Exits:");
		Console.ResetColor();



		foreach (var exit in exits)
			{
				Console.ForegroundColor = ConsoleColor.Magenta;
				Console.Write($" - {exit.Key}");
				Console.ResetColor();
				Console.WriteLine($" leads to {exit.Value.Description}");
			}
	
Console.WriteLine();

	}

}
	

	public Room GetExit(string direction)
	{

direction = direction.ToLower();

		if (exits.ContainsKey(direction)) 
		return exits[direction];


		if (hiddenExits.ContainsKey(direction)) 
		return hiddenExits[direction];


		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	private string GetExitString()
	{
		if (exits.Count == 0)
		return "There are no visible exits.";

		return "Exits: " + string.Join(", ", exits.Keys);
	}

public string GetDescription() => description;


public void AddItem(Item item) 
{
	if (item == null) return;
	items.Add(item);

}


public Item RemoveItem(string name) 
{
	if (string.IsNullOrWhiteSpace(name))
	return null;

	Item foundItem = null;

	foreach (Item item in items)
		{
			if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
			{
				foundItem = item;
				break;
			}
		}

		if (foundItem == null)
		return null;

		items.Remove(foundItem);
		return foundItem;

}

public string GetItemDescriptions()
{
	if (items.Count == 0) 
	return "There are no items here.";


var itemNames = items.Select(i => i.GetUseDescription().Trim()).ToList();


string result;

if (itemNames.Count == 1)
		{
			result = $"There is a {itemNames[0]} here.";
		}
		else if (itemNames.Count == 2)
		{
			result = $"There is {itemNames[0]} and a {itemNames[1]} here.";
		}
		else
		{
			string allButLast = string.Join(", ", itemNames.Take(itemNames.Count - 1));
			string last = itemNames.Last();
			result = $"There is {allButLast}, and {last} here.";
		}

		return result;

}




public void AddChest(Chest chest)
{
	Chest = chest;
}

public Item GetItem(string itemName)
	{
		foreach (Item item in items)
		{
			if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
			return item;
		}

		return null;
	}


public void AddNPC(NPC npc)
	{
		Npc = npc;
	}


public List<string> GetExitNames()
	{
		return new List<string>(exits.Keys);
	}


public List<string> GetUnvisitedExitNames()
	{
		List<string> unvisited = new List<string>();

		foreach (var exit in exits)
		{
			if (!exit.Value.Visited)
			{
				unvisited.Add(exit.Key);
			}
		}
		return unvisited;
	}


}
