using SLE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    public class GSESION : IOBSERVADO
    {

        #region Singleton

        private GSESION() { }

        protected static readonly GSESION _instancia = new GSESION();
        public static GSESION Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Properties

        private APATENTE _perfilActivo;
        public APATENTE PerfilActivo {
            get { return _perfilActivo; }
        }

        private USUARIO _usuarioActivo;
        public USUARIO UsuarioActivo {
            get { return _usuarioActivo; }
        }

        #endregion Properties

        #region Metodos

        public bool IniciarSesion(USUARIO usuario)
        {
            bool b = false;
            USUARIO u = GUSUARIO.Instancia.ObtenerObjeto(usuario);
            if (u != null)
            {
                if (GUSUARIO.Instancia.ValidarContraseña(u, usuario))
                {
                    this._usuarioActivo = u;
                    this._perfilActivo = GFAMILIA.Instancia.ObtenerPatente(this.UsuarioActivo.Familia);
                    b = true;
                    // registrar con la bitacora
                }
            }
            return b;
        }

        public void CerrarSesion()
        {
            // implementar bitacora

        }

        #endregion Metodos

        #region Observer

        public List<IOBSERVADOR> ListaForm { get; set; }

        public void Notificar()
        {
            // Terminar de implementar (GUSUARIO/GIDIOMA)
            foreach (IOBSERVADOR ob in ListaForm)
            {
                ob.TraducirForm();
            }
        }

        public void Registrar(IOBSERVADOR o)
        {
            ListaForm.Add(o);
            o.TraducirForm();
        }

        public void Remover(IOBSERVADOR o)
        {
            ListaForm.Remove(o);
        }

        #endregion Observer

    }
}
