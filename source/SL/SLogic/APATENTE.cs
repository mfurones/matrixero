﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace SL.SLogic
{
    abstract class APATENTE
    {

        #region Properties
        private string _nombre;
        public string nombre {
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

        public abstract System.Windows.Forms.ToolStripMenuItem mostrarMS(ref System.Windows.Forms.MenuStrip menu);

        public abstract void mostrarTV(ref System.Windows.Forms.TreeNodeCollection padres, string nombre = null);

        #endregion Metodos




    }
}