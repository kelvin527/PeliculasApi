using Microsoft.EntityFrameworkCore;

namespace PeliculasApi.Entidades
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
