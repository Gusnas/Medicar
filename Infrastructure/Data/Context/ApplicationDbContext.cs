using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Agenda> Agendas => Set<Agenda>();
        public DbSet<AgendaHorario> AgendaHorarios => Set<AgendaHorario>();
        public DbSet<Consulta> Consultas => Set<Consulta>();
        public DbSet<Medico> Medicos => Set<Medico>();
    }
}
