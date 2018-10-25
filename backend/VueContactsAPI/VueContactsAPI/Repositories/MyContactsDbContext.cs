using Microsoft.EntityFrameworkCore;
using VueContactsAPI.Entities;

namespace VueContactsAPI.Repositories
{
    public class MyContactsDbContext : DbContext
    {
        public MyContactsDbContext(DbContextOptions<MyContactsDbContext> options): base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
