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
    }
}
