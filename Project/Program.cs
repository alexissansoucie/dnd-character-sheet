// Project/Program.cs — swap Thing for your record's name, and print your own facts
var registry = new Registry();

registry.Add(new Thing("the first one"));
registry.Add(new Thing("the second one"));
registry.Add(new Thing("the third one"));

Console.WriteLine(Registry.Topic);
Console.WriteLine($"{registry.Count} on file.");
Console.WriteLine();

foreach (Thing item in registry.All())
{
    Console.WriteLine(item.Name);
}