using Microsoft.EntityFrameworkCore;


namespace ApiReservasBus2.Modelos
{
    public class AppDbContext2 :DbContext
    {
        public AppDbContext2(DbContextOptions<AppDbContext2> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Autobus> Autobus { get; set; }
        public DbSet<Viaje> Viaje { get; set; }
    }
}
