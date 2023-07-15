using DotNetRPG.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetRPG.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
               new Skill { Id = 1, Name = "Rasengan", Damage = 1400 },
               new Skill { Id = 2, Name = "kamehameha", Damage = 1000 },
               new Skill { Id = 3, Name = "clone", Damage = 300 },
               new Skill { Id = 4, Name = "Fire Ball", Damage = 900 },
               new Skill { Id = 5, Name = "RasenShuriken", Damage = 1500 },
               new Skill { Id = 6, Name = "Windwall", Damage = 0 }

          );
        }
        // public DbSet<Character> Character { get; set; }
        public DbSet<Character> characters => Set<Character>();
        public DbSet<User> users => Set<User>();
        public DbSet<Weapon> weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}
