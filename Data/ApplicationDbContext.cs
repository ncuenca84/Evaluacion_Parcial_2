using Evaluacion_Parcial_2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Evaluacion_Parcial_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Taller> Talleres { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
    }
}
