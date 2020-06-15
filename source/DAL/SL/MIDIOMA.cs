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
    class MIDIOMA : IMAPPER<IDIOMA>
    {
        #region Singleton

        private MIDIOMA() { }

        protected static readonly MIDIOMA _instancia = new MIDIOMA();
        public static MIDIOMA Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region IMAPPER

        public int Agregar(IDIOMA Elemento)
        {
            return this.MappearParametros("Agregar", Elemento);
        }

        public int Modificar(IDIOMA Elemento)
        {
            return this.MappearParametros("Modificar", Elemento);
        }

        public int Eliminar(IDIOMA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("codigo", Elemento.Codigo);
            return ACCESODB.Instancia.Escribir("Idioma_Eliminar", p);
        }

        public List<IDIOMA> ListarTodos()
        {
            DataTable dt = ACCESODB.Instancia.Leer("Idioma_ListarTodos", null);
            if (dt.Rows.Count > 0)
            {
                List<IDIOMA> ls = new List<IDIOMA>();
                foreach (DataRow dr in dt.Rows)
                {
                    IDIOMA u = new IDIOMA() { Codigo = (string)dr["codigo"], Nombre = (string)dr["nombre"], ListaLeyendas = ListarLeyendas((string)dr["codigo"]) };
                    ls.Add(u);
                }
                return ls;
            }
            else { return null; }
        }

        public IDIOMA ObtenerObjeto(IDIOMA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("codigo", Elemento.Codigo);
            DataTable dt = ACCESODB.Instancia.Leer("Idioma_ObtenerObjeto", p);
            if (dt != null)
            {
                DataRow dr = dt.Rows[0];
                return new IDIOMA() { Codigo = (string)dr["codigo"], Nombre = (string)dr["nombre"], ListaLeyendas = ListarLeyendas((string)dr["codigo"]) };
            }
            else
            {
                return null;
            }
        }

        #endregion IMAPPER

        #region Metodos

        private int MappearParametros(string procedimiento, IDIOMA idioma)
        {
            int f = 1;
            IDbDataParameter[] p = new IDbDataParameter[1];
            p[0] = ACCESODB.Instancia.CrearParametro("nombre", idioma.Nombre);
            p[1] = ACCESODB.Instancia.CrearParametro("codigo", idioma.Codigo);
            if (ACCESODB.Instancia.Escribir($"Idioma_{procedimiento}", p) < 1)
            {
                f = 0;
                //escribir bitacora
            }
            foreach (LEYENDA l in idioma.ListaLeyendas)
            {
                if (this.MPleyenda($"IdiomaLeyenda_{procedimiento}", l, idioma) < 1)
                {
                    f = 0;
                    //escribir bitacora
                }
            }
            return f;
        }

        private int MPleyenda(string procedimiento, LEYENDA leyenda, IDIOMA idioma)
        {
            IDbDataParameter[] p = new IDbDataParameter[1];
            p[0] = ACCESODB.Instancia.CrearParametro("codigoL", leyenda.Codigo);
            p[1] = ACCESODB.Instancia.CrearParametro("codigoI", idioma.Codigo);
            return ACCESODB.Instancia.Escribir(procedimiento, p);
        }

        private List<LEYENDA> ListarLeyendas(string idiomaCodigo)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("codigoI", idiomaCodigo);
            DataTable dt = ACCESODB.Instancia.Leer("IdiomaLeyenda_ListarLeyendas", p);
            if (dt.Rows.Count > 0)
            {
                List<LEYENDA> ls = new List<LEYENDA>();
                foreach (DataRow dr in dt.Rows)
                {
                    ls.Add(MLEYENDA.Instancia.ObtenerObjeto(new LEYENDA() { Codigo = (string)dr["codigoI"] }));
                }
                return ls;
            }
            else { return null; }
        }

        #endregion Metodos
    }
}
