using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Globalization;

namespace SLE
{
    public class PATENTEGRUPO : APATENTE
    {
        #region Constructor
        public PATENTEGRUPO() { }

        #endregion Constructor

        #region Properties

        private List<APATENTE> _listaPatentes = new List<APATENTE>();
        public List<APATENTE> listaPatentes {
            get { return _listaPatentes; }
            set { _listaPatentes = value; }
        }
        #endregion Properties

        #region Metodos

        public override ToolStripMenuItem mostrarMS(ref MenuStrip menu)
        {
            System.Windows.Forms.ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = this.nombre;
            if (tsmi.Text != null)
            {
                tsmi.Tag = this.nombre.Trim().ToUpper().Substring(0, 8);
            }
            foreach (SLE p in _listaPatentes)
            {
                if (tsmi.Text == "")
                {
                    p.mostrarMS(ref menu);
                }
                else
                {
                    tsmi.DropDownItems.Add(p.mostrarMS(ref menu));
                }
            }
            if (tsmi.Text != null)
            {
                menu.Items.Add(tsmi);
            }
            return null;
            //devuelve un nothing porque es polimorfico, osea solo cuando es patente simple me sirve que me devuelva un toolstrip, 
            //cuando es grupo con agregar el item al menu alcanza
        }

        public override void mostrarTV(ref TreeNodeCollection padres, string nombre = null)
        {
            if (this.nombre != "")
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                node.Text = this.nombre;
                node.Tag = this.ID;
                padres.Add(node);
            }
            foreach (SLE p in _listaPatentes)
            {
                p.mostrarTV(ref padres, this.nombre);
            }
        }

        #endregion Metodos
    }
}
