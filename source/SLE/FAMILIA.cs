using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLE
{
    public class FAMILIA
    {

        private int _ID;
        public int ID 
        {
            get { return _ID; }
            set { _ID = value; }        
        }

        private string _nombre;
        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

    }
}
