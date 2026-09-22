// Project/Program.cs — swap Thing for your record's name, and print your own facts
using System.ComponentModel;
using Spectre.Console;

var registry = new Registry();
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
while(noExit){
    Menu();
}

void AddCharacter()
{
    AnsiConsole.Markup("[aqua]What is your Character's name?[/] ");
    string name = Console.ReadLine() ?? "---";

    AnsiConsole.Markup("[blue]What is your Character's species?[/] ");
    string species = Console.ReadLine() ?? "---";

    AnsiConsole.Markup("[purple]What is your Character's class?[/] ");
    string characterClass = Console.ReadLine() ?? "---";

    AnsiConsole.Markup("[darkblue]What is your Character's background?[/] ");
    string background = Console.ReadLine() ?? "---";

    AnsiConsole.Markup("[aqua]What is your Character's age?[/] ");
    int age = int.Parse(Console.ReadLine() ?? "");

    Character character = new Character(name, species, characterClass, background, age);

    registry.Add(character);
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
            AddCharacter();
            break;
        case 2:
            CharacterTable();
            break;
        case 0:
            noExit = false;
            break;
        default:
            AnsiConsole.MarkupLine("[fuchsia]Please enter 1, 2, or 0.[/]");
            break;
    }
}

void CharacterTable()
{
    AnsiConsole.MarkupLine($"[bold teal]\n{Markup.Escape(Registry.Topic)}[/]");
    AnsiConsole.MarkupLine($"[darkblue]{registry.Count} character(s) on file.[/]");

    var table = new Table();
    table.Border = TableBorder.Rounded;
    table.BorderStyle = new Style(Color.Black);
    table.Title("[bold purple]D&D Characters[/]");
    table.AddColumn("[bold fuchsia]Name[/]");
    table.AddColumn("[bold aqua]Species[/]");
    table.AddColumn("[bold blue]Class[/]");
    table.AddColumn("[bold teal]Background[/]");
    table.AddColumn("[bold darkblue]Age[/]");

    int rowIndex = 0;
    foreach (Character character in registry.All())
    {
        string rowColor = rowIndex % 2 == 0 ? "darkred" : "darkgreen";
        table.AddRow(
            $"[{rowColor}]{Markup.Escape(character.Name)}[/]",
            $"[{rowColor}]{Markup.Escape(character.Species)}[/]",
            $"[{rowColor}]{Markup.Escape(character.Class)}[/]",
            $"[{rowColor}]{Markup.Escape(character.Background)}[/]",
            $"[{rowColor}]{character.Age}[/]");
        rowIndex++;
    }

    AnsiConsole.Write(table);
}
