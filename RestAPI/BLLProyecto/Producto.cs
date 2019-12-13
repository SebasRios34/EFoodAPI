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
    public class Producto
    {
        #region
        private int tipoConsecutivo, codigoProducto, codigoLineaComida, codigoTipoPrecio;
        private string nombreProducto, contenido;

        public int TipoConsecutivo
        {
            get { return tipoConsecutivo; }
            set { tipoConsecutivo = value; }
        }

        public int CodigoProducto
        {
            get { return codigoProducto; }
            set { codigoProducto = value; }
        }

        public int CodigoLineaComida
        {
            get { return codigoLineaComida; }
            set { codigoLineaComida = value; }
        }

        public int CodigoTipoPrecio
        {
            get { return codigoTipoPrecio; }
            set { codigoTipoPrecio = value; }
        }

        public string NombreProducto
        {
            get { return nombreProducto; }
            set { nombreProducto = value; }
        }

        public string Contenido
        {
            get { return contenido; }
            set { contenido = value; }
        }
        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarProducto()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarProducto";
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

        public bool insertarProducto(string accion)
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
                    sql = "insertarProducto";
                }
                else
                {
                    sql = "modificarProducto";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[6];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@tipoConsecutivo", SqlDbType.Int, tipoConsecutivo);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@codigoProducto", SqlDbType.Int, codigoProducto);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@nombreProducto", SqlDbType.VarChar, nombreProducto);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@codigoLineaComida", SqlDbType.Int, codigoLineaComida);
                DAL.agregarEstructuraParametros(ref parametros, 4, "@contenido", SqlDbType.VarChar, contenido);
                DAL.agregarEstructuraParametros(ref parametros, 5, "@codigoTipoPrecio", SqlDbType.VarChar, codigoTipoPrecio);

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

        public bool eliminarProducto(int codigoProcesadorPago)
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
                sql = "eliminarProducto";
                ParametrosStructures[] parametros = new ParametrosStructures[1];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@codigoProducto", SqlDbType.Int, codigoProducto);
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
