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
    public class Consecutivo
    {
        #region Propiedades
        private int consecutivoId, tipoConsecutivo, rolId;
        private string prefijo;

        public int ConsecutivoId
        {
            get { return consecutivoId; }
            set { consecutivoId = value; }
        }

        public int TipoConsecutivo
        {
            get { return tipoConsecutivo; }
            set { tipoConsecutivo = value; }
        }

        public int RolId
        {
            get { return rolId; }
            set { rolId = value; }
        }

        public string Prefijo
        {
            get { return prefijo; }
            set { prefijo = value; }
        }
        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarConsecutivo()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarConsecutivo";
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

        public bool modificarConsecutivo(string accion)
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return false;
            }
            else
            {
                if (accion.Equals("Modificar"))
                {
                    sql = "modificarConsecutivo";
                }
                else
                {
                    sql = "insertarConsecutivo";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[4];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@consecutivoId", SqlDbType.Int, consecutivoId);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@tipoConsecutivo", SqlDbType.Int, tipoConsecutivo);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@prefijo", SqlDbType.VarChar, prefijo);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@rolId", SqlDbType.Int, rolId);

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
