using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SLE;

namespace SL
{
    public class GPERMISOS
    {

        #region Singleton

        private GPERMISOS() { }

        private static readonly GPERMISOS _instancia = new GPERMISOS();
        public static GPERMISOS ObtenerInstancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Metodos

        public APATENTE ObtenerPatente(FAMILIA familia)
        {
            //Implementar como obtener una patente --> mapper

            return new PATENTE();
        }

        public APATENTE ListarPatente()
        {
            //Implementar como obtener un listado de patentes --> mapper

            return new PATENTE();
        }

        public List<FAMILIA> ListarFamilia()
        {
            //Implementar como obtener un listado de patentes --> mapper

            return new List<FAMILIA>();
        }

        public int CrearFamilia(FAMILIA familia)
        {
            //Implementar bitacora
            //Implementar crear familia --> mapper

            return -1;
        }

        public int ModificarFamilia(List<APATENTE> listaPatentes, FAMILIA familia)
        {
            //Implementar bitacora
            //Implementar modificar familia --> mapper

            return -1;
        }

        public int EliminarFamilia(FAMILIA familia)
        {
            //Implementar bitacora
            //Implementar eliminar familia --> mapper

            return -1;
        }

        #endregion Metodos

    }
}
