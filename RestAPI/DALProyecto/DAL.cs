using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DALProyecto
{

    public struct ParametrosStructures
    {
        public string nombreParametro;
        public SqlDbType tipoDato;
        public Object valorParametro;
    }
    public static class DAL
    {
        /// <summary>
        /// Inicializa una conexion contra un servidor de base de datos, retorna un sqlconnection si todo esta bien y null si fallo
        /// </summary>
        /// <param name="nombreConexion">nombre de la cadena conexion a ejecutar</param>
        /// <param name="mensajeError">mensaje de error o confirmacion</param>
        /// <param name="numError">numero del error</param>
        /// <returns></returns>
        /// 

        public static SqlConnection traerConexion(string nombreConexion, ref string mensajeError, ref int numError)
        {
            SqlConnection conn;

            try
            {
                string cadenaConexion = "";

                cadenaConexion = ConfigurationManager.ConnectionStrings[nombreConexion].ToString();

                conn = new SqlConnection(cadenaConexion);
                mensajeError = String.Empty;
                numError = 0;
                return conn;
            }
            catch (NullReferenceException ex)
            {
                mensajeError = "NO SE ENCONTRO LA CADENA DE CONEXION: " + nombreConexion + "/nERROR: " + ex.Message;
                numError = -1;
                return null;
            }
            catch (ConfigurationException ex)
            {
                mensajeError = "ERROR: " + ex.Message;
                numError = -2;
                return null;
            }
        }



        /// <summary>
        /// realiza conexion contra la base de datos
        /// </summary>
        /// <param name="conn">variable estilo sqlconnection que contiene la cadena de conexion</param>
        /// <param name="mensajeError">mensaje de error o confirmacion</param>
        /// <param name="numError">numero de error</param>
        public static void conectar(SqlConnection conn, ref string mensajeError, ref int numError)
        {
            try
            {
                conn.Open();
                mensajeError = "ok";
                numError = 0;
            }
            catch (SqlException ex)
            {
                mensajeError = "ERROR CON LA CONEXION DEL SERVIDOR CON LA BASE DE DATOS. /n ERROR:" + ex.Message;
                numError = ex.Number;
            }
        }

        /// <summary>
        /// realiza desconeccion contra la base de datos
        /// </summary>
        /// <param name="conn">variable estilo sqlconnection que contiene la cadena de conexion</param>
        /// <param name="mensajeError">mensaje de error o confirmacion</param>
        public static void desconectar(SqlConnection conn, ref string mensajeError, ref int numError)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    mensajeError = "CONEXION CERRADA";
                    numError = 0;
                }
                else
                {
                    conn.Close();
                    mensajeError = "CONEXION SIGQUE ABEIRTA";
                }
            }
            catch (SqlException ex)
            {
                mensajeError = "ERROR CON LA CONEXION DEL SERVIDOR CON LA BASE DE DATOS. /nERROR" + ex.Message;
                numError = ex.Number;
            }
        }

        /// <summary>
        /// realiza la carga de un dataset de solo lectura.
        /// </summary>
        /// <param name="sql">Sentencia sql o nombre del SP</param>
        /// <param name="conn">variable donde recide la conexion</param>        
        /// <param name="numError">numero de error</param>
        /// <param name="mensajeError">mensaje de error</param>
        /// <param name="procedimientoAlmacenado">indica si se ejecuta un procedimiento almacenado</param>
        ///            
        public static DataSet ejecutarDataSet(SqlConnection conn, string sql, bool procedimientoAlmacenado, ref string mensajeError, ref int numError)
        {
            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet();
            try
            {
                dataAdapter = new SqlDataAdapter(sql, conn);
                if (procedimientoAlmacenado)
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                }
                dataAdapter.Fill(dataSet);
                numError = 0;
                mensajeError = "ok";
                return dataSet;
            }
            catch (SqlException ex)
            {
                numError = ex.Number;
                mensajeError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// realiza la carga de un dataset con parametros.
        /// </summary>
        /// <param name="sql">Sentencia sql o nombre del procedimiento almacenado</param>
        /// <param name="conn">variable donde recide la conexion</param>        
        /// <param name="numError">numero de error</param>
        /// <param name="mensajeError">mensaje de error</param>
        /// <param name="parametros">lista de parametros que necesita el procedimiento almacenado o sentencia SQL para su ejecución</param>        
        /// <param name="procedimientoAlmacenado">indica si se ejecuta un procedimiento almacenado</param>        
        ///
        public static DataSet ejecutarDataSetParametros(SqlConnection conn, string sql, bool procedimientoAlmacenado, ParametrosStructures[] parametros, ref string mensajeError, ref int numError)
        {
            SqlDataAdapter dataAdapter;
            DataSet dataSet = new DataSet();
            try
            {
                dataAdapter = new SqlDataAdapter(sql, conn);
                if (procedimientoAlmacenado)
                {
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                }
                foreach (ParametrosStructures var in parametros)
                {
                    agregarParametroAdapter(ref dataAdapter, var.nombreParametro, var.valorParametro.ToString(), var.tipoDato);
                }
                dataAdapter.Fill(dataSet);
                numError = 0;
                mensajeError = "ok";
                return dataSet;
            }
            catch (SqlException ex)
            {
                numError = ex.Number;
                mensajeError = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="query"></param>
        /// <param name="mensajeError"></param>
        /// <param name="numError"></param>
        public static void ejecutarDataReader(ref SqlDataReader dataReader, SqlCommand query, ref string mensajeError, ref int numError)
        {
            try
            {
                dataReader = query.ExecuteReader(CommandBehavior.CloseConnection);
                numError = 0;
                mensajeError = "ok";
            }
            catch (SqlException ex)
            {
                numError = ex.Number;
                mensajeError = ex.Message;
            }
        }
        /// <summary>
        /// ejecuta una sentecia de tipo SQL contra la base de datos
        /// </summary>
        /// <param name="conn">variable donde recide la conexion</param>
        /// <param name="sql">sentencia sql o nombre del procedimiento almacenado</param>
        /// <param name="procedimientoAlmacenado">indica si se ejecuta un procedimiento almacenado</param>
        /// <param name="mensajeError">mensaje de error</param>
        /// <param name="numError">numero de error</param>        
        public static void ejecutaSqlCommand(SqlConnection conn, string sql, bool procedimientoAlmacenado, ref string mensajeError, ref int numError)
        {
            SqlCommand query;
            try
            {
                query = new SqlCommand(sql, conn);
                if (procedimientoAlmacenado)
                {
                    query.CommandType = CommandType.StoredProcedure;
                }
                int resultado = 0;
                resultado = query.ExecuteNonQuery();
                mensajeError = "ok";
                numError = 0;
            }
            catch (SqlException ex)
            {
                mensajeError = "ERROR AL EJECUTAR LA SENTENCIA DEL SQL. /nERROR: " + ex.Message;
                numError = ex.Number;
            }
        }
        /// <summary>
        /// ejecuta una sentecia de tipo SQL contra la base de datos
        /// </summary>
        /// <param name="conn">variable donde recide la conexion</param>
        /// <param name="sql">sentencia sql o nombre del procedimiento almacenado</param>
        /// <param name="procedimientoAlmacenado">indica si se ejecuta un procedimiento almacenado</param>
        /// <param name="parametros">lista de parametros que necesita el procedimiento almacenado o sentencia SQL para su ejecucion</param>
        /// <param name="mensajeError">mensaje de error</param>
        /// <param name="numError">numero de error</param>        
        public static void ejecutarSqlCommandParametros(SqlConnection conn, string sql, bool procedimientoAlmacenado, ParametrosStructures[] parametros, ref string mensajeError, ref int numError)
        {
            SqlCommand query;
            try
            {
                int resultado = 0;
                query = new SqlCommand(sql, conn);
                if (procedimientoAlmacenado)
                {
                    query.CommandType = CommandType.StoredProcedure;
                }
                foreach (ParametrosStructures var in parametros)
                {
                    agregarParametroCommand(ref query, var.nombreParametro, var.valorParametro.ToString(), var.tipoDato);
                }
                resultado = query.ExecuteNonQuery();
                mensajeError = "ok";
                numError = 0;
            }
            catch (SqlException ex)
            {
                mensajeError = "ERROR AL EJECUTAR LA SENTENCIA DEL SQL. /nERROR: " + ex.Message;
                numError = ex.Number;
            }
        }

        public static void agregarParametroCommand(ref SqlCommand command, string nombreParametro, string valorParametro, SqlDbType tipoDato)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombreParametro;
            parametro.Value = valorParametro;
            parametro.SqlDbType = tipoDato;
            command.Parameters.Add(parametro);
        }

        public static void agregarParametroAdapter(ref SqlDataAdapter dataAdapter, string nombreParametro, string valorParametro, SqlDbType tipoDato)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nombreParametro;
            parametro.Value = valorParametro;
            parametro.SqlDbType = tipoDato;
            dataAdapter.SelectCommand.Parameters.Add(parametro);
        }

        public static void agregarEstructuraParametros(ref ParametrosStructures[] parametros, int posicion, string nombreParametro, SqlDbType tipoDato, object valorParametro)
        {
            parametros[posicion].nombreParametro = nombreParametro.ToString();
            parametros[posicion].tipoDato = tipoDato;
            parametros[posicion].valorParametro = valorParametro;
        }
    }
}
