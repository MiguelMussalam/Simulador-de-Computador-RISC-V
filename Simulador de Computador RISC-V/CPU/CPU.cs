using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Text;

namespace Simulador_de_Computador_RISC_V.CPU
{
    public class CPU
    {
        public uint PC { get; set; }
        public uint[] Registradores { get; set; }

        public Barramento Barramento;
        public CPU(Barramento barramento)
        {
            PC = 0;
            Registradores = new uint[32];
            this.Barramento = barramento;
        }

        public void InicializarRandomicamenteRegistradores()
        {
            Random rand = new Random();
            for (int i = 0; i < 32; i++)
            {
                Registradores[i] = (uint)rand.Next(1,100);
                Console.WriteLine($"Registrador x{i}: {Registradores[i]}");
            }
        }

        public void InicializarRegistradoresZerados()
        {
            for (int i = 0; i < 32; i++)
            {
                Registradores[i] = 0;
            }
        }

        public void iterar_pc()
        {
            PC += 4;
        }

    }
}
