using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}