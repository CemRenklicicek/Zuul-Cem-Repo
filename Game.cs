using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	public Room CurrentRoom { get; set; }

	private bool gameWon = false;

	private bool coinGiven = false;

	private bool playerDied = false;

	private int restartCount = 0;



    // Constructor
    public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	
private void RestartGame()

{
Console.Clear();

restartCount++;
parser = new Parser();
player = new Player();
gameWon = false;

CreateRooms();


}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room lobby = new Room("the hotel's main lobby");
		Room theatre = new Room("the theatre");
		Room pub = new Room("the hotel's pub");
		Room suite = new Room("the celebrity suite");
		Room restaurant = new Room("the restaurant");
		Room library = new Room("the hotel library");
		Room hallway = new Room("the main hall");
		Room bedroom = new Room("the bedroom of the suite");
		Room lab = new Room("the sacred laboratory");

		// Initialise room exits
		lobby.AddExit("east", restaurant);
		lobby.AddExit("west", pub);
		lobby.AddExit("north", hallway);

		theatre.AddExit("south", hallway);

		pub.AddExit("south", lobby);
		pub.AddExit("north", theatre);

		suite.AddExit("down", hallway);
		suite.AddExit("west", bedroom);

		hallway.AddExit("up", suite);
		hallway.AddExit("west", theatre);
		hallway.AddExit("east", library);
		hallway.AddExit("south", lobby);

		bedroom.AddExit("south", suite);
		bedroom.AddExit("west", lab);

		lab.AddExit("south", bedroom);
		lab.AddExit("down", library);


		restaurant.AddExit("south", lobby);
		restaurant.AddExit("north", pub);




		Room golden = new Room("in the secret garden");
        golden.IsWinningRoom = true;
		library.AddHiddenExit("zula", golden);

		library.AddExit("west", hallway);
	


Item emerald = new Item(3, "emerald", "a beautiful green emerald");
Item orange = new Item(1, "orange", "a orange", 2, 10);
Item beer = new Item(2, "beer", "a glass of beer", 1, 10);
Item burger = new Item(1, "burger", "a fresh hamburger", 1, 15);
Item milkshake = new Item(2, "milkshake", "a strawberry milkshake", 5, 15);
Item armour = new Item(7, "armor", "some protective armour");
Item potion = new Item(1, "potion", "a energy potion", 6, 25);
Item popcorn = new Item(2, "popcorn", "a bag of popcorn", 3, 15);
Item cookie = new Item(1, "cookie", "a chocolate chip cookie", 2, 7);
Item painting = new Item(5, "painting", "a painting of Van Gogh");
Item recordPlayer = new Item(7, "record player", "an old record player. Maybe you can play a record on it", false);

Item amethyst = new Item(4, "amethyst", "a shining amethyst");
Item record = new Item(1, "record", "a vinyl record");

Chest suiteChest = new Chest();
suite.AddChest(suiteChest);

suite.Chest.AddItem(record);
suite.Chest.AddItem(amethyst);

bedroom.AddItem(emerald);
pub.AddItem(orange);
restaurant.AddItem(burger);
library.AddItem(armour);
theatre.AddItem(popcorn);
hallway.AddItem(painting);
lab.AddItem(potion);
pub.AddItem(beer);
pub.AddItem(milkshake);
theatre.AddItem(cookie);
suite.AddItem(recordPlayer);

NPC waiter = new NPC(
    "a waiter",
    "You talk to a waiter who stands at the other end of the room"
);

// Add multiple dialogue options
waiter.AddOption("Compliment the food.", player =>
{
    Console.WriteLine();
	Console.WriteLine("Waiter: \"Thank you! The chef will be delighted\"");
		Console.WriteLine(); 

});
waiter.AddOption("Make small talk.", player =>
{
    Console.WriteLine();
	Console.WriteLine("Waiter: \"Busy day, huh? Hope you're enjoying the hotel\"");
		Console.WriteLine(); 

});
waiter.AddOption("Ask about hidden rooms.", player =>
{
    Console.WriteLine();
	Console.WriteLine("Waiter: \"Hmm… some secrets are best left undiscovered\"");
		Console.WriteLine(); 

});
waiter.AddOption("Ask about history", player =>
{
    Console.WriteLine();
	Console.WriteLine("Waiter: \"The hotel originally opened in 1905, and got renovated in 1995\"");
	Console.WriteLine(); 
});

restaurant.AddNPC(waiter);


		// Create your Items here
		// ...
		// And add them to the Rooms
		// ...

		// Start game outside
		player.CurrentRoom = lobby;
		player.health = player.maxHealth;
	
	}

	//  Main play routine. Loops until end of play.
public void Play()
	{
		PrintWelcome();
   player.CurrentRoom.GetLongDescriptionWithExits();

bool finished = false;
while (!finished)
{


Command command = parser.GetCommand();

Console.WriteLine();


if (gameWon)
{
		if (command.CommandWord == "restart")
		{
					Console.Clear();
					HandleRestart();
					continue;
		}

		else if (command.CommandWord == "quit")

		{
		finished = true;
		break;
		}
	    else
		{
			Console.WriteLine("You've won! Type restart to play again, or enter to quit");
			Console.WriteLine();
            continue;
		}	

	}


finished = ProcessCommand(command);


if (player.CurrentRoom.IsWinningRoom)
			{
			Console.WriteLine("You've managed to find the Fountain of Healing, a lost artificact");
			Console.WriteLine("You've been heavily awarded and won");
			Console.WriteLine();
			Console.WriteLine("Press enter to exit, or type restart to play again.");
			Console.WriteLine();
         	gameWon = true;
			continue; 
			}








if (command.CommandWord == "restart")
			{
				Console.Clear();
				HandleRestart();
				continue;
			}




            if (player.health <= 0)
			{
				Console.WriteLine("You've explored long enough. Time to go elsewhere");
				Console.WriteLine("Type restart or quit whether you want to return or leave (restart/quit)");
				Console.WriteLine();
				playerDied = true;

				continue;


			}


		}
		
		Console.WriteLine("Thank you for playing");
		Console.WriteLine("Press [Enter] to continue");
		Console.ReadLine();

	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		if (player == null)
		return;

        Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Welcome to the Zula Hotel");
		Console.WriteLine();
		Console.ResetColor();
		Console.WriteLine("Feel free to explore, though your have to keep track of your stamina");
		Console.WriteLine("There are also some interesting items scattered throughout");
		Console.WriteLine();
	}







	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			Console.WriteLine();
			return wantToQuit; // false
		}

		switch (command.CommandWord)
		{
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "look":
				PrintLook(command);
				break;
			case "status":
			    seeStatus(command);
				break;
			case "take":
			    Take(command);
				break;
			case "drop":
			    Drop(command);
				break;
			case "talk":
			Talk(command);
			     break;
			case "open":
			    OpenChest(command);
				break;
			case "use":
			UseItem(command);
			break;


			case "quit":
				wantToQuit = true;
				break;
		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	private void PrintLook(Command command)
	{
				player.CurrentRoom.GetLongDescriptionWithExits();
	}


    public void seeStatus(Command command)

	{
		Console.WriteLine(player.GetInventory());
		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"Your status right now is {player.health}");
		Console.WriteLine();
		Console.ResetColor();
	}
		

private void Take(Command command)
{
    if (!command.HasSecondWord())
    {
        Console.WriteLine("Take what?");
        return;
    }

    string itemName = command.SecondWord.ToLower();
    Item item = null;
    bool fromChest = false;

    if (player.CurrentRoom.Chest != null)
    {
        item = player.CurrentRoom.Chest.GetItems()
               .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        if (item != null) fromChest = true;
    }

    if (item == null)
    {
        item = player.CurrentRoom.GetItem(itemName);
    }

    // If still not found, tell the player
    if (item == null)
    {
        Console.WriteLine("That item isn't in this room");
        Console.WriteLine();
        return;
    }

    // Check if the item can be carried
    if (!item.IsCarryable)
    {
        Console.WriteLine("That item is too big for you to carry");
        Console.WriteLine();
        return;
    }

    if (!player.AddItem(item))
    {
        Console.WriteLine("Your inventory is full. You must drop something first");
        return;
    }

    if (fromChest)
        player.CurrentRoom.Chest.RemoveItem(item.Name);
    else
        player.CurrentRoom.RemoveItem(item.Name);

    Console.WriteLine($"You have taken {item.Description}");
	Console.WriteLine();



if (item.Name.ToLower() == "painting")
		{
			Console.WriteLine("The back of it says 'Secret Garden'");
			Console.WriteLine("What does it mean? And where?");
			Console.WriteLine();
		}



}

private void Drop(Command command)
{
	if (!command.HasSecondWord())
	{
		Console.WriteLine("What do you want to drop?");
		return;
	}

	string itemName = command.SecondWord.ToLower();
	Item item = player.RemoveItem(itemName);
	
	if (item == null)
	{
		Console.WriteLine("Umm, you don't have that item.");
		Console.WriteLine();
		return;
	}

	player.CurrentRoom.AddItem(item);
	Console.WriteLine($"You have dropped {item.Description}.");
	Console.WriteLine();


}




	private void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;
		Room nextRoom = player.CurrentRoom.GetExit(direction);

		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			Console.WriteLine();
			return;
		}

		player.CurrentRoom = nextRoom;
		player.CurrentRoom.Visited = true;
		player.Damage(11);
		Console.ForegroundColor = ConsoleColor.Green;
	    Console.WriteLine($"Your stamina is now at {player.health}");
		Console.ResetColor();
		Console.WriteLine();

		player.CurrentRoom.GetLongDescriptionWithExits();





if (!nextRoom.IsWinningRoom)
		{
			Random rand = new Random();
			if (rand.Next(3) == 0)
			{
				RandomHotelEvent();
			}
		}


	if (nextRoom.IsWinningRoom)
	gameWon = true;
	
	
	}


private void OpenChest(Command command)
	{
		Chest chest = player.CurrentRoom.Chest;

		if (chest == null)
		{
			Console.WriteLine("There is no chest here");
			return;
		}

		if (!command.HasSecondWord() || command.SecondWord.ToLower() == "chest")
		{
			var itemsinChest = chest.GetItems().ToList();
			if (itemsinChest.Count == 0)
			{
				Console.WriteLine("You opened the chest, but it is empty");
				return;
			}

			string itemNames;
			if (itemsinChest.Count == 1)
			{
				itemNames = itemsinChest[0].Description;
			}
			else if (itemsinChest.Count == 2)
			{
				itemNames = $"{itemsinChest[0].Description} and {itemsinChest[1].Description}";
			}
			else
			{
				itemNames = string.Join(", ", itemsinChest.Take(itemsinChest.Count - 1).Select(i => i.Description))
				+ $", and {itemsinChest.Last().Description}";
			}

			Console.WriteLine($"You've opened the chest, and took {itemNames} from it");

			foreach (var chestItem in itemsinChest)
			{
				if (player.AddItem(chestItem))
				{
					chest.RemoveItem(chestItem.Name);
				}
				else
				{
					Console.WriteLine($"Your inventory is full. You can't take the {chestItem.Description} right now.");
				
				}
			}
			Console.WriteLine();
			return;
		}

string itemName = command.SecondWord.ToLower();
Item singleItem = chest.RemoveItem(itemName);

if (singleItem == null)
		{
			Console.WriteLine("That item isn't in the chest");
			Console.WriteLine();
			return;
		}

		if (!player.AddItem(singleItem))
		{
			Console.WriteLine("Your inventory is full. Drop something first");
			chest.AddItem(singleItem);
			return;
		}

		Console.WriteLine($"You've opened the chest, and have taken {singleItem.Description} from it");
		Console.WriteLine();

	}



private void UseItem(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Use what?");
			return;
		}


	string itemName = command.SecondWord.ToLower();
    Item item = player.GetItem(itemName);

if (item == null)
		{
			Console.WriteLine("Umm, you don't have that item");
			Console.WriteLine();
			return;
		}



if (item.Name == "beer")
		{
			Console.WriteLine("You drink a big sip of beer. It tastes yucky");
			player.Damage(15);
			Console.WriteLine($"Your stamina has been drained and is now {player.health}");
			Console.WriteLine();
			player.RemoveItem("beer");
			return;
		}

if (item.Name == "coin")
		{
			UseCoin();
			return;
		}

if (item.Name == "record")
{
	Item recordPlayer = player.CurrentRoom.GetItem("record player");
	if (recordPlayer != null)
		{
			Console.WriteLine("You play the record on the record player. An angelic voice whispers:");
			Console.WriteLine("'There is something secretive hidden in the library...'");
			Console.WriteLine();
			
		}
			else
			{
				Console.WriteLine("There is no record player here to play anything on");
				Console.WriteLine();
			}
	return;

}

if (item.IsConsumable())
		{
			int effect = item.Consume();

			if (effect >= 0)
			{
				Console.WriteLine($"You have consumed the {item.Name}. You recovered {effect}% stamina");
				Console.ResetColor();
				Console.WriteLine();
				player.Heal(effect);

			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"You comsume {item.Name}. It drains {-effect}% stamina");
				Console.ResetColor();
				Console.WriteLine();
				player.Damage(-effect);

			}

if (item.Uses == 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"You've used everything up");
				Console.ResetColor();
				Console.WriteLine();
				player.RemoveItem(item.Name);
			}

Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Your stamina is now {player.health}");
			Console.ResetColor();
			Console.WriteLine();
			return;


		}

	Console.WriteLine($"You can't really use that right now");
	Console.WriteLine();


	}



private void UseCoin()
	{

Item coin = player.GetItem("coin");
if (coin == null)
		{
			Console.WriteLine("I'm afraid you don't have a coin to use");
			return;
		}

Room current = player.CurrentRoom;

switch (current.Description.ToLower())
		{
			case "the hotel's main lobby":
			Console.WriteLine("You toss the tiny coin into the fountain in the lobby");
			Console.WriteLine($"You feel slightly refreshed and recieved 5% stamina");
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"Your stamina is now {player.health}");
			Console.ResetColor();

			Console.WriteLine();
			player.Heal(10);
			break;


			case "the hotel library":
			Console.WriteLine("You toss the coin and it lands on an old book. A secret passage opens");
			Room secretRoom = current.GetExit("zula");
			if (secretRoom != null)
				{
					Console.WriteLine("Type 'go zula' to enter the passage");
					Console.WriteLine();
				}
				break;


		default:
		Console.WriteLine("You spin the coin around, but it just briefly changes color");
		Console.WriteLine();
		break;
		}


	}



private void HandleRestart()
	{

		Random rand = new Random();

		string previousRoom = player.CurrentRoom.Description.ToLower();
		

		if (previousRoom == "the hotel's main lobby" && restartCount == 0)
		{
string[] lobbyStartMessages =
			{
				"Um, you've barely begun, but i guess we can reset things",
				"You are already in the lobby. How about we explore first",
				"Pretty boring, ugh",
				"You just came here, but you still type restart",
			};

						Console.WriteLine(lobbyStartMessages[rand.Next(lobbyStartMessages.Length)]);
						Console.WriteLine();
		}
		else if (gameWon)

{

			string[] winRestartMessages =
			{
				"The hotel resets, but your triumph remains with you",
				"You feel a small breeze, as if the secret garden put an element within you",
				"A note appears before you: 'Ready for another adventure?'",
				"The hotel is going to have a slightly different vibe",
				"Watch out for someone like Bowser of Eggman this time",
				"Okay, you come back, but your past victory will be noted",
				"Will there be something new, or something more vicious?",

			};

			Console.WriteLine(winRestartMessages[rand.Next(winRestartMessages.Length)]);
			Console.WriteLine();


		}

		

else if (playerDied)
		{
			string[] deathRestartMessages =
			{
				"You return, but the hotel looks like it didn't forgot you",
				"You feel like something dragged you back",
				"You look at the same group of images again. That means you're back at the same place",
				"You've missed out on something. Can you find it now?",
			};

Console.WriteLine(deathRestartMessages[rand.Next(deathRestartMessages.Length)]);
Console.WriteLine();

		}

		else

		{


			string[] regularMessages = {
			"Everything resets! Everything will be back to square one",
			"Snore",
			"Yawn",
			"Back to the start we go! Maybe watch your step this time",
			"'Uh-oh. Who pressed Ctrl+Z on me?' the waiter asked",
			"Something tells me you got pushed back to the start. But by who?"
		};

		Console.WriteLine(regularMessages[rand.Next(regularMessages.Length)]);
		Console.WriteLine();
		}
	


		Console.Write("Restarting");
		for (int i = 0; i < 6; i++)
		{
			Console.Write(".");
			System.Threading.Thread.Sleep(1100);
		}

		RestartGame();

if (previousRoom != "the hotel's main lobby" && !coinGiven)

{
        Item tinyCoin = new Item(1, "coin", "a shiny tiny coin");
		player.AddItem(tinyCoin);
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("A tiny coin was miraclously given to you");
		Console.WriteLine();
		Console.ResetColor();

		coinGiven = true;
}


		PrintWelcome();
		player.CurrentRoom.GetLongDescriptionWithExits();

		gameWon = false;
		playerDied = false;
	

	}


private void Talk(Command command)
	{
		var npc = player.CurrentRoom.Npc;

		if (npc == null)
		{
			Console.WriteLine("There is no one here to talk to.");
			return;
		}

		Console.WriteLine(npc.Greeting);
		Console.WriteLine();

var availableOptions = npc.Options.FindAll(opt => !opt.Used);

if (availableOptions.Count == 0)
		{
			Console.WriteLine("But you've already talked about everything needed");
			Console.WriteLine();
			return;
		}

		for (int i = 0; i < availableOptions.Count; i++)
		{
			Console.WriteLine($"{i + 1}) {availableOptions[i].Text}");
		}

		Console.WriteLine();
		Console.WriteLine("Choose an option (number): ");
		Console.WriteLine();

		string input = Console.ReadLine();
		if (int.TryParse(input, out int choice) && choice > 0 && choice <= availableOptions.Count)
		{
			var selectedOption = availableOptions[choice - 1];
			selectedOption.Effect(player);
			selectedOption.MarkUsed("You already asked this.");
		}
		else
		{
			Console.WriteLine("You chose nothing or an invalid option.");
		}
	}


private void RandomHotelEvent()
	{
		string[] events =
		{
			"A maid runs past you singing opera",
			"A friendly ghost hovers past you",
			"You hear someone giggling in the other room",
			"You hear small thunder outside",
			"You make out two kids playing nearby",
			"The lights briefly changes colors. It's so pretty",
			"You feel a little breeze somewhere",
			"A fun little tune plays over an intercom",
			"You make out a weird cat's meow",
			"A chandelier moves slightly above you, but nothing falls",
			"The room's accompanying television screen plays a fun broadcast",
			"You smell a glamourous odor",
			"You notice a small glowing footprint that disappears when you approach it"
		};

Random rand = new Random();
if (rand.Next(0, 2) == 0)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine(events[rand.Next(events.Length)]);
			Console.ResetColor();
			Console.WriteLine();
		}

	}
	

}
