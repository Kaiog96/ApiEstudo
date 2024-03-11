namespace ApiEstudo.Model.Context
{
    using Microsoft.EntityFrameworkCore;

    public class MysqlContext : DbContext
    {
        public MysqlContext(){}

        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options) {}

        public DbSet<Person> Persons { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
