using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//importamos la direccion de dll para trabajar con las BDs
using System.Data;
//importamos la direccion de dll para trabajar con herramientas especificas de SQLSERVER a nivel cliente
using System.Data.SqlClient;

namespace DAL
{
    class ACCESODB:IACCESODB
    {

        #region Constructor
        public ACCESODB() { }

        #endregion Constructor

        #region Conexion
        //generamos una coneccion del tipo SQLserver
        SqlConnection cn = new SqlConnection();

        private void cxAbrir()
        {
            //Initial catalog --> Nombre de la base de datos
            //Datasourse --> Servidor de sql
            //Integrated security --> La forma en que se loguea al sql (SSPI utiliza los permisos de windows)
            // = "SSPI"

            cn.ConnectionString = "Initial catalog=GENERALAMF;data source=UAISQL;Integrated Security=SSPI";
            //corroboramos si la conexion ya esta abierta
            if (cn.State != ConnectionState.Open)
            {
                cn.Open();
            }
        }

        private void cxCerrar()
        {
            //cierra la conexion
            cn.Close();
        }

        #endregion Conexion

        #region Interaccion con la BD de SQLSERVER

        DataTable IACCESODB.Leer(string procedimiento, IDbDataParameter[] parametros)
        {
            DataTable tabla = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter();
            DA.SelectCommand = new SqlCommand();
            DA.SelectCommand.CommandText = procedimiento;
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            DA.SelectCommand.Connection = cn;
            if (parametros != null)
            {
                DA.SelectCommand.Parameters.AddRange(parametros);
            }
            cxAbrir();
            try
            {
                DA.Fill(tabla);
            }
            catch (Exception ex)
            {
                tabla = null;
            }
            cxCerrar();
            return tabla;
        }

        int IACCESODB.Escribir(string procedimiento, IDbDataParameter[] parametros)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = procedimiento;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            cmd.Parameters.AddRange(parametros);
            cxAbrir();
            int fa;
            try
            {
                fa = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                fa = -1;
            }
            cxCerrar();
            return fa;
        }

        #endregion Interaccion con la BD de SQLSERVER

        #region Parametros para SQLSERVER

        IDbDataParameter IACCESODB.CrearParametro(string nombre, string valor)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = "@" + nombre;
            p.DbType = DbType.String;
            p.Value = valor;
            return p;
        }

        IDbDataParameter IACCESODB.CrearParametro(string nombre, int valor)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = "@" + nombre;
            p.DbType = DbType.Int32;
            p.Value = valor;
            return p;
        }

        #endregion Parametros para SQLSERVER
    }
}
