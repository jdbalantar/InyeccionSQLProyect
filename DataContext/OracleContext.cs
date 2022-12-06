using InyeccionSQLProyect.Models;
using Microsoft.EntityFrameworkCore;

namespace InyeccionSQLProyect.DataContext
{
    public partial class OracleContext : DbContext
    {
        public OracleContext(DbContextOptions<OracleContext> options) : base(options)
        {

        }
        
        public virtual DbSet<UsuariosOracle> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ORACLEAPIDB");
            
            modelBuilder.Entity<UsuariosOracle>(entity =>
            {
                entity.ToTable("USUARIOS");

                entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ID");

                entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            });
            
            OnModelCreatingPartial(modelBuilder);
        }
        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}