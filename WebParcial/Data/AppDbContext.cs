using Microsoft.EntityFrameworkCore;
using WebParcial.Models;

namespace WebParcial.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar la relación entre Curso y Docente
            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Docente)               // Un Curso tiene un Docente
                .WithMany()                            // Un Docente puede tener muchos Cursos
                .HasForeignKey(c => c.IdDocente)       // La clave foránea es IdDocente
                .OnDelete(DeleteBehavior.SetNull);     // Puedes definir el comportamiento de la eliminación (SetNull, Cascade, etc.)
        }
    }
}

