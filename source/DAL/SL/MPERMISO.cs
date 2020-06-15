using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using SLE;



namespace DAL.SL
{
    public class MPERMISO
    {

        #region Singleton

        private MPERMISO() { }

        protected static readonly MPERMISO _instancia = new MPERMISO();
        public static MPERMISO Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Familia

        public int AgregarFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("Nombre", familia.nombre);
            return ACCESODB.Instancia.Escribir("Familia_AgregarFamilia", p);
        }

        public int EliminarFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", familia.ID);
            return ACCESODB.Instancia.Escribir("Familia_EliminarFamilia", p);
        }

        public FAMILIA ObtenerObjetoFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", familia.ID);
            DataTable dt = ACCESODB.Instancia.Leer("Familia_ObtenerObjetoFamilia", p);
            DataRow dr = dt.Rows[0];
            FAMILIA fam = new FAMILIA() { ID = (int)dr["Id_familia"], nombre = (string)dr["nombre"] };
            return fam;
        }

        public List<FAMILIA> ListarTodosFamilia()
        {
            DataTable dt = ACCESODB.Instancia.Leer("Familia_ListarTodosFamilia", null);
            if (dt.Rows.Count > 0)
            {
                List<FAMILIA> listaFamilia = new List<FAMILIA>();
                foreach (DataRow dr in dt.Rows)
                {
                    FAMILIA familia = new FAMILIA() { ID = (int)dr["Id_familia"], nombre = (string)dr["nombre"] };
                    listaFamilia.Add(familia);
                }
                return listaFamilia;
            }
            else { return null; }
        }

        public int EliminarFamiliaPatente(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", familia.ID);
            return ACCESODB.Instancia.Escribir("FamiliaPatente_EliminarFamiliaPatente", p);
        }

        public int ModificarFamiliaPatente( List<APATENTE> listaPatentes, FAMILIA familia)
        {
            //primero borro todas filas de la familia
            this.EliminarFamiliaPatente(familia);
            //ahora cargo todas las patentes para esa familia, de esta forma tambien me sirve para nuevas familias.
            int flag = 1;
            foreach (APATENTE patente in listaPatentes)
            {
                IDbDataParameter[] p = new IDbDataParameter[1];
                p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", familia.ID);
                p[1] = ACCESODB.Instancia.CrearParametro("ID_patente", patente.ID);
                int f = ACCESODB.Instancia.Escribir("FamiliaPatente_ModificarFamiliaPatente", p);
                if (f < 0) { flag = -1; }
            }
            return flag;
        }

        #endregion Familia

        #region Patente

        public APATENTE ArmarEstructuraPatente(DataTable dt)
        {
            //agrupa todos los permisos para esta familia.
            PATENTEGRUPO PActuales = new PATENTEGRUPO();
            foreach (DataRow dr in dt.Rows)
            {
                if (DBNull.Value.Equals((int)dr["padre"]))
                {
                    PATENTEGRUPO pg = new PATENTEGRUPO { ID = (int)dr["iD_patente"], Nombre = (string)dr["nombre"] };
                    PActuales.listaPatentes.Add(pg);
                }
            }
            foreach (DataRow dr in dt.Rows)
            {
                foreach (PATENTEGRUPO pa in PActuales.listaPatentes)
                {
                    //pregunto si tiene padre y si el padre es igual al que estoy recorriendo.
                    if (!DBNull.Value.Equals((int)dr["padre"]) && (pa.ID == (int)dr["padre"]))
                    {
                        PATENTE patente = new PATENTE { ID = (int)dr["ID_Patente"], Nombre = (string)dr["nombre"], Formulario = (string)dr["formulario"] };
                        pa.listaPatentes.Add(patente);
                    }
                }
            }
            return PActuales;
        }

        public APATENTE ObtenerObjetoPatente(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("ID_familia", familia.ID);
            //traerme todas las patentes de la tabla FamiliaPatente segun el ID_Familia
            DataTable dt = ACCESODB.Instancia.Leer("FamiliaPatente_ObtenerObjetoPatente", p);
            return this.ArmarEstructuraPatente(dt);
        }

        public APATENTE ListarTodosPatente()
        {
            DataTable dt = ACCESODB.Instancia.Leer("Patentes_ListarTodosPatente", null);
            return this.ArmarEstructuraPatente(dt);
        }

        #endregion Patente

    }
}
