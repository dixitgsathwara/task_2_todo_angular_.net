using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> users { get; set; }
}

public class User
{
    public int id { get; set; }
    public string username { get; set; }
    public string password { get; set; }
}
