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
    public class PreguntaSeguridad
    {
        #region Propiedades
        private int preguntaSeguridadId;
        private string preguntaSeguridadNombre;

        public int PreguntaSeguridadId
        {
            get { return preguntaSeguridadId; }
            set { preguntaSeguridadId = value; }
        }

        public string PreguntaSeguridadNombre
        {
            get { return PreguntaSeguridadNombre; }
            set { preguntaSeguridadNombre = value; }
        }

        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion


        public string cargarPreguntaSeguridad()
        {
            conn = DALProyecto.DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarPreguntaSeguridad";
                ds = DALProyecto.DAL.ejecutarDataSet(conn, sql, true, ref mensajeError, ref numError);
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

        public bool agregarPreguntaSeguridad(string accion)
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
                    sql = "insertarPreguntaSeguridad";
                }
                else
                {
                    sql = "modificarPreguntaSeguridad";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[2];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@preguntaSeguridadId", SqlDbType.Int, preguntaSeguridadId);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@nombrePreguntaSeguridad", SqlDbType.VarChar, preguntaSeguridadNombre);

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
