using System;
using System.Collections.Generic;
using System.Text;

// Classe para representar a memória do computador RISC-V que será alimentada direto com as instruções necessárias
// para a simulação.

// Essa memória tera um caso de exemplo: "Hello World".



//0x00429313 - Teste de SLLI
//0x0042D313 - Teste de SRLI
//0x4042D313 - Teste de SRAI
//0x00A2C313 - Teste para XORI
namespace Simulador_de_Computador_RISC_V
{
    public class Memoria
    {
        public uint[] memoria;
        
        public Memoria(bool tipo_teste)
        {
            if (tipo_teste) // ASCII
            {
                // Instruções em formato hexadecimal para imprimir todos os caracteres ASCII de 0 a 127
                memoria = new uint[]
                {0x00A2C313, //0x02800093,
                 0x00000113,
                 0x0ff00193,
                 0xc0000237,
                 0x00208023,
                 0x00220023,
                 0x00108093,
                 0x00110113,
                 0xfe21d8e3,
                 0x0000006f
                };
            }
            else
            {
                // Instruções em formato hexadecimal para imprimir "Hello"
                memoria = new uint[]
                {0x02000093,
                 0xc0000137,
                 0x0000c183,
                 0x00018863,
                 0x00310023,
                 0x00108093,
                 0xff1ff06f,
                 0x6c6c6548
                };
            }
        }
    }
}
