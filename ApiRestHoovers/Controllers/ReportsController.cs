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
    public class ReportsController : ControllerBase
    {
        private readonly HOOVERSContext _context;

        public ReportsController(HOOVERSContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("infoReport")]
        public ActionResult<List<ReporteVista>> GetInfoReport()
        {
            var clienteService = new ClienteService();
            List<ReporteVista> report = clienteService.GetReportAll();

            try
            {

                _context.LogBitacoras.Add(new LogBitacora
                {
                    IdModule = 5,
                    IdMethod = 1,
                    Descripcion = "Se obtiene el listado General del Reporte"
                });

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(report);
        }

        [HttpGet]
        [Route("viajesPorCliente")]
        public ActionResult<List<TotalViaje>> GetTotal(string idCliente)
        {
            var clienteService = new ClienteService();

            try
            {

                _context.LogBitacoras.Add(new LogBitacora
                {
                    IdModule = 5,
                    IdMethod = 1,
                    Descripcion = "Reporte de viajes por clienteID " + idCliente
                });

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(clienteService.getTotalViajes(idCliente));
        }

        [HttpGet]
        [Route("viajesPorDepartamento")]
        public ActionResult<List<TotalViaje>> GetTotalDepto(string id_departamento)
        {
            var clienteService = new ClienteService();

            try
            {

                _context.LogBitacoras.Add(new LogBitacora
                {
                    IdModule = 5,
                    IdMethod = 1,
                    Descripcion = "Reporte de viajes por Departamento " + id_departamento
                });

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(clienteService.getTotalViajesDepto(id_departamento));
        }

        [HttpGet]
        [Route("viajesByIdAndDates")]
        public ActionResult<List<ViajesByIdDates>> GetReportOne(string idCliente, string fechaInicio, string fechaFin)
        {
            var clienteService = new ClienteService();

            try
            {

                _context.LogBitacoras.Add(new LogBitacora
                {
                    IdModule = 5,
                    IdMethod = 1,
                    Descripcion = "Reporte de viajes por cliente " + idCliente + " y fechas " + fechaFin + " " + fechaFin
                });

                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


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
                    try
                    {

                        _context.LogBitacoras.Add(new LogBitacora
                        {
                            IdModule = 5,
                            IdMethod = 1,
                            Descripcion = "Reporte de viajes por año y mes " + fechaInicio + " " + fechaFin
                        });

                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    return Ok(cliente);
                }
                return NotFound("Error: No existen viajes en este rango de fechas");
            }
        }

        [HttpGet]
        [Route("ReportTopMarcas")]
        public ActionResult<List<ReporteTopResult>> ReportTopMarcas()
        {
            var clienteService = new ClienteService();
            {
                var marcas = clienteService.GetTopMarcas();
                if (marcas != null)
                {
                    try
                    {

                        _context.LogBitacoras.Add(new LogBitacora
                        {
                            IdModule = 5,
                            IdMethod = 1,
                            Descripcion = "Reporte Top 10 por marcas"
                        });

                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    return Ok(marcas);
                }
                return NotFound("Error: No existen marcas");
            }
        }

        [HttpGet]
        [Route("ReportDetallePorTipo")]
        public ActionResult<List<ReporteDetalleTipo>> ReportDetallePorTipo()
        {
            var clienteService = new ClienteService();
            {
                var tipos = clienteService.GetDetalleByTipo();
                if (tipos != null)
                {
                    try
                    {

                        _context.LogBitacoras.Add(new LogBitacora
                        {
                            IdModule = 5,
                            IdMethod = 1,
                            Descripcion = "Reporte por tipo vehiculo"
                        });

                        _context.SaveChanges();
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    return Ok(tipos);
                }
                return NotFound("Error: No existen tipos");
            }
        }
    }
}
