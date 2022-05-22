using Microsoft.EntityFrameworkCore;
namespace WebApiTicas2022.Models
{
    public class TicasContext:DbContext
    {
        public TicasContext(DbContextOptions<TicasContext> options) : base (options)
        {
            //en este contructor se usa para el usos de esta clase, junto a un valor option 
        }
        public DbSet<Product> Products { get; set; } = null!;//Lista de productos.

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<Restaurant> Restaurants { get; set; } = null!;

        public DbSet<Student> Students { get; set; }

        public DbSet<Promoción> Promocions { get; set; } = null!;

        public DbSet<Gender> Genders { get; set; } = null;

        public DbSet<Client> Clients { get; set; } = null!;
    }
}
