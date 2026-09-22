using System.ComponentModel;
using Spectre.Console;

var registry = new Registry();

Character phoenix = new Character("Phoenix", "Fairy", "Rouge", "background", 45);
registry.Add(phoenix);
Character virgil = new Character("Virgil", "Human", "Bard", "background", 36);
registry.Add(virgil);

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} on file.");
Console.WriteLine();

// One I know something about.
Character? known = registry.Find("Virgil");
if (known == null)
{
    Console.WriteLine("Nothing on file by that name.");
}
else
{
    known.LevelUp();
    Console.WriteLine($"{known.Name} - Level {known.Level}");
}

// And one nobody has ever heard of.
Character? missing = registry.Find("something I never added");
Console.WriteLine(missing == null
    ? "Nothing on file by that name."
    : "...found someCharacter that shouldn't be there.");

Console.WriteLine();
Console.Write("Take one off the books (Enter to skip): ");
string? name = Console.ReadLine();
if (!string.IsNullOrWhiteSpace(name))
{
    Console.WriteLine(registry.Remove(name) ? "Removed." : "Nothing by that name.");
}

Console.WriteLine();
foreach (Character item in registry.All())
{
    Console.WriteLine(item.Name);
}
Console.WriteLine($"{registry.Count} on file.");
bool noExit = true;

var fontPath = @"C:\Users\alexi\OneDrive\Documents\WCTC\Fall2026\.NetDatabases\dnd-charactor-sheet\Project\Small.flf";
var font = FigletFont.Load(fontPath);
var dungeonsAndDragons = new FigletText(font, "Dungeons And Dragons")
{
    Justification = Justify.Center,
    Color = Color.DeepSkyBlue1

};
var characterSheet = new FigletText(font, "Character Sheet Manager")
{
    Justification = Justify.Center,
    Color = Color.BlueViolet

};
AnsiConsole.Write(dungeonsAndDragons);
AnsiConsole.Write(characterSheet);
AnsiConsole.WriteLine();

string[] dragonLines = Dragon.Art.Split(Environment.NewLine);
int dragonWidth = dragonLines.Max(line => line.Length);
int leftPadding = Math.Max(0, (Console.WindowWidth - dragonWidth) / 2);
for (int lineIndex = 0; lineIndex < dragonLines.Length; lineIndex++)
{
    string color = lineIndex % 2 == 0 ? "purple" : "blue";
    string centeredLine = new string(' ', leftPadding) + dragonLines[lineIndex];
    AnsiConsole.MarkupLine($"[{color}]{Markup.Escape(centeredLine)}[/]");
}

while (noExit)
{
    Menu();
}

void Menu()
{

    AnsiConsole.MarkupLine("[bold aqua]\nD&D Character Menu[/]");
    foreach (string item in MenuItems.Items)
    {
        AnsiConsole.MarkupLine($"\t[fuchsia]{Markup.Escape(item)}[/]");
    }
    Console.WriteLine();

    int choice = AnsiConsole.Ask<int>("[bold darkblue]What do you want to do? [/]");

    switch (choice)
    {
        case 1:
            Character character = Character.AddCharacter();
            registry.Add(character);
            break;
        case 2:
            CharacterTable.Display(registry);
            break;
        case 3:
            CharacterTable.FindCharacter();
            break;

        case 4:
            break;

        case 5:
            break;

        case 0:
            noExit = false;
            break;
        default:
            AnsiConsole.MarkupLine("[fuchsia]Please enter 1, 2, or 0.[/]");
            break;
    }
}