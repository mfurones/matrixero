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

        #region Properties

        private List<APATENTE> _listaPatentes = new List<APATENTE>();
        public List<APATENTE> listaPatentes {
            get { return _listaPatentes; }
            set { _listaPatentes = value; }
        }
        
        #endregion Properties

        #region Metodos

        public override ToolStripMenuItem MostrarMS(ref MenuStrip menu)
        {
            System.Windows.Forms.ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = this.Nombre;
            if (tsmi.Text != null)
            {
                tsmi.Tag = this.Nombre.Trim().ToUpper().Substring(0, 8);
            }
            foreach (APATENTE p in _listaPatentes)
            {
                if (tsmi.Text == "")
                {
                    p.MostrarMS(ref menu);
                }
                else
                {
                    tsmi.DropDownItems.Add(p.MostrarMS(ref menu));
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

        public override void MostrarTV(ref TreeNodeCollection padres, string nombre = null)
        {
            if (this.Nombre != "")
            {
                System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                node.Text = this.Nombre;
                node.Tag = this.ID;
                padres.Add(node);
            }
            foreach (APATENTE p in _listaPatentes)
            {
                p.MostrarTV(ref padres, this.Nombre);
            }
        }

        #endregion Metodos
    }
}
