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

    [Fact]
    public void Check3_FindHandsBackTheRecordItHolds()
    {
        var registry = new Registry();

        var depot = new Character("Phoenix Longbottom", "Phoenix", "Longbottom", "Fairy", "Rouge", "background", 45);
        registry.Add(depot);

        var found = registry.Find("Phoenix Longbottom");

        Assert.Same(depot, found);
    }

    [Fact]
    public void Check4_RemovingAStrangerSaysNo()
    {
        var registry = new Registry();

        var keeper = new Character("Phoenix Longbottom", "Phoenix", "Longbottom", "Fairy", "Rouge", "background", 45);
        registry.Add(keeper);

        var removed = registry.Remove("Draco Malfoy");

        Assert.False(removed);
        Assert.Equal(1, registry.Count);
    }

    [Fact]
    public void Check5_TheSameNameCannotRegisterTwice()
    {
        var registry = new Registry();

        registry.Add(registry.NewItem("Phoenix Longbottom"));
        registry.Add(registry.NewItem("Phoenix Longbottom"));

        Assert.Equal(1, registry.Count);
    }

}