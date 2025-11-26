using System;
using System.Collections.Generic;
using System.Text;

namespace Simulador_de_Computador_RISC_V
{
    internal class Barramento
    {
        public uint LerMemoria(uint endereco, Memoria memoria)
        {
            return memoria.memoria[endereco / 4];
        }

        public uint EscreverMemoria(uint endereco, uint valor, Memoria memoria)
        {
            memoria.memoria[endereco / 4] = valor;
            return valor;
        }
    }
}
