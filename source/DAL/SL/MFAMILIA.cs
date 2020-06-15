using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using SLE;

namespace DAL.SL
{
    class MFAMILIA : IMAPPER<FAMILIA>
    {

        #region Singleton

        private MFAMILIA() { }

        protected static readonly MFAMILIA _instancia = new MFAMILIA();
        public static MFAMILIA Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Mapper

        public int Agregar(FAMILIA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("Nombre", Elemento.nombre);
            return ACCESODB.Instancia.Escribir("Familia_Agregar", p);
        }

        public int Eliminar(FAMILIA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", Elemento.ID);
            return ACCESODB.Instancia.Escribir("Familia_Eliminar", p);
        }

        public List<FAMILIA> ListarTodos()
        {
            DataTable dt = ACCESODB.Instancia.Leer("Familia_ListarTodos", null);
            if (dt.Rows.Count > 0)
            {
                List<FAMILIA> listaFamilia = new List<FAMILIA>();
                foreach (DataRow dr in dt.Rows)
                {
                    FAMILIA familia = new FAMILIA() { ID = (int)dr["ID_familia"], nombre = (string)dr["nombre"] };
                    listaFamilia.Add(familia);
                }
                return listaFamilia;
            }
            else { return null; }
        }

        public int Modificar(FAMILIA Elemento)
        {
            return 0;
        }

        public FAMILIA ObtenerObjeto(FAMILIA Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", Elemento.ID);
            DataTable dt = ACCESODB.Instancia.Leer("Familia_ObtenerObjeto", p);
            DataRow dr = dt.Rows[0];
            FAMILIA fam = new FAMILIA() { ID = (int)dr["ID_familia"], nombre = (string)dr["nombre"] };
            return fam;
        }

        #endregion Mapper

        #region Metodos

        public int ModificarFamiliaPatente(List<APATENTE> listaPatentes, FAMILIA familia)
        {
            //primero borro todas filas de la familia
            this.Eliminar(familia);
            //ahora cargo todas las patentes para esa familia, de esta forma tambien me sirve para nuevas familias.
            int flag = 1;
            foreach (APATENTE patente in listaPatentes)
            {
                IDbDataParameter[] p = new IDbDataParameter[1];
                p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", familia.ID);
                p[0] = ACCESODB.Instancia.CrearParametro("ID_patente", patente.ID);
                int f = ACCESODB.Instancia.Escribir("FamiliaPatente_ModificarFamiliaPatente", p);
                if (f < 0) { flag = -1; }
            }
            return flag;
        }

        #endregion Metodos

    }
}
