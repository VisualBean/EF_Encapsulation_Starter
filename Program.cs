using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace EF_Encapsulation_Starter;

class Program
{
    static void Main(string[] args)
    {
        using var db = new EncapsulationContext();
        db.Database.EnsureCreated();

        var demon = new Demon
        {
            Name = "Belzebob", // yes.. bob..
            PowerDamage = 69,
            PowerName = "FIREBALLZ"
        };
        
        db.Demons.Add(demon);
        db.SaveChanges();
        
        Console.WriteLine(demon);
        Console.WriteLine(db.Demons.Count());
    }
}

[DebuggerDisplay("Name: {Name}")]
public class Demon
{
    public int Age { get; set; }

    public bool HasHorns { get; set; }

    public string Name { get; set; }
    
    public int PowerDamage { get; set; }

    public string PowerName { get; set; }
    
    public List<string> Underlings { get; set; } = new List<string>();

    public override string ToString()
    {
        return Name;
    }
}

class EncapsulationContext : DbContext
{
    public DbSet<Demon> Demons { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Demon>(demon =>
        {
            demon.Property<int>("Id");
            demon.HasKey("Id");
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=demons.db");
        base.OnConfiguring(optionsBuilder);
    }
}