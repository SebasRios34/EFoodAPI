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
    public class Bitacora
    {
        private int codigoBitacora, usuariosAdminId;
        private string descripcion;
        private DateTime hora, fecha;

        public int CodigoBitacora
        {
            get { return codigoBitacora; }
            set { codigoBitacora = value; }
        }

        public int UsuariosAdmin
        {
            get { return usuariosAdminId; }
            set { usuariosAdminId = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public DateTime Hora
        {
            get { return hora; }
            set { hora = value; }
        }

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion



        public string cargarBitacora()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarBitacora";
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

        public bool agregarBitacora(string accion)
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
                    sql = "insertarBitacora";
                }
                else
                {
                    sql = "modificarBitacora";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[5];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@codigoBitacora", SqlDbType.Int, codigoBitacora);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@usuarioAdminID", SqlDbType.Int, usuariosAdminId);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@fecha", SqlDbType.Date, fecha);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@hora", SqlDbType.Time, hora);
                DAL.agregarEstructuraParametros(ref parametros, 4, "@descripcion", SqlDbType.VarChar, descripcion);

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
