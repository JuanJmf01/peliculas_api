using Microsoft.EntityFrameworkCore;
using PeliculasApii.Entidades;

namespace PeliculasApii
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        //Genera una tabla a partir del esquema: "Genero"
        public DbSet<Genero> Generos { get; set; }
    }
}
