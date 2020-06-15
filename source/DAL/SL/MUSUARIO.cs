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

        #region IMAPPER

        public int Agregar(USUARIO Elemento)
        {
            return this.MappearParametros("Usuario_agregar", Elemento);
        }

        public int Modificar(USUARIO Elemento)
        {
            return this.MappearParametros("Usuario_modificar", Elemento);
        }

        public int Eliminar(USUARIO Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("usuario", Elemento.Usuario);
            return ACCESODB.Instancia.Escribir("Usuario_eliminar", p);
        }

        public List<USUARIO> ListarTodos()
        {
            DataTable dt = ACCESODB.Instancia.Leer("Usuario_ListarTodos", null);
            if (dt.Rows.Count > 0)
            {
                List<USUARIO> ls = new List<USUARIO>();
                foreach (DataRow dr in dt.Rows)
                {
                    IDIOMA i = MIDIOMA.Instancia.ObtenerObjeto(new IDIOMA { Codigo = (string)dr["idioma_codigo"] });
                    FAMILIA f = MPERMISO.Instancia.ObtenerObjetoFamilia(new FAMILIA { ID = (int)dr["ID_familia"] });
                    USUARIO u = new USUARIO() { Nombre = (string)dr["nombre"], Apellido = (string)dr["apellido"], Usuario = (string)dr["usuario"], Contraseña = (string)dr["contraseña"], Familia = f, Idioma = i };
                    ls.Add(u);
                }
                return ls;
            }
            else { return null; }
        }

        public USUARIO ObtenerObjeto(USUARIO Elemento)
        {
            IDbDataParameter[] p = new IDbDataParameter[0];
            p[0] = ACCESODB.Instancia.CrearParametro("usuario", Elemento.Usuario);
            DataTable dt = ACCESODB.Instancia.Leer("Usuario_ObtenerObjeto", p);
            if (dt != null)
            {
                DataRow dr = dt.Rows[0];
                IDIOMA i = MIDIOMA.Instancia.ObtenerObjeto(new IDIOMA { Codigo = (string)dr["idioma_codigo"] });
                FAMILIA f = MPERMISO.Instancia.ObtenerObjetoFamilia(new FAMILIA { ID = (int)dr["ID_familia"] });
                return new USUARIO() { Nombre = (string)dr["nombre"], Apellido = (string)dr["apellido"], Usuario = (string)dr["usuario"], Contraseña = (string)dr["contraseña"], Familia = f, Idioma = i };
            }
            else
            {
                return null;
            }

        }

        #endregion IMAPPERI

        #region Metodos

        private int MappearParametros(string procedimiento, USUARIO usuario)
        {
            IDbDataParameter[] p = new IDbDataParameter[5];
            p[0] = ACCESODB.Instancia.CrearParametro("nombre", usuario.Nombre);
            p[1] = ACCESODB.Instancia.CrearParametro("apellido", usuario.Apellido);
            p[2] = ACCESODB.Instancia.CrearParametro("usuario", usuario.Usuario);
            p[3] = ACCESODB.Instancia.CrearParametro("contraseña", usuario.Contraseña);
            p[4] = ACCESODB.Instancia.CrearParametro("familia", usuario.Familia.ID);
            p[5] = ACCESODB.Instancia.CrearParametro("codigo", usuario.Idioma.Codigo);
            return ACCESODB.Instancia.Escribir(procedimiento, p);
        }

        #endregion Metodos

    }
}
