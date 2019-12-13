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
    public class Cheque
    {
        private int monto;
        private string numeroCheque, cuenta;

        public int Monto
        {
            get { return monto; }
            set { monto = value; }
        }

        public string NumeroCheque
        {
            get { return numeroCheque; }
            set { numeroCheque = value; }
        }

        public string Cuenta
        {
            get { return cuenta; }
            set { cuenta = value; }
        }

        #region Variables para Conexion
        SqlConnection conn;
        string mensajeError;
        int numError;
        string sql;
        DataSet ds;
        #endregion

        public string cargarCheque()
        {
            conn = DAL.traerConexion("public", ref mensajeError, ref numError);
            if (conn == null)
            {
                return null;
            }
            else
            {
                sql = "cargarCheque";
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

        public bool insertarCheque(string accion)
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
                    sql = "insertarCheque";
                }
                else
                {
                    sql = "modificarCheque";
                }
                ParametrosStructures[] parametros = new ParametrosStructures[3];
                DAL.agregarEstructuraParametros(ref parametros, 0, "@numeroCheque", SqlDbType.VarChar, numeroCheque);
                DAL.agregarEstructuraParametros(ref parametros, 1, "@cuenta", SqlDbType.VarChar, cuenta);
                DAL.agregarEstructuraParametros(ref parametros, 2, "@monto", SqlDbType.Int, monto);

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
