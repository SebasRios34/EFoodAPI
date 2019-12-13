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
    public class DatosTarjetas
    {
        #region Propiedades
        private int numeroTarjeta, codigoTarjeta, cvv;
        private DateTime fechaExpiracion;

        public int NumeroTarjeta
        {
            get { return numeroTarjeta; }
            set { numeroTarjeta = value; }
        }

        public int CodigoTarjeta
        {
            get { return codigoTarjeta; }
            set { codigoTarjeta = value; }
        }

        public int CVV
        {
            get { return cvv; }
            set { cvv = value; }
        }

        public DateTime FechaExpiracion
        {
            get { return fechaExpiracion; }
            set { fechaExpiracion = value; }
        }
        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarDatosTarjeta()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarDatosTarjeta";
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

        public bool insertarDatosTarjeta(string accion)
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
                    sql = "insertarDatosTarjeta";
                }
                else
                {
                    sql = "modificarDatosTarjeta";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[4];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@numeroTarjeta", SqlDbType.Int, numeroTarjeta);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@codigoTarjeta", SqlDbType.Int, codigoTarjeta);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@fechaExpiracion", SqlDbType.DateTime, fechaExpiracion);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@cvv", SqlDbType.Int, cvv);

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
