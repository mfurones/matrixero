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
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("Nombre", familia.nombre);
            return ACCESODB<IACCESODB>.Instancia.Escribir("Familia_Crear", p);
        }

        public int EliminarFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("ID_Familia", familia.ID);
            return ACCESODB<IACCESODB>.Instancia.Escribir("Familia_Eliminar", p);
        }

        public FAMILIA ObtenerObjetoFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("IDFAM", familia.ID);
            DataTable dt = ACCESODB<IACCESODB>.Instancia.Leer("Familia_Obtener", p);
            DataRow dr = dt.Rows[0];
            FAMILIA fam = new FAMILIA() { ID = (int)dr["Id_Familia"], nombre = (string)dr["nombre"] };
            return fam;
        }

        public List<FAMILIA> ListarTodosFamilia()
        {
            DataTable dt = ACCESODB<IACCESODB>.Instancia.Leer("Familia_Listar", null);
            if (dt.Rows.Count > 0)
            {
                List<FAMILIA> listaFamilia = new List<FAMILIA>();
                foreach (DataRow dr in dt.Rows)
                {
                    FAMILIA familia = new FAMILIA() { ID = (int)dr["Id_Familia"], nombre = (string)dr["Nombre"] };
                    listaFamilia.Add(familia);
                }
                return listaFamilia;
            }
            else { return null; }
        }

        public int EliminarFamiliaPatente(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("ID_Fam", familia.ID);
            return ACCESODB<IACCESODB>.Instancia.Escribir("FamiliaPatente_BorrarPorFamilia", p);
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
                p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("ID_Fam", familia.ID);
                p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("@ID_Pat", patente.ID);
                int f = ACCESODB<IACCESODB>.Instancia.Escribir("FamiliaPatente_Nueva", p);
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
                    PATENTEGRUPO pg = new PATENTEGRUPO { ID = (int)dr["id_patente"], Nombre = (string)dr["nombre"] };
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
                        PATENTE patente = new PATENTE { ID = (int)dr["Id_Patente"], Nombre = (string)dr["nombre"], Formulario = (string)dr["formulario"].ToString() };
                        pa.listaPatentes.Add(patente);
                    }
                }
            }
            return PActuales;
        }

        public APATENTE ObtenerObjetoPatente(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("IDFamilia", familia.ID);
            DataTable dt = ACCESODB<IACCESODB>.Instancia.Leer("PatentesPorFamilia", p);
            return this.ArmarEstructuraPatente(dt);
        }

        public APATENTE ListarTodosPatente()
        {
            DataTable dt = ACCESODB<IACCESODB>.Instancia.Leer("Patentes_Listar", null);
            return this.ArmarEstructuraPatente(dt);
        }

        #endregion Patente

    }
}
