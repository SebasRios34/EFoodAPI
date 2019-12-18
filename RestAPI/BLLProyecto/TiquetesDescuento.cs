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
    public class TiquetesDescuento
    {
        #region Propiedadas
        private int tipoConsecutivo, codigoTiqueteDescuento, disponibles, descuentoPorcentaje, descuentoCantidad;
        private string nombreTiquete;

        public int TipoConsecutivo
        {
            get { return tipoConsecutivo; }
            set { tipoConsecutivo = value; }
        }

        public int CodigoTiqueteDescuento
        {
            get { return codigoTiqueteDescuento; }
            set { codigoTiqueteDescuento = value; }
        }

        public int Disponibles
        {
            get { return disponibles; }
            set { disponibles = value; }
        }

        public int DescuentoPorcentaje { 
        
            get { return descuentoPorcentaje; }
            set { descuentoPorcentaje = value; }
        }

        public int DescuentoCantidad
        {
            get { return descuentoCantidad; }
            set { descuentoCantidad = value; }
        }

        public string NombreTiquete
        {
            get { return nombreTiquete; }
            set { nombreTiquete = value; }
        }
        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarTiquetesDescuento()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarTiquetesDescuento";
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

        public bool insertarTiquetesDescuento(string accion)
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
                    sql = "insertarTiqueteDescuento";
                }
                else
                {
                    sql = "modificarTiquetesDescuento";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[6];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@tipoConsecutivo", SqlDbType.Int, tipoConsecutivo);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@codigoTiqueteDescuento", SqlDbType.Int, codigoTiqueteDescuento);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@nombreTiqueteDescuento", SqlDbType.VarChar, nombreTiquete);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@disponibles", SqlDbType.Int, disponibles);
                DAL.agregarEstructuraParametros(ref parametros, 4, "@descuentoPorcentaje", SqlDbType.Int, descuentoPorcentaje);
                DAL.agregarEstructuraParametros(ref parametros, 5, "@descuentoCantidad", SqlDbType.Int, descuentoCantidad);

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

        public bool eliminarTiposPrecio(int id)
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
                sql = "eliminarTiqueteDescuento";
                ParametrosStructures[] parametros = new ParametrosStructures[1];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@codigoTiqueteDescuento", SqlDbType.Int, id);
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
