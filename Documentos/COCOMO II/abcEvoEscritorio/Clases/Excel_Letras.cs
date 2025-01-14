using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCEvoEscritorio.Clases
{
    internal class Excel_Letras
    {
        public char letra(int numero)
        {
            char estaLetra = ' ';

            if (numero <= 26)
            {
                estaLetra = Convert.ToChar(64 + numero);
            }
            else if (numero <= 52)
            {
                estaLetra = Convert.ToChar(64 + numero - 26);
            }
            else if (numero <= 78)
            {
                estaLetra = Convert.ToChar(64 + numero - 52);
            }
            else if (numero <= 104)
            {
                estaLetra = Convert.ToChar(64 + numero - 78);
            }
            else if (numero <= 130)
            {
                estaLetra = Convert.ToChar(64 + numero - 104);
            }
            else if (numero <= 156)
            {
                estaLetra = Convert.ToChar(64 + numero - 130);
            }

            return estaLetra;
        }
    }
}
