using ApiRestHoovers.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestHoovers.Services
{
    public class ClienteService
    {
        private SqlConnection _Conn = new SqlConnection();

        public List<ClienteResult> GetClientes()
        {
            _Conn = SqlService.GetSqlConnection();
            _Conn.Open();
            List<ClienteResult> result = _Conn.Query<ClienteResult>("SELECT * FROM CLIENTE").Where(x => x.Estado == 1).ToList();
            _Conn.Close();
            return result;
        }

        public List<TotalViaje> getTotalViajes(string idCliente, string precioViaje) {
            String queryCLiente;
            String queryViaje;
            if (idCliente == null)
            {
               
                 queryCLiente =
                    "where id_cliente = id_cliente ";
            }
            else {
                 queryCLiente =
                    "where id_cliente = '" + idCliente + "' ";
            }
            if (precioViaje == null)
            {
               
                queryViaje =
                   "v.PRECIO_VIAJE = v.PRECIO_VIAJE ";
            }
            else
            {
                queryViaje =
                    "v.PRECIO_VIAJE = '" + precioViaje + "' ";
            }

            _Conn = SqlService.GetSqlConnection();
            _Conn.Open();
            var result = _Conn.Query<TotalViaje>("select c.NOMBRE as nombre, count(*) as total from VIAJE v " +
                   "inner join CLIENTE c on c.ID = v.ID_CLIENTE " +
                    queryCLiente +
                    "and " + queryViaje+
                    "group by c.nombre").ToList();
            return result;

        }

        public List<ReporteVista> GetReportAll()
        {
            _Conn = SqlService.GetSqlConnection();
            _Conn.Open();
            List<ReporteVista> result = _Conn.Query<ReporteVista>("SELECT * FROM VW_VIAJE_CLIENTE").ToList();
            _Conn.Close();
            return result;
        }

        public List<ViajesByIdDates> getTotalViajesIdClientAndDates(int idCliente, string fechaInicio, string fechaFin)
        {
            _Conn = SqlService.GetSqlConnection();
            _Conn.Open();
            var result = _Conn.Query<ViajesByIdDates>("SELECT NOMBRE AS CLIENTE, COUNT(1) VIAJES FROM VW_VIAJE_CLIENTE " +
                   "WHERE ID_CLIENTE = " + idCliente +
                    " AND FECHA_VIAJE BETWEEN  '" + fechaInicio + "' AND '" + fechaFin + "' " +
                    "GROUP BY NOMBRE").ToList();
            return result;

        }

        public List<ViajesYearMothTotals> getTotalViajesByDates(string fechaInicio, string fechaFin)
        {
            _Conn = SqlService.GetSqlConnection();
            _Conn.Open();
            var result = _Conn.Query<ViajesYearMothTotals>("SELECT YEAR(FECHA_VIAJE) ANIO, MONTH(FECHA_VIAJE) MES, COUNT(1) VIAJES FROM VW_VIAJE_CLIENTE " +
                   "WHERE FECHA_VIAJE BETWEEN '" + fechaInicio + "' AND '" + fechaFin + "' " +
                   "GROUP BY YEAR(FECHA_VIAJE), MONTH(FECHA_VIAJE)").ToList();

            if (result.Count != 0)
            {
                return result;
            } else
            {
                return null;
            }
        }


    }
}
