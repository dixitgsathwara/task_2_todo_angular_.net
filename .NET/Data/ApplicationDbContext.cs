using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> users { get; set; }
    public DbSet<Contact> contacts { get; set; }
}

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}
public class Contact
{
    public int id { get; set; }
    public string userid { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
}
