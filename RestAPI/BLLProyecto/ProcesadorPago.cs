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
    public class ProcesadorPago
    {
        #region
        private int tipoConsecutivo, codigoProcesador, codigoTarjeta;
        private string nombreProcesador, tipo, estado, requiereVerificacion, metodo;

        public int TipoConsecutivo
        {
            get { return tipoConsecutivo; }
            set { tipoConsecutivo = value; }
        }
        public int CodigoProcesador
        {
            get { return codigoProcesador; }
            set { codigoProcesador = value; }
        }

        public int CodigoTarjeta
        {
            get { return codigoTarjeta; }
            set { codigoTarjeta = value; }
        }

        public string NombreProcesador
        {
            get { return nombreProcesador; }
            set { nombreProcesador = value; }
        }

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string RequiereVerificacion
        {
            get { return requiereVerificacion; }
            set { requiereVerificacion = value; }
        }

        public string Metodo
        {
            get { return metodo; }
            set { metodo = value; }
        }
        #endregion

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarProcesadorPago()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarProcesadorPago";
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

        public bool insertarProcesadorPago(string accion)
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
                    sql = "insertarProcesadorPago";
                }
                else
                {
                    sql = "modificarProcesadorPago";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[8];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@tipoConsecutivo", SqlDbType.Int, tipoConsecutivo);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@codigoProcesador", SqlDbType.Int, codigoProcesador);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@nombreProcesador", SqlDbType.VarChar, nombreProcesador);
                DAL.agregarEstructuraParametros(ref parametros, 3, "@tipo", SqlDbType.VarChar, tipo);
                DAL.agregarEstructuraParametros(ref parametros, 4, "@estado", SqlDbType.VarChar, estado);
                DAL.agregarEstructuraParametros(ref parametros, 5, "@requiereVerificacion", SqlDbType.VarChar, requiereVerificacion);
                DAL.agregarEstructuraParametros(ref parametros, 6, "@metodo", SqlDbType.VarChar, metodo);
                DAL.agregarEstructuraParametros(ref parametros, 7, "@codigoTarjeta", SqlDbType.Int, codigoTarjeta);

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

        public bool eliminarProcesadorPago(int codigoProcesadorPago)
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
                sql = "eliminarLineaComida";
                ParametrosStructures[] parametros = new ParametrosStructures[1];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@codigoProcesador", SqlDbType.Int, codigoProcesadorPago);
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
