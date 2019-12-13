using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using DALProyecto;
using Newtonsoft.Json;

namespace BLLProyecto
{
    public class DatosCliente
    {
        #region Propiedades
        private int clienteId, telefono, numeroTarjeta;
        private string nombreCliente, apellidosCliente, direccionEnvio;

        public int ClienteId
        {
            get { return clienteId; }
            set { clienteId = value; }
        }

        public int Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public int NumeroTarjeta
        {
            get { return numeroTarjeta; }
            set { numeroTarjeta = value; }
        }

        public string NombreCliente
        {
            get { return nombreCliente; }
            set { nombreCliente = value; }
        }

        public string ApellidosCliente
        {
            get { return apellidosCliente; }
            set { apellidosCliente = value; }
        }

        public string DireccionEnvio
        {
            get { return direccionEnvio; }
            set { direccionEnvio = value; }
        }
        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarDatosCliente()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarDatosCliente";
                ds = DAL.ejecutarDataSet(conn, sql, true, ref mensajeError, ref numError);
                if (numError != 0)
                {
                    return null;
                }
                else
                {
                    return JsonConvert.SerializeObject(ds.Tables[0]);
                }
            }
        }

        public bool insertarDatosCliente(string accion)
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return false;
            }
            else
            {
                if (accion.Equals("Insertar"))
                {
                    sql = "insertarDatosCliente";
                }
                else
                {
                    sql = "modificarDatosCliente";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[6];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@clienteId", SqlDbType.Int, clienteId);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@nombreCliente", SqlDbType.VarChar, nombreCliente);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@apellidosCliente", SqlDbType.VarChar, apellidosCliente);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@telefono", SqlDbType.Int, telefono);
                DAL.agregarEstructuraParametros(ref parametros, 4, "@direccionEnvio", SqlDbType.VarChar, direccionEnvio);
                DAL.agregarEstructuraParametros(ref parametros, 5, "@numeroTarjeta", SqlDbType.Int, numeroTarjeta);

                DAL.conectar(conn, ref mensajeError, ref numError);
                DAL.ejecutarSqlCommandParametros(conn, sql, true, parametros, ref mensajeError, ref numError);

                if (numError != 0)
                {
                    HttpContext.Current.Response.Redirect("NUMERO DE ERROR: " + numError.ToString() + "MENSAJE DE ERROR: " + mensajeError);
                    DAL.desconectar(conn, ref mensajeError, ref numError);
                    return false;
                }
                else
                {
                    DAL.desconectar(conn, ref mensajeError, ref numError);
                    return true;
                }
            }
        }
    }
}
