using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Windows.Forms;

namespace SL
{
    public class CRIPTOGRAFIA
    {
        #region Singleton

        private CRIPTOGRAFIA() { }

        protected static readonly CRIPTOGRAFIA _instancia = new CRIPTOGRAFIA();
        public static CRIPTOGRAFIA Instancia
        {
            get { return _instancia; }
        }

        #endregion Singleton

        #region Properties


        #endregion Properties

        #region Metodos

        // Devuelve el valor del string en hash MD5
        public string GetHashMD5(string value)
        {
            string str = "";
            try
            {
                UnicodeEncoding ue = new UnicodeEncoding();
                byte[] byteSourceText = ue.GetBytes(value);
                MD5CryptoServiceProvider MD5cripto = new MD5CryptoServiceProvider();
                byte[] bytehash = MD5cripto.ComputeHash(byteSourceText);
                MD5cripto.Clear();
                str = Convert.ToBase64String(bytehash);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return str;
        }

        // Compara 2 valores Hash
        public bool CompareHashMD5(string hash1, string hash2)
        {
            return hash2.Equals(hash1);
        }

        #endregion Metodos

    }
}
