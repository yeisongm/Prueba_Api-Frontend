 using System.ComponentModel.DataAnnotations;

namespace PrestamosBancarios.Models;

public class Prestamo
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El monto es requerido")]
    [Range(1000, 1000000, ErrorMessage = "El monto debe estar entre $1,000 y $1,000,000")]
    public decimal Monto { get; set; }

    [Required(ErrorMessage = "El plazo en meses es requerido")]
    [Range(6, 60, ErrorMessage = "El plazo debe estar entre 6 y 60 meses")]
    public int PlazoEnMeses { get; set; }

    public DateTime FechaSolicitud { get; set; }
    public EstadoPrestamo Estado { get; set; }
    
    [Required]
    public string IdUsuario { get; set; }
    public string? IdAdministrador { get; set; }
    public DateTime? FechaAprobacion { get; set; }
    public string? MotivoRechazo { get; set; }
}

public enum EstadoPrestamo
{
    Pendiente,
    Aprobado,
    Rechazado
}