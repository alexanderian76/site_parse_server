using System.Numerics;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using site_parse_server.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<ParseTask> Tasks { get; set; }
    public DbSet<Request> Requests { get; set; }


    public DataBaseContext()
    {
            Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Filename=DataBase.db");
    }
}