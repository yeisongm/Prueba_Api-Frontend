using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrestamosBancarios.Data;
using PrestamosBancarios.Models;

namespace PrestamosBancarios.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrestamosController : ControllerBase
{
    private readonly PrestamoDbContext _contexto;

    public PrestamosController(PrestamoDbContext contexto)
    {
        _contexto = contexto;
    }

    [HttpPost]
    public async Task<ActionResult<Prestamo>> SolicitarPrestamo(SolicitudPrestamo solicitud)
    {
        var prestamo = new Prestamo
        {
            Monto = solicitud.Monto,
            PlazoEnMeses = solicitud.PlazoEnMeses,
            FechaSolicitud = DateTime.UtcNow,
            Estado = EstadoPrestamo.Pendiente,
            IdUsuario = "usuario-prueba" // Valor hardcodeado temporalmente
        };

        _contexto.Prestamos.Add(prestamo);
        await _contexto.SaveChangesAsync();

        return CreatedAtAction(nameof(ObtenerPrestamo), new { id = prestamo.Id }, prestamo);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Prestamo>> ObtenerPrestamo(int id)
    {
        var prestamo = await _contexto.Prestamos.FindAsync(id);

        if (prestamo == null)
        {
            return NotFound();
        }

        return prestamo;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prestamo>>> ObtenerPrestamosPorUsuario()
    {
        return await _contexto.Prestamos.ToListAsync();
    }

    [HttpPut("{id}/aprobar")]
    public async Task<IActionResult> AprobarPrestamo(int id)
    {
        var prestamo = await _contexto.Prestamos.FindAsync(id);

        if (prestamo == null)
        {
            return NotFound();
        }

        prestamo.Estado = EstadoPrestamo.Aprobado;
        prestamo.FechaAprobacion = DateTime.UtcNow;
        prestamo.IdAdministrador = "admin-prueba"; // Valor hardcodeado temporalmente

        await _contexto.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}/rechazar")]
    public async Task<IActionResult> RechazarPrestamo(int id, [FromBody] string motivo)
    {
        var prestamo = await _contexto.Prestamos.FindAsync(id);

        if (prestamo == null)
        {
            return NotFound();
        }

        prestamo.Estado = EstadoPrestamo.Rechazado;
        prestamo.MotivoRechazo = motivo;
        prestamo.IdAdministrador = "admin-prueba"; // Valor hardcodeado temporalmente

        await _contexto.SaveChangesAsync();
        return NoContent();
    }
}