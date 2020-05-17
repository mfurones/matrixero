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
using System.Data.SqlClient;



namespace DAL.SL
{
    public class MPERMISOS
    {

        #region Singleton

        private MPERMISOS() { }

        private static readonly MPERMISOS _instancia = new MPERMISOS();
        public static MPERMISOS ObtenerInstancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Familia

        public int CrearFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("Nombre", familia.nombre);
            return ACCESODB<IACCESODB>.ObtenerInstancia.Escribir("Familia_Crear", p);
        }

        public int EliminarFamilia(FAMILIA familia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("ID_Familia", familia.ID);
            return ACCESODB<IACCESODB>.ObtenerInstancia.Escribir("Familia_Eliminar", p);
        }

        public FAMILIA ObtenerFamilia(int IDfamilia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("IDFAM", IDfamilia);
            DataTable dt = ACCESODB<IACCESODB>.ObtenerInstancia.Leer("Familia_Obtener", p);
            DataRow dr = dt.Rows[0];
            FAMILIA familia = new FAMILIA() { ID = (int)dr["Id_Familia"], nombre = (string)dr["nombre"] };
            return familia;
        }

        public List<FAMILIA> ListarFamilia()
        {
            DataTable dt = ACCESODB<IACCESODB>.ObtenerInstancia.Leer("Familia_Listar", null);
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

        public int EliminarFamiliaPatente(int IDfamilia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("ID_Fam", IDfamilia);
            return ACCESODB<IACCESODB>.ObtenerInstancia.Escribir("FamiliaPatente_BorrarPorFamilia", p);
        }

        public int ModificarFamiliaPatente( List<APATENTE> listaPatentes, int IDfamilia)
        {
            //primero borro todas filas de la familia
            this.EliminarFamiliaPatente(IDfamilia);
            //ahora cargo todas las patentes para esa familia, de esta forma tambien me sirve para nuevas familias.
            int flag = 1;
            foreach (APATENTE patente in listaPatentes)
            {
                IDbDataParameter[] p = new IDbDataParameter[1];
                p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("ID_Fam", IDfamilia);
                p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("@ID_Pat", patente.ID);
                int f = ACCESODB<IACCESODB>.ObtenerInstancia.Escribir("FamiliaPatente_Nueva", p);
                if (f < 0) { flag = -1; }
            }
            return flag;
        }

        #endregion Familia

        #region Patente

        public APATENTE ArmarEstructuraPatentes(DataTable dt)
        {
            //agrupa todos los permisos para esta familia.
            PATENTEGRUPO PActuales = new PATENTEGRUPO();
            foreach (DataRow dr in dt.Rows)
            {
                if (DBNull.Value.Equals((int)dr["padre"]))
                {
                    PATENTEGRUPO pg = new PATENTEGRUPO { ID = (int)dr["id_patente"], nombre = (string)dr["nombre"] };
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
                        PATENTE patente = new PATENTE { ID = (int)dr["Id_Patente"], nombre = (string)dr["nombre"], formulario = (string)dr["formulario"].ToString() };
                        pa.listaPatentes.Add(patente);
                    }
                }
            }
            return PActuales;
        }


        public APATENTE ObtenerPatente(int IDfamilia)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.ObtenerInstancia.CrearParametro("IDFamilia", IDfamilia);
            DataTable dt = ACCESODB<IACCESODB>.ObtenerInstancia.Leer("PatentesPorFamilia", p);
            return this.ArmarEstructuraPatentes(dt);
        }

        public APATENTE ListarPatentes()
        {
            DataTable dt = ACCESODB<IACCESODB>.ObtenerInstancia.Leer("Patentes_Listar", null);
            return this.ArmarEstructuraPatentes(dt);
        }



        #endregion Patente



    }
}
