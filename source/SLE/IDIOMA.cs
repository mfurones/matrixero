using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLE
{
    public class IDIOMA
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

        private List<LEYENDA> _listaLeyendas;
        public List<LEYENDA> ListaLeyendas
        {
            get { return _listaLeyendas; }
            set { _listaLeyendas = value; }
        }

        #endregion Properties

        #region Metodos

        public override string ToString()
        {
            return this.Nombre;
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
                IDIOMA p = (IDIOMA)obj;
                return this.Codigo == p.Codigo;
            }
        }


        #endregion Metodos

    }
}
