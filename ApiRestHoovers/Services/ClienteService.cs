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
            var total = new TotalViaje();
            var result = _Conn.Query<TotalViaje>("select c.NOMBRE as nombre, count(*) as total from VIAJE v " +
                   "inner join CLIENTE c on c.ID = v.ID_CLIENTE " +
                    queryCLiente +
                    "and " + queryViaje+
                    "group by c.nombre").ToList();
            return result;

        }
       

        //public List<Fecha> GetFecha()
        //{
        //    _Conn = SqlService.GetSqlConnection();
        //    _Conn.Open();
        //    List<Fecha> result = _Conn.Query<Fecha>("SELECT * FROM FECHA").ToList();
        //    _Conn.Close();
        //    return result;
        //}


    }
}
