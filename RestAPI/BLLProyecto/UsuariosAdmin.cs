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
    public class UsuariosAdmin
    {
        #region Propiedades
        private int usuariosAdminId, rolId, preguntaSeguridadId;
        private string usuariosNombre, contrasena, email, respuestaSeguridad, estado;

        public int UsuariosAdminId
        {
            get { return usuariosAdminId; }
            set { usuariosAdminId = value; }
        }

        public int RolId
        {
            get { return rolId; }
            set { rolId = value; }
        }

        public int PreguntaSeguridadId
        {
            get { return preguntaSeguridadId; }
            set { preguntaSeguridadId = value; }
        }

        public string UsuariosNombre
        {
            get { return usuariosNombre; }
            set { usuariosNombre = value; }
        }

        public string Contrasena
        {
            get { return contrasena; }
            set { contrasena = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string RespuestaSeguridad
        {
            get { return respuestaSeguridad; }
            set { respuestaSeguridad = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
        #endregion


        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion


        public string cargarUsuariosAdmin()
        {
            conn = DALProyecto.DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarUsuariosAdmin";
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

        public string cargarUsuariosAdminId()
        {
            conn = DALProyecto.DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarUsuariosAdminId";
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

        public bool modificarContrasena(string accion)
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
                    sql = "modificarUsuariosAdminContrasena";
                }
                else
                {
                    sql = "modificarUsuariosAdmin";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[2];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@usuariosAdminId", SqlDbType.Int, usuariosAdminId);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@contrasena", SqlDbType.VarChar, contrasena);
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

        public bool agregarUsuariosAdmin(string accion)
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
                    sql = "insertarUsuariosAdmin";
                    ParametrosStructures[] parametros = new ParametrosStructures[8];
                    DAL.agregarEstructuraParametros(ref parametros, 0, "@usuariosAdminId", SqlDbType.Int, usuariosAdminId);
                    DAL.agregarEstructuraParametros(ref parametros, 1, "@usuariosNombre", SqlDbType.VarChar, usuariosNombre);
                    DAL.agregarEstructuraParametros(ref parametros, 2, "@contrasena", SqlDbType.VarChar, contrasena);
                    DAL.agregarEstructuraParametros(ref parametros, 3, "@email", SqlDbType.VarChar, email);
                    DAL.agregarEstructuraParametros(ref parametros, 4, "@rolId", SqlDbType.Int, rolId);
                    DAL.agregarEstructuraParametros(ref parametros, 5, "@preguntaSeguridadId", SqlDbType.Int, preguntaSeguridadId);
                    DAL.agregarEstructuraParametros(ref parametros, 6, "@respuestaSeguridad", SqlDbType.VarChar, respuestaSeguridad);
                    DAL.agregarEstructuraParametros(ref parametros, 7, "@estado", SqlDbType.VarChar, estado);
                    DAL.conectar(conn, ref mensajeError, ref numError);
                    DAL.ejecutarSqlCommandParametros(conn, sql, true, parametros, ref mensajeError, ref numError);
                }
                else
                {
                    sql = "modificarUsuariosAdmin";
                    ParametrosStructures[] parametros = new ParametrosStructures[7];
                    DAL.agregarEstructuraParametros(ref parametros, 0, "@usuariosAdminId", SqlDbType.Int, usuariosAdminId);
                    DAL.agregarEstructuraParametros(ref parametros, 1, "@usuarioNombre", SqlDbType.VarChar, usuariosNombre);
                    DAL.agregarEstructuraParametros(ref parametros, 2, "@email", SqlDbType.VarChar, email);
                    DAL.agregarEstructuraParametros(ref parametros, 3, "@rolId", SqlDbType.Int, rolId);
                    DAL.agregarEstructuraParametros(ref parametros, 4, "@preguntaSeguridadId", SqlDbType.Int, preguntaSeguridadId);
                    DAL.agregarEstructuraParametros(ref parametros, 5, "@respuestaSeguridad", SqlDbType.VarChar, respuestaSeguridad);
                    DAL.agregarEstructuraParametros(ref parametros, 6, "@estado", SqlDbType.VarChar, estado);
                    DAL.conectar(conn, ref mensajeError, ref numError);
                    DAL.ejecutarSqlCommandParametros(conn, sql, true, parametros, ref mensajeError, ref numError);
                }


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
