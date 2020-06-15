using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SL
{
    public interface IOBSERVADO
    {
        List<IOBSERVADOR> ListaForm { get; set; }

        void Registrar(IOBSERVADOR o);

        void Remover(IOBSERVADOR o);

        void Notificar();

    }
}
