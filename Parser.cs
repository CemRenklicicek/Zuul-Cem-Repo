using System;
using System.Linq;

class Parser
{
	// Holds all valid command words
	private readonly CommandLibrary commandLibrary; 

	// Constructor
	public Parser()
	{
		commandLibrary = new CommandLibrary();
	}

	// Ask and interpret the user input. Return a Command object.
	public Command GetCommand()
	{
		Console.Write("> "); // print prompt

		string word1 = null;
		string word2 = null;


string input = Console.ReadLine()?.Trim();
if (string.IsNullOrEmpty(input)) return new Command(null, null);

string[] words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

if (words.Length > 0) word1 = words [0].ToLower();
if (words.Length > 1)
		{
			word2 = string.Join(" ", words.Skip(1)).ToLower();
		}

		// Now check whether this word is known. If so, create a command with it.
		if (commandLibrary.IsValidCommandWord(word1))
		 {
			return new Command(word1, word2);
		}

		// If not, create a "null" command (for unknown command).
		return new Command(null, null);
	}

	// Prints a list of valid command words from commandLibrary.
	public void PrintValidCommands()
	{
		Console.WriteLine("Use one of those commands to progress.");
		Console.WriteLine();
		Console.WriteLine("Examples: Type 'go' with a direction you like to take.");
		Console.WriteLine("Or type 'use', 'take', or 'drop' to do something with an item.");
		Console.WriteLine();
		Console.WriteLine(commandLibrary.GetCommandsString());
		Console.WriteLine();
	}
}
