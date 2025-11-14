using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Reflection.Emit;
using System.Text;

namespace Simulador_de_Computador_RISC_V
{
    public class CPU
    {
        public uint PC { get; set; }
        public uint[] Registradores { get; set; }
        public CPU()
        {
            PC = 0;
            Registradores = new uint[32];
        }
        public void IniciarInstrucoes(Memoria memoria)
        {
            uint instrucao = memoria.memoria[PC / 4];
            Console.WriteLine($"Instrução lida: 0x{instrucao:X8} no endereço PC: 0x{PC:X8}");
            DecodificarInstrucao(instrucao);
            iterar_pc();
        }

        private void DecodificarInstrucao(uint instrucao)
        {
            byte opcode = (byte)(instrucao & 0b01111111);
            Console.WriteLine($"Opcode da instrução: 0x{opcode:X2}");
            ULA(opcode, instrucao);
        }

        private void ULA(byte opcode, uint instrucao)
        {
            switch (opcode)
            {
                case 0b0001111:
                    {
                        byte funct3 = (byte)((instrucao >> 12) & 0b00000111);
                        // FENCE
                        if (funct3 == 000) break; //??

                        else if (funct3 == 001) break; //??
                        break;
                    }
                case 0b0010011:
                    {
                        byte funct3 = (byte)((instrucao >> 12) & 0b00000111);
                        byte funct7 = (byte)((instrucao >> 25) & 0b01111111);

                        // SLLI
                        if (funct3 == 0b001 && funct7 == 0b0000000)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = x[rs1] << shamt
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] << (int)((instrucao >> 20) & 0b11111));
                        }
                        // SRLI
                        else if (funct3 == 0b101 && funct7 == 0b0000000)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = x[rs1] >>u shamt
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] >> (int)((instrucao >> 20) & 0b11111));
                        }
                        // SRAI
                        else if (funct3 == 0b101 && funct7 == 0b0100000)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }

                            // x[rd] = x[rs1] >>s shamt
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)((int)Registradores[(instrucao >> 15) & 0b11111] >> (int)((instrucao >> 20) & 0b11111));
                        }
                        // XORI
                        else if (funct3 == 0b100)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }

                            // x[rd] = x[rs1] ^ imm
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] ^
                                (((int)instrucao) >> 20));
                        }
                        // ORI
                        else if (funct3 == 0b110)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = x[rs1] | imm
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] |
                                (((int)instrucao) >> 20));
                        }
                        // ANDI
                        else if (funct3 == 0b111)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = x[rs1] & imm
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] &
                                (((int)instrucao) >> 20));
                        }
                        // SLTI
                        else if (funct3 == 0b010)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = (x[rs1] < imm) signed
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(((int)Registradores[(instrucao >> 15) & 0b11111] < ((int)instrucao >> 20)) ? 1 : 0);
                        }
                        // SLTIU
                        else if (funct3 == 0b011)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = (x[rs1] < imm) unsigned
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)((Registradores[(instrucao >> 15) & 0b11111] < (uint)(instrucao >> 20)) ? 1 : 0);
                        }
                        // ADDI
                        else if (funct3 == 0b000)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = x[rs1] + imm
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] +
                                (((int)instrucao) >> 20));
                        }
                        break;
                    }
                    case 0b0010111:
                    {
                        byte funct3 = (byte)((instrucao >> 12) & 0b00000111);
                        byte funct7 = (byte)((instrucao >> 25) & 0b01111111);

                        // AUIPC
                        if (((instrucao >> 7) & 0b11111) == 0b0)
                        {
                            Console.WriteLine("Registrador[0] é imutável.");
                            break;
                        }
                        // x[rd] = PC + (imm << 12)
                        Registradores[(instrucao >> 7) & 0b11111] =
                            (uint)(PC + ((instrucao & 0xFFFFF000)));

                        //SLL
                        if (funct3 == 0b001 && funct7 == 0b0000000)
                        {
                            if (((instrucao >> 7) & 0b11111) == 0b0)
                            {
                                Console.WriteLine("Registrador[0] é imutável.");
                                break;
                            }
                            // x[rd] = x[rs1] << (x[rs2] & 0b11111)
                            Registradores[(instrucao >> 7) & 0b11111] =
                                (uint)(Registradores[(instrucao >> 15) & 0b11111] << (int)(Registradores[(instrucao >> 20) & 0b11111] & 0b11111));
                        }

                        break;
                    }
                default: break;
            }
            return;
        }
        private void iterar_pc()
        {
            PC += 4;
        }

    }
}
