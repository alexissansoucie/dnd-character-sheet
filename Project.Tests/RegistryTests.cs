using Microsoft.Win32;

namespace Project.Tests;

public class RegistryTests
{
    [Fact]
    public void Check2_AddingGrowsTheCount()
    {
        var registry = new Registry();

        var phoenix = new Character("Phoenix Longbottom", "Phoenix", "Longbottom", "Fairy", "Rouge", "background", 45);
        registry.Add(phoenix);
        var virgil = new Character("Virgil Pureman", "Virgil", "Pureman", "Human", "Bard", "background", 36);
        registry.Add(virgil);

        Assert.Equal(2, registry.Count);
        // set the scene: a fresh Registry
        // do the thing:   Add two records, with two DIFFERENT names
        // check:          Assert.Equal — what should Count be?
    }
}