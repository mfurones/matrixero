using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SLE;

namespace SL
{
    public class GFAMILIA : IGESTOR<FAMILIA>
    {

        #region Singleton

        private GFAMILIA() { }

        protected static readonly GFAMILIA _instancia = new GFAMILIA();
        public static GFAMILIA Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Metodos

        public void Agregar(FAMILIA Elemento)
        {
            //Implementar bitacora
            //Implementar crear familia --> mapper
        }

        public void Modificar(FAMILIA Elemento)
        {
            //Implementar bitacora
            //Implementar modificar familia --> mapper
        }

        public void Eliminar(FAMILIA Elemento)
        {
            //Implementar bitacora
            //Implementar eliminar familia --> mapper
        }

        public List<FAMILIA> ListarTodos()
        {
            //Implementar como obtener un listado de patentes --> mapper

            return new List<FAMILIA>();
        }

        public FAMILIA ObtenerObjeto(FAMILIA Elemento)
        {
            //Implementar como obtener un listado de patentes --> mapper

            return new FAMILIA();
        }

        #endregion Metodos

    }
}
