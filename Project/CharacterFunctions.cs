using System.Xml;
using Microsoft.IdentityModel.Tokens;
using Spectre.Console;

public static class CharacterFunctions
{
    public static void SelectCharacter(Registry registry)
    {
        if (registry.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]There are no characters to select.[/]");
            return;
        }

        while (true)
        {
            AnsiConsole.Markup("[DarkViolet]Enter the character index (or press enter to cancel): [/] ");
            string? input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            if (int.TryParse(input, out int index) &&
                index >= 0 &&
                index < registry.Count)
            {
                Character character = registry.All()[index];
                CharacterMenu(registry, character);
                return;
            }

            AnsiConsole.MarkupLine("[red]Enter a valid index from the table.[/]");
        }
    }

    private static void CharacterMenu(Registry registry, Character character)
    {
        while (true)
        {
            AnsiConsole.MarkupLine($"\n[bold aqua]{Markup.Escape(character.Name)}[/]");
            AnsiConsole.MarkupLine("[fuchsia]1 - View character[/]");
            AnsiConsole.MarkupLine("[fuchsia]2 - Level up[/]");
            AnsiConsole.MarkupLine("[fuchsia]3 - Edit inventory[/]");
            AnsiConsole.MarkupLine("[fuchsia]4 - Remove character[/]");
            AnsiConsole.MarkupLine("[fuchsia]0 - Back[/]");
            AnsiConsole.Markup("[bold blue]What do you want to do? [/] ");

            string choice = Console.ReadLine()?.Trim() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    AnsiConsole.MarkupLine($"[aqua]{Markup.Escape(character.Line())}[/]");
                    break;
                case "2":
                    character.LevelUp();
                    AnsiConsole.MarkupLine($"[DeepSkyBlue4]{Markup.Escape(character.Name)} leveled up to level {character.Level}.[/]");
                    break;
                case "3":
                    InventoryFunctions.Edit(character);
                    break;
                case "4":
                    if (ConfirmRemoval(character))
                    {
                        registry.Remove(character.Name);
                        AnsiConsole.MarkupLine($"[red]{Markup.Escape(character.Name)} has been removed.[/]");
                        return;
                    }

                    break;
                case "0":
                    return;
                default:
                    AnsiConsole.MarkupLine("[red]Please enter a valid menu number.[/]");
                    break;
            }
        }
    }

    private static bool ConfirmRemoval(Character character)
    {
        return ReadYesNo($"[red]Are you sure you want to remove {Markup.Escape(character.Name)}? (y/n)[/] ");
    }

    public static void FindCharacter(Registry registry)
    {
        string? typed;
        string searchMethod;
        while (true)
        {
            AnsiConsole.Markup("[purple]Search by Full Name (f) or Partial Name (p) (enter to cancel):[/] ");
            typed = Console.ReadLine();
            searchMethod = typed?.Trim() ?? string.Empty;

            typed = typed?.Trim();
            if (string.IsNullOrWhiteSpace(typed))
            {
                return;
            }

            if (searchMethod.Equals("f", StringComparison.OrdinalIgnoreCase) ||
                searchMethod.Equals("p", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }
            AnsiConsole.MarkupLine("[red]A valid response is required.[/]");
        }

        string name;
        while (true)
        {
            AnsiConsole.Markup("[aqua]What is the Character's name? (enter to cancel)[/] ");
            typed = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(typed))
            {
                return;
            }

            if (typed.Any(char.IsLetter))
            {
                name = typed;
                break;
            }

            AnsiConsole.MarkupLine("[red]Enter a name that contains at least one letter.[/]");
        }

        if (searchMethod.Equals("f", StringComparison.OrdinalIgnoreCase))
        {
            Character? character = registry.Find(name);
            if (character != null)
            {
                Console.WriteLine(character.Line());
            }
            else
            {
                PromptToAddCharacter(registry, name);
            }
        }
        else
        {
            List<Character> matches = registry.Matching(name);
            if (matches.Count == 0)
            {
                PromptToAddCharacter(registry, name);
            }
            else
            {
                foreach (Character character in matches)
                {
                    Console.WriteLine(character.Line());
                }
            }
        }
    }

    private static void PromptToAddCharacter(Registry registry, string searchedName)
    {
        AnsiConsole.MarkupLine($"[yellow]No character found matching {Markup.Escape(searchedName)}.[/] ");
        if (!ReadYesNo("[aqua]Would you like to add this character? (y/n)[/] "))
        {
            return;
        }

        Character character = Character.AddCharacter();
        if (registry.Find(character.Name) != null)
        {
            AnsiConsole.MarkupLine($"[yellow]A character named {Markup.Escape(character.Name)} is already on file.[/]");
            return;
        }

        registry.Add(character);
        AnsiConsole.MarkupLine($"[green]{Markup.Escape(character.Name)} was added.[/]");
    }

    public static void RemoveCharacter(Registry registry)
    {
        if (registry.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]There are no characters to remove.[/]");
            return;
        }

        CharacterTable.DisplayOnly(registry);

        while (true)
        {
            AnsiConsole.Markup("[aqua]Enter the character index to remove (or press enter to cancel):[/] ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                AnsiConsole.MarkupLine("[red]Nothing Removed.[/]");
                return;
            }

            if (!int.TryParse(input, out int index))
            {
                AnsiConsole.MarkupLine("[red]Enter a valid number.[/]");
                continue;
            }

            List<Character> characters = registry.All();
            if (index < 0 || index >= characters.Count)
            {
                AnsiConsole.MarkupLine("[red]That index does not exist.[/]");
                continue;
            }

            Character character = characters[index];
            if (ReadYesNo($"[red]Are you sure you want to remove {Markup.Escape(character.Name)}? (y/n)[/] "))
            {
                registry.Remove(character.Name);
                AnsiConsole.MarkupLine($"[red]{Markup.Escape(character.Name)} has been removed[/]");
                return;
            }

            AnsiConsole.MarkupLine("[yellow]Character was not removed.[/]");
            return;
        }
    }

    private static bool ReadYesNo(string prompt)
    {
        while (true)
        {
            AnsiConsole.Markup(prompt);
            string? response = Console.ReadLine()?.Trim();

            if (response != null &&
                (response.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                 response.Equals("yes", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            if (response != null &&
                (response.Equals("n", StringComparison.OrdinalIgnoreCase) ||
                 response.Equals("no", StringComparison.OrdinalIgnoreCase)))
            {
                return false;
            }

            AnsiConsole.MarkupLine("[red]Please enter yes or no.[/]");
        }
    }

    public static void LevelUpCharacters(Registry registry)
    {
        foreach (Character character in registry.All())
        {
            character.LevelUp();
        }

        AnsiConsole.MarkupLine("[DeepSkyBlue4]\nALL CHARACTERS LEVELED UP!!![/]");
    }
}
