using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestHoovers.Models;

namespace ApiRestHoovers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly HOOVERSContext _context;

        public VehiculoController(HOOVERSContext context)
        {
            _context = context;
        }

        // GET: api/Vehiculo
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Vehiculo>>> GetVehiculos()
        //{
        //    return await _context.Vehiculos.ToListAsync();
        //}

        // GET: api/Vehiculo/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Vehiculo>> GetVehiculo(int id)
        //{
        //    var vehiculo = await _context.Vehiculos.FindAsync(id);

        //    if (vehiculo == null)
        //    {
        //        return NotFound();
        //    }

        //    return vehiculo;
        //}

        // PUT: api/Vehiculo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (id != vehiculo.Id)
            {
                return BadRequest();
            }

            _context.Entry(new Vehiculo
            {
                Id = vehiculo.Id,
                Modelo = vehiculo.Modelo,
                Nombre = vehiculo.Nombre,
                Descripcion = vehiculo.Descripcion,
                IdTipo = vehiculo.IdTipo,
                Estado = vehiculo.Estado,
                FechaCreacion = DateTime.Now.AddDays(-1),
                FechaActualizacion = DateTime.Now
            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehiculo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<String> PostVehiculo(Vehiculo vehiculo)
        {
            _context.Vehiculos.Add( new Vehiculo { 
                Modelo = vehiculo.Modelo,
                Nombre = vehiculo.Nombre,
                Descripcion = vehiculo.Descripcion,
                IdTipo = vehiculo.IdTipo
            });
            await _context.SaveChangesAsync();

            return "Vehiculo creado exitosament";
        }

        // DELETE: api/Vehiculo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehiculo(int id)
        {
            var vehiculo = await _context.Vehiculos.FindAsync(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            _context.Vehiculos.Remove(vehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VehiculoExists(int id)
        {
            return _context.Vehiculos.Any(e => e.Id == id);
        }
    }
}
