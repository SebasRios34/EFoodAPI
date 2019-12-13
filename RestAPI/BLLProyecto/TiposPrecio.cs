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
    public class TiposPrecio
    {
        private int tipoConsecutivo, codigoTipoPrecio, precioMonto;
        private string nombrePrecio;

        public int TipoConsecutivo
        {
            get { return tipoConsecutivo; }
            set { tipoConsecutivo = value; }
        }

        public int CodigoTipoPrecio
        {
            get { return codigoTipoPrecio; }
            set { codigoTipoPrecio = value; }
        }

        public int PrecioMonto
        {
            get { return precioMonto; }
            set { precioMonto = value; }
        }

        public string NombrePrecio
        {   
            get { return nombrePrecio; }
            set { nombrePrecio = value; }
        }

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarTiposPrecio()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarTipoPrecio";
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

        public bool insertarTiposPrecio(string accion)
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
                    sql = "insertarTipoPrecio";
                }
                else
                {
                    sql = "modificarTipoPrecio";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[4];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@tipoConsecutivo", SqlDbType.Int, tipoConsecutivo);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@codigoTipoPrecio", SqlDbType.Int, codigoTipoPrecio);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@nombrePrecio", SqlDbType.VarChar, nombrePrecio);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@precioMonto", SqlDbType.VarChar, precioMonto);

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

        public bool eliminarTiposPrecio(int codigoTiposPrecio)
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                //insertar en la table de errores
                HttpContext.Current.Response.Redirect("Error.aspx?error=" + numError.ToString() + "&men=" + mensajeError);
                return false;
            }
            else
            {
                sql = "eliminarTipoPrecio";
                ParametrosStructures[] parametros = new ParametrosStructures[1];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@codigoTarjeta", SqlDbType.Int, codigoTipoPrecio);
                DAL.conectar(conn, ref mensajeError, ref numError);
                DAL.ejecutarSqlCommandParametros(conn, sql, true, parametros, ref mensajeError, ref numError);
                if (numError != 0)
                {
                    //insertar en la table de errores
                    HttpContext.Current.Response.Redirect("Error.aspx?error=" + numError.ToString() + "&men=" + mensajeError);
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
