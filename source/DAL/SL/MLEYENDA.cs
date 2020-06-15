using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using SLE;

namespace DAL.SL
{
    class MLEYENDA : IMAPPER<LEYENDA>
    {

        #region Singleton

        private MLEYENDA() { }

        protected static readonly MLEYENDA _instancia = new MLEYENDA();
        public static MLEYENDA Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region IMAPPER

        public int Agregar(LEYENDA Elemento)
        {
            return this.MappearParametros("Leyenda_Agregar", Elemento);
        }

        public int Modificar(LEYENDA Elemento)
        {
            return this.MappearParametros("Leyenda_modificar", Elemento);
        }

        public int Eliminar(LEYENDA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("codigo", Elemento.Codigo);
            return ACCESODB.Instancia.Escribir("Leyenda_Eliminar", p);
        }

        public List<LEYENDA> ListarTodos()
        {
            DataTable dt = ACCESODB.Instancia.Leer("Leyenda_ListarTodos", null);
            if (dt.Rows.Count > 0)
            {
                List<LEYENDA> ls = new List<LEYENDA>();
                foreach (DataRow dr in dt.Rows)
                {
                    LEYENDA l = new LEYENDA() { Codigo = (string)dr["codigo"], Nombre = (string)dr["nombre"], Texto = (string)dr["texto"] };
                    ls.Add(l);
                }
                return ls;
            }
            else { return null; }
        }

        public LEYENDA ObtenerObjeto(LEYENDA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("codigo", Elemento.Codigo);
            DataTable dt = ACCESODB.Instancia.Leer("Leyenda_ObtenerObjeto", p);
            if (dt != null)
            {
                DataRow dr = dt.Rows[0];
                return new LEYENDA() { Codigo = (string)dr["codigo"], Nombre = (string)dr["nombre"], Texto = (string)dr["texto"] };
            }
            else
            {
                return null;
            }
        }

        #endregion IMAPPER

        #region Metodos

        private int MappearParametros(string procedimiento, LEYENDA leyenda)
        {
            IDbDataParameter[] p = new IDbDataParameter[2];
            p[0] = ACCESODB.Instancia.CrearParametro("codigo", leyenda.Codigo);
            p[1] = ACCESODB.Instancia.CrearParametro("nombre", leyenda.Nombre);
            p[2] = ACCESODB.Instancia.CrearParametro("texto", leyenda.Texto);
            return ACCESODB.Instancia.Escribir(procedimiento, p);
        }

        #endregion Metodos

    }
}
