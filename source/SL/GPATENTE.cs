using SLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    class GPATENTE : IGESTOR<APATENTE>
    {

        #region Singleton

        private GPATENTE() { }

        protected static readonly GPATENTE _instancia = new GPATENTE();
        public static GPATENTE Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Metodos

        public void Agregar(APATENTE Elemento)
        {
            //Implementar como obtener una patente --> mapper
            //Implementar bitacora
        }

        public void Eliminar(APATENTE Elemento)
        {
            //Implementar como obtener una patente --> mapper
            //Implementar bitacora
        }

        public void Modificar(APATENTE Elemento)
        {
            //Implementar como obtener una patente --> mapper
            //Implementar bitacora
        }

        public List<APATENTE> ListarTodos()
        {
            //Implementar como obtener una patente --> mapper
            return new List<APATENTE>();
        }

        public APATENTE ObtenerObjeto(APATENTE Elemento)
        {
            //Implementar como obtener una patente --> mapper
            return new PATENTE();
        }

        #endregion Metodos

    }
}