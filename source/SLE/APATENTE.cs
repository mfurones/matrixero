using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace SLE
{
    public abstract class APATENTE
    {

        #region Properties
        private string _nombre;
        public string Nombre {
            get { return _nombre; }
            set { _nombre = value; }
        }

        private int _ID;
        public int ID {
            get { return _ID; }
            set { _ID = value; }
        }

        #endregion Properties

        #region Metodos

        public abstract System.Windows.Forms.ToolStripMenuItem MostrarMS(ref System.Windows.Forms.MenuStrip menu);

        public abstract void MostrarTV(ref System.Windows.Forms.TreeNodeCollection padres, string nombre = null);

        #endregion Metodos

    }
}
