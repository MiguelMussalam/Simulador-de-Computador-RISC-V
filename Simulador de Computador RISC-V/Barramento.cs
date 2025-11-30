using System;
using System.Collections.Generic;
using System.Text;

namespace Simulador_de_Computador_RISC_V
{
    public class Barramento
    {
        CPU.CPU Cpu { get; set; }
        Memoria Memoria { get; set; }

        public void ReferenciarModulos(CPU.CPU Cpu, Memoria Memoria)
        {
            this.Cpu = Cpu;
            this.Memoria = Memoria;
        }
        public uint LerDadoMemoria(uint endereco)
        {
            return Memoria.Ram[endereco / 4];
        }

        public void EnviarDadoMemoria(uint endereco, uint valor, Memoria.TamanhoAcesso tamanhoAcesso)
        {
            Memoria.Escrever(endereco, valor, tamanhoAcesso);
        }
    }
}
