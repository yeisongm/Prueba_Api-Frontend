using Microsoft.EntityFrameworkCore;
using PrestamosBancarios.Models;

namespace PrestamosBancarios.Data;

public class PrestamoDbContext : DbContext
{
    public PrestamoDbContext(DbContextOptions<PrestamoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Prestamo> Prestamos { get; set; }
} 