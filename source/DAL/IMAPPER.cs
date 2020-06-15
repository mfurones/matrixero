using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IMAPPER<T>
    {

        int Agregar(T Elemento);

        int Modificar(T Elemento);

        int Eliminar(T Elemento);

        List<T> ListarTodos();

        T ObtenerObjeto(T Elemento);


    }
}
