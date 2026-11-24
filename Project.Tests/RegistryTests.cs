using Microsoft.Win32;
using Microsoft.EntityFrameworkCore;

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

    [Fact]
    public void Week8_TheRegistrySurvivesARestart()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        Character item = registry.NewItem("Arwen Thornheart");
        registry.Add(item);
        item.LevelUp();

        registry.Save(new ProjectContext(options));

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Equal(2, reopened.All()[0].Level);
    }

    [Fact]
    public void Week10_ASecondSaveChangesTheRow()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        registry.Add(registry.NewItem("Virgil Pureman"));
        registry.Save(new ProjectContext(options));

        ProjectContext db = new ProjectContext(options);
        Registry nextRun = new Registry();
        nextRun.Load(db);
        Character item = nextRun.Find("Virgil Pureman")!;
        item.LevelUp();
        nextRun.Save(db);

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Equal(2, reopened.Find("virgil Pureman")!.Level);
    }

    [Fact]
    public void Week9_FindComesBackEmptyHanded()
    {
        Registry registry = new Registry();
        Character item = registry.NewItem("Arwen Thornheart");
        registry.Add(item);
        Assert.Same(item, registry.Find("Arwen Thornheart"));
        Assert.Null(registry.Find("a name nobody has"));
    }

    [Fact]
    public void Week11_ARemovedRecordStaysGone()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        Character kept = registry.NewItem("Ariel Lowwater");
        Character removed = registry.NewItem("Virgil Pureman");
        registry.Add(kept);
        registry.Add(removed);
        registry.Save(new ProjectContext(options));

        ProjectContext db = new ProjectContext(options);
        Registry nextRun = new Registry();
        nextRun.Load(db);
        nextRun.Remove("Virgil Pureman");
        nextRun.Save(db);

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(1, reopened.Count);
        Assert.Null(reopened.Find("Virgil Pureman"));
    }

    [Fact]
    public void Week12_DetailsComeBack()
    {
        DbContextOptions<ProjectContext> options = new DbContextOptionsBuilder<ProjectContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        Registry registry = new Registry();
        Character phoenix = registry.NewItem("Phoenix Longbottom");
        Character virgil = registry.NewItem("Virgil Pureman");
        registry.Add(phoenix);
        registry.Add(virgil);
        phoenix.CharactersInventory.Add(registry.NewDetail("Spellbook"));
        phoenix.CharactersInventory.Add(registry.NewDetail("Wand"));
        virgil.CharactersInventory.Add(registry.NewDetail("Lute"));
        registry.Save(new ProjectContext(options));

        Registry reopened = new Registry();
        reopened.Load(new ProjectContext(options));

        Assert.Equal(
            new[] { "Spellbook", "Wand" },
            reopened.Find("Phoenix Longbottom")!.CharactersInventory
                .Select(detail => detail.Name)
                .OrderBy(name => name));
        Assert.Equal(
            new[] { "Lute" },
            reopened.Find("Virgil Pureman")!.CharactersInventory
                .Select(detail => detail.Name));
    }
    [Fact]
    public void Week13_ABadFileAddsNothing()
    {
        string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".json");
        File.WriteAllText(path, """[ "a name of yours", "" ]""");

        Registry registry = new Registry();

        Assert.Throws<InvalidDataException>(() => registry.Import(path));
        Assert.Equal(0, registry.Count);
    }
}
