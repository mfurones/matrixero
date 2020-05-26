using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    public interface IGESTOR<T>
    {
        void Agregar(T Elemento);

        void Modificar(T Elemento);

        void Eliminar(T Elemento);

        List<T> ListarTodos();

        T ObtenerObjeto(T Elemento);

    }
}
