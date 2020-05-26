using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace SLE
{
    public class PATENTE : APATENTE
    {

        #region Properties

        private string _formulario;
        public string Formulario {
            get { return _formulario; }
            set { _formulario = value; }
        }

        #endregion Properties

        #region Metodos

        public override ToolStripMenuItem MostrarMS(ref MenuStrip menu)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = this.Nombre;
            tsmi.Tag = this.Formulario;
            return tsmi;
        }

        public override void MostrarTV(ref TreeNodeCollection padres, string nombre = null)
        {
            foreach (System.Windows.Forms.TreeNode padre in padres)
            {
                if (padre.Text == nombre)
                {
                    System.Windows.Forms.TreeNode node = new System.Windows.Forms.TreeNode();
                    node.Text = this.nombre;
                    node.Tag = this.ID;
                    padre.Nodes.Add(node);
                }
            }
        }

        #endregion Metodos
    }
}
