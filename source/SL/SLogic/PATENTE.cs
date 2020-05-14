using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace SL.SLogic
{
    class PATENTE : APATENTE

    {

        #region Properties

        private string _formulario;
        public string formulario {
            get { return _formulario; }
            set { _formulario = value; }
        }

        #endregion Properties

        #region Metodos

        public override ToolStripMenuItem mostrarMS(ref MenuStrip menu)
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = this.nombre;
            tsmi.Tag = this.formulario;
            return tsmi;
        }

        public override void mostrarTV(ref TreeNodeCollection padres, string nombre = null)
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
