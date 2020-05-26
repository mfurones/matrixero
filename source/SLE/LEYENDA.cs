using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLE
{
    public class LEYENDA : IComparable<LEYENDA>
    {

        #region Properties

        private string _nombre;
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private string _codigo;
        public string Codigo
        {
            get { return _codigo; }
            set { _codigo = value; }
        }

        private string _texto;
        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        #endregion Properties

        #region Metodos

        public override string ToString()
        {
            return this.Codigo;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                LEYENDA p = (LEYENDA)obj;
                return this.Codigo == p.Codigo;
            }
        }

        public int CompareTo(LEYENDA obj)
        {
            // If other is not a valid object reference, this instance is greater.
            if (obj == null) return 1;
            return this.Nombre.CompareTo(obj.Nombre);
        }

        #endregion Metodos
    }
}
