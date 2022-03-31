using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiRestHoovers.Models;
using ApiRestHoovers.Services;

namespace ApiRestHoovers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly HOOVERSContext _context;

        public ClientesController(HOOVERSContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("cliente")]
        public ActionResult<List<ClienteResult>> Get()
        {
            var clienteService = new ClienteService();
            List<ClienteResult> clientes = clienteService.GetClientes();

            return Ok(clientes);
        }

        [HttpGet]
        [Route("infoReport")]
        public ActionResult<List<ReporteVista>> GetInfoReport()
        {
            var clienteService = new ClienteService();
            List<ReporteVista> report = clienteService.GetReportAll();

            return Ok(report);
        }

        [HttpGet]
        [Route("viajesPorCliente")]
        public ActionResult<List<TotalViaje>> GetTotal(string idCliente, string precio)
        {
            var clienteService = new ClienteService();
            return Ok(clienteService.getTotalViajes(idCliente, precio));
        }

        [HttpGet]
        [Route("viajesByIdAndDates")]
        public ActionResult<List<ViajesByIdDates>> GetReportOne(int idCliente, string fechaInicio, string fechaFin)
        {
            var clienteService = new ClienteService();
            return Ok(clienteService.getTotalViajesIdClientAndDates(idCliente, fechaInicio, fechaFin));
        }

        [HttpGet]
        [Route("GetViajesByDates")]
        public ActionResult<List<ViajesYearMothTotals>> GetReportTwo(string fechaInicio, string fechaFin)
        {
            var clienteService = new ClienteService();
            {
                var cliente = clienteService.getTotalViajesByDates(fechaInicio, fechaFin);
                if (cliente != null)
                {
                    return Ok(cliente);
                }
                return NotFound("Error: No existen viajes en este rango de fechas");
            }
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(new Cliente {
                Id = id,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono,
                Estado = cliente.Estado,
                FechaCreacion = DateTime.Now.AddDays(-1),
                FechaActualizacion = DateTime.Now

            }).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<String> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(new Cliente
            {
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido,
                Telefono = cliente.Telefono
            });

            await _context.SaveChangesAsync();

            return "Cliente creado exitosamente";
        }


        [HttpPost("Masivo")]
        public ActionResult<IEnumerable<Cliente>> AddUsuario(List<Cliente> Usuario)
        {
            List<Cliente> guardados = new List<Cliente>();
            if (Usuario != null)
            {
                foreach (var item in Usuario)
                {
                    if (item != null)
                    {
                        _context.Clientes.Add(new Cliente
                        {
                            Nombre = item.Nombre,
                            Apellido = item.Apellido,
                            Telefono = item.Telefono
                        });

                        guardados.Add(item);
                        //_context.SaveChanges();


                    }
                }
                if (guardados.Count > 0)
                {
                    _context.SaveChanges();
                    return Ok(guardados);
                }
                else
                {
                    return NotFound("No se pudieron agregar los Usuarios" + guardados.Count);
                }
            }
            else
            {
                return BadRequest("No se recibió información para almacenar");
            }
        }





        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
