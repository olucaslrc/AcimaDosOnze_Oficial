using Microsoft.EntityFrameworkCore;
using AcimaDosOnze_Oficial.AuthenticationSector.Entities;

namespace AcimaDosOnze_Oficial.AuthenticationSector.Data
{
    public class DBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Icao> Icaos { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=AcimaDosOnzeDb.db");
    }
}