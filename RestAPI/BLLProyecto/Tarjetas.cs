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
    public class Tarjetas
    {
        private int tipoConsecutivo, codigoTarjeta;
        private string nombreTarjeta;

        public int TipoConsecutivo
        {
            get { return tipoConsecutivo; }
            set { tipoConsecutivo = value; }
        }

        public int CodigoTarjeta
        {
            get { return codigoTarjeta; }
            set { codigoTarjeta = value; }
        }

        public string Nombretarjeta
        {
            get { return nombreTarjeta; }
            set { nombreTarjeta = value; }
        }

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarTarjetas()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarTarjetas";
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

        public bool insertarTarjetas(string accion)
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
                    sql = "insertarTarjetas";
                }
                else
                {
                    sql = "modificarTarjetas";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[3];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@tipoConsecutivo", SqlDbType.Int, tipoConsecutivo);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@codigoTarjeta", SqlDbType.Int, codigoTarjeta);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@nombreTarjeta", SqlDbType.VarChar, nombreTarjeta);

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

        public bool eliminarTarjetas(int id)
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
                sql = "eliminarTarjetas";
                ParametrosStructures[] parametros = new ParametrosStructures[1];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@codigoTarjeta", SqlDbType.Int, id);
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
