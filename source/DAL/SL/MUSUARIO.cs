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
    public class MUSUARIO : IMAPPER<USUARIO>
    {

        #region Singleton

        private MUSUARIO() { }

        protected static readonly MUSUARIO _instancia = new MUSUARIO();
        public static MUSUARIO Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        private int MappearParametros(string procedimiento, USUARIO usuario)
        {
            IDbDataParameter[] p = new IDbDataParameter[5];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("nombre", usuario.Nombre);
            p[1] = ACCESODB<IACCESODB>.Instancia.CrearParametro("apellido", usuario.Apellido);
            p[2] = ACCESODB<IACCESODB>.Instancia.CrearParametro("usuario", usuario.Usuario);
            p[3] = ACCESODB<IACCESODB>.Instancia.CrearParametro("contraseña", usuario.Contraseña);
            p[4] = ACCESODB<IACCESODB>.Instancia.CrearParametro("familia", usuario.Familia.ID);
            // falta definir idioma
            //p[5] = ACCESODB<IACCESODB>.Instancia.CrearParametro("idioma", usuario.Idioma);
            return ACCESODB<IACCESODB>.Instancia.Escribir(procedimiento, p);
        }

        public int Agregar(USUARIO Elemento)
        {
            return this.MappearParametros("Familia_crear", Elemento);
        }

        public int Modificar(USUARIO Elemento)
        {
            return this.MappearParametros("Familia_modificar", Elemento);
        }

        public int Eliminar(USUARIO Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("usuario", Elemento.Usuario);
            return ACCESODB<IACCESODB>.Instancia.Escribir("Familia_eliminar", p);
        }

        public List<USUARIO> ListarTodos()
        {
            DataTable dt = ACCESODB<IACCESODB>.Instancia.Leer("Usuario_Listar", null);
            if (dt.Rows.Count > 0)
            {
                List<USUARIO> ls = new List<USUARIO>();
                foreach (DataRow dr in dt.Rows)
                {
                    //agregar idioma
                    USUARIO u = new USUARIO() { Nombre = (string)dr["nombre"], Apellido = (string)dr["apellido"], Usuario = (string)dr["usuario"], Contraseña = (string)dr["contraseña"], Familia = MPERMISO.Instancia.ObtenerFamilia((int)dr["familia"]), Idioma = null };
                    ls.Add(u);
                }
                return ls;
            }
            else { return null; }
        }

        public USUARIO ObtenerObjeto(USUARIO Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB<IACCESODB>.Instancia.CrearParametro("usuario", Elemento.Usuario);
            DataTable dt = ACCESODB<IACCESODB>.Instancia.Leer("Usuario_Obtener", p);
            if (dt != null)
            {
                DataRow dr = dt.Rows[0];
                //agregar idioma
                return new USUARIO() { Nombre = (string)dr["nombre"], Apellido = (string)dr["apellido"], Usuario = (string)dr["usuario"], Contraseña = (string)dr["contraseña"], Familia = MPERMISO.Instancia.ObtenerFamilia((int)dr["familia"]), Idioma = null };
            }
            else
            {
                return null;
            }

        }
    }
}
