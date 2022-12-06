using InyeccionSQLProyect.Models;
using Microsoft.EntityFrameworkCore;

namespace InyeccionSQLProyect.DataContext
{
    public class PgContext : DbContext
    {
        public PgContext(DbContextOptions<PgContext> options) : base(options)
        {

        }

        public virtual DbSet<UsuariosPostgres> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<UsuariosPostgres>(entity =>
            {
                entity.ToTable("usuarios");

                entity.Property(x => x.Id).HasMaxLength(100);
                entity.Property(x => x.Nombre).HasMaxLength(100);

            });
            
            base.OnModelCreating(modelBuilder);
        }
    }

}