using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SLE;
using DAL;
using DAL.SL;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SL
{
    public class GUSUARIO : IGESTOR<USUARIO>
    {

        #region Singleton

        private GUSUARIO() { }

        protected static readonly GUSUARIO _instancia = new GUSUARIO();
        public static GUSUARIO Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Metodos

        #region Mapper

        public void Agregar(USUARIO usuario)
        {
            if (ValidarFormato(usuario.Usuario) && ValidarFormato(usuario.Contraseña))
            {
                usuario.Contraseña = CRIPTOGRAFIA.Instancia.GetHashMD5(usuario.Contraseña);
                MUSUARIO.Instancia.Agregar(usuario);
                // implementar Bitacora
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("El nombre de usuario y la Contraseña deben ser alfanumericos de 6 a 15 caracteres", "",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        public void Modificar(USUARIO usuario)
        {
            if (ValidarFormato(usuario.Usuario) && ValidarFormato(usuario.Contraseña))
            {
                usuario.Contraseña = CRIPTOGRAFIA.Instancia.GetHashMD5(usuario.Contraseña);
                MUSUARIO.Instancia.Modificar(usuario);
                // implementar Bitacora
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("El nombre de usuario y la Contraseña deben ser alfanumericos de 6 a 15 caracteres", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        public void Eliminar(USUARIO usuario)
        {
            MUSUARIO.Instancia.Eliminar(usuario);
            // implementar bitacora
        }

        public USUARIO ObtenerObjeto(USUARIO usuario)
        {
            return MUSUARIO.Instancia.ObtenerObjeto(usuario);
        }

        public List<USUARIO> ListarTodos()
        {
            return MUSUARIO.Instancia.ListarTodos();
        }

        #endregion Mapper

        public bool ValidarContraseña(USUARIO usuarioDB, USUARIO usuarioSesion)
        {
            return CRIPTOGRAFIA.Instancia.CompareHashMD5(usuarioDB.Contraseña, CRIPTOGRAFIA.Instancia.GetHashMD5(usuarioSesion.Contraseña));
        }

        private bool ValidarFormato(string texto)
        {
            Regex reg = new Regex("^[A-Za-z0-9]{6,15}");
            return reg.IsMatch(texto);
        }

        public void ModificarIdioma(USUARIO usuario)
        {
            MUSUARIO.Instancia.Modificar(usuario);
            // implementar bitacora
        }

        #endregion Metodos

    }
}
