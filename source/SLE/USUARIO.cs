using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLE
{
    public class USUARIO
    {

        private string _nombre;
        public string Nombre {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _apellido;
        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        private string _usuario;
        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        private string _contraseña;
        public string Contraseña
        {
            get { return _contraseña; }
            set { _contraseña = value; }
        }

        private FAMILIA _familia;
        public FAMILIA Familia
        {
            get { return _familia; }
            set { _familia = value; }
        }

        private IDIOMA _idioma;
        public IDIOMA Idioma
        {
            get { return _idioma; }
            set { _idioma = value; }
        }

        #region Metodos

        public override bool Equals(object obj)
        {
            USUARIO usr = (USUARIO)obj;
            if (usr != null)
            {
                return false;
            }
            else
            {
                return _usuario.Equals(usr._usuario);
            }
        }

        public override string ToString()
        {
            return this._nombre + ", " + this._apellido; 
        }

        #endregion Metodos



    }
}
