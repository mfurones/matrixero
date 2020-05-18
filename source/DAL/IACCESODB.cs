using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//importamos la direccion de dll para trabajar con las BDs
using System.Data;


namespace DAL
{
    //interfaz de accesos para unificar los metodos e independizar la implementacion segun el motor de BD que utilice
    public interface IACCESODB
    {
        //metodo para realizar una lectura
        DataTable Leer(string procedimiento, IDbDataParameter[] parametros);

        //metodo para realizar una escritura
        int Escribir(string procedimiento, IDbDataParameter[] parametros);

        //metodo para establecer un parametro del tipo string
        IDbDataParameter CrearParametro(string nombre, string valor);

        //metodo para establecer un parametro del tipo integer
        IDbDataParameter CrearParametro(string nombre, int valor);

    }
}
