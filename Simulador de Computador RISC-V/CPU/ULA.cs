using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;

namespace Simulador_de_Computador_RISC_V.CPU
{
    public static class ULA
    {
        public static void SLLI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SLLI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] << shamt
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] <<
                (int)((instr >> 20) & 0b11111));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) << shamt({(int)((instr >> 20) & 0b11111)})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SRLI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SRLI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] >> shamt
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] >>
                (int)((instr >> 20) & 0b11111));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) >> shamt({(int)((instr >> 20) & 0b11111)})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SRAI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SRAI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] >> shamt (aritmético)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] >>
                (int)((instr >> 20) & 0b11111));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) >> shamt({(int)((instr >> 20) & 0b11111)})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void XORI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação XORI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] ^ imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] ^
                (((int)instr) >> 20));
            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) ^ imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void ORI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação ORI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] | imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] |
                (((int)instr) >> 20));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) | imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void ANDI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação ANDI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] & imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] &
                (((int)instr) >> 20));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) & imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }

        public static void SLTI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SLTI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = (x[rs1] < imm) ? 1 : 0
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(((int)cpu.Registradores[(instr >> 15) & 0b11111] <
                (((int)instr) >> 20)) ? 1 : 0);

            Console.WriteLine($"Operação: rs1(0x{(int)cpu.Registradores[(instr >> 15) & 0b11111]}) < imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SLTIU(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SLTIU\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = (x[rs1] < imm) ? 1 : 0 (unsigned)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((cpu.Registradores[(instr >> 15) & 0b11111] <
                (uint)(((int)instr) >> 20)) ? 1 : 0);

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) < imm({(uint)(((int)instr) >> 20)})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }

        public static void ADDI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação ADDI");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] + imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] + (((int)instr) >> 20));

            Console.WriteLine($"Operação: rs1(0x{(int)cpu.Registradores[(instr >> 15) & 0b11111]:X8}) + imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void AUIPC(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }
            Console.WriteLine("Operação AUIPC\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = PC + (imm << 12)
            uint offset = instr & 0xFFFFF000;
            cpu.Registradores[(instr >> 7) & 0b11111] = cpu.PC + offset;

            Console.WriteLine($"Operação: PC(0x{cpu.PC:X8}) + imm<<12(0x{offset:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }

        public static void SLL(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }
            Console.WriteLine("Operação SLL\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] << (x[rs2] & 0b11111)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] << (int)(cpu.Registradores[(instr >> 20) & 0b11111] & 0b11111));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) << (rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8}) & 0b11111)");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SRL(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }
            Console.WriteLine("Operação SRL\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] >> (x[rs2] & 0b11111)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] >> (int)(cpu.Registradores[(instr >> 20) & 0b11111] & 0b11111));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) >> (rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8}) & 0b11111)");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SRA(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }
            Console.WriteLine("Operação SRA\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] >> (x[rs2] & 0b11111) (aritmético)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] >> (int)(cpu.Registradores[(instr >> 20) & 0b11111] & 0b11111));

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) >> (rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8}) & 0b11111)");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void ADD(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação ADD\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] + x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] +
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) + rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SUB(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SUB\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] - x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] -
                (int)cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1(0x{(int)cpu.Registradores[(instr >> 15) & 0b11111]:X8}) - rs2(0x{(int)cpu.Registradores[(instr >> 20) & 0b11111]:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void XOR(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }
            Console.WriteLine("Operação XOR\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");
            // x[rd] = x[rs1] ^ x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] ^
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) ^ rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void OR(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação OR\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] | x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] |
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) | rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void AND(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação AND\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = x[rs1] & x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] &
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) & rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SLT(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SLT\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = (x[rs1] < x[rs2]) signed
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(((int)cpu.Registradores[(instr >> 15) & 0b11111] < (int)cpu.Registradores[(instr >> 20) & 0b11111]) ? 1 : 0);

            Console.WriteLine($"Operação: rs1(0x{(int)cpu.Registradores[(instr >> 15) & 0b11111]}) < rs2(0x{(int)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void SLTU(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação SLTU\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = (x[rs1] < x[rs2]) unsigned
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((cpu.Registradores[(instr >> 15) & 0b11111] < cpu.Registradores[(instr >> 20) & 0b11111]) ? 1 : 0);

            Console.WriteLine($"Operação: rs1(0x{cpu.Registradores[(instr >> 15) & 0b11111]:X8}) < rs2(0x{cpu.Registradores[(instr >> 20) & 0b11111]:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }


        public static void LUI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                cpu.iterar_pc();
                return;
            }

            Console.WriteLine("Operação LUI\n");
            Console.WriteLine($"rd antes da operação: 0x{cpu.Registradores[((instr >> 7) & 0b11111)]:X8}");

            // x[rd] = imm << 12
            uint imm = instr & 0xFFFFF000;
            cpu.Registradores[(instr >> 7) & 0b11111] = imm;

            Console.WriteLine($"Operação: imm<<12(0x{imm:X8})");
            Console.WriteLine($"rd depois da operação: 0x{cpu.Registradores[(instr >> 7) & 0b11111]:X8}\n\n");

            cpu.iterar_pc();
        }

        public static void BEQ(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BEQ\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC:X8}");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;


            // if (x[rs1] == x[rs2]) pc += sext(offset)
            if (cpu.Registradores[rs1_index] == cpu.Registradores[rs2_index])
            {
                uint imm12 = (instr >> 31) & 0x1;
                uint imm11 = (instr >> 7) & 0x1;
                uint imm10_5 = (instr >> 25) & 0x3F;
                uint imm4_1 = (instr >> 8) & 0xF;

                uint offset_13bit = (imm12 << 12) | (imm11 << 11) | (imm10_5 << 5) | (imm4_1 << 1);
                int final_offset = ((int)(offset_13bit << 19)) >> 19;

                cpu.PC = (uint)((int)cpu.PC + final_offset);
                Console.WriteLine($"BEQ: Branch tomado! Offset: {final_offset}. Novo PC: {cpu.PC:X8}");
            }
            else
            {
                cpu.iterar_pc();
                Console.WriteLine("Sem Branch.");
            }
            Console.WriteLine($"PC depois da operação: {cpu.PC:X8}\n\n");
        }

        public static void BNE(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BNE\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC:X8}");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;

            // if (x[rs1] != x[rs2]) pc += sext(offset)
            if (cpu.Registradores[rs1_index] != cpu.Registradores[rs2_index])
            {
                uint imm12 = (instr >> 31) & 0x1;
                uint imm11 = (instr >> 7) & 0x1;
                uint imm10_5 = (instr >> 25) & 0x3F;
                uint imm4_1 = (instr >> 8) & 0xF;

                uint offset_13bit = (imm12 << 12) | (imm11 << 11) | (imm10_5 << 5) | (imm4_1 << 1);
                int final_offset = ((int)(offset_13bit << 19)) >> 19;

                cpu.PC = (uint)((int)cpu.PC + final_offset);
                Console.WriteLine($"BNE: Branch tomado! Offset: {final_offset}. Novo PC: {cpu.PC:X8}");
            }
            else
            {
                cpu.iterar_pc();
                Console.WriteLine("Sem Branch.");
            }
            Console.WriteLine($"PC depois da operação: {cpu.PC:X8}\n\n");
        }

        public static void BLT(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BLT\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC:X8}");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;

            // if (x[rs1] <s x[rs2]) pc += sext(offset)
            if ((int)cpu.Registradores[rs1_index] < (int)cpu.Registradores[rs2_index])
            {
                uint imm12 = (instr >> 31) & 0x1;
                uint imm11 = (instr >> 7) & 0x1;
                uint imm10_5 = (instr >> 25) & 0x3F;
                uint imm4_1 = (instr >> 8) & 0xF;

                uint offset_13bit = (imm12 << 12) | (imm11 << 11) | (imm10_5 << 5) | (imm4_1 << 1);
                int final_offset = ((int)(offset_13bit << 19)) >> 19;

                cpu.PC = (uint)((int)cpu.PC + final_offset);
                Console.WriteLine($"BLT: Branch tomado! Offset: {final_offset}. Novo PC: {cpu.PC:X8}");
            }
            else
            {
                cpu.iterar_pc();
                Console.WriteLine("Sem Branch.");
            }
            Console.WriteLine($"PC depois da operação: {cpu.PC:X8}\n\n");
        }


        public static void BGE(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BGE\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC:X8}");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;


            // if (x[rs1] >= s x[rs2]) pc += sext(offset)
            if ((int)cpu.Registradores[rs1_index] >= (int)cpu.Registradores[rs2_index])
            {
                uint imm12 = (instr >> 31) & 0x1;
                uint imm11 = (instr >> 7) & 0x1;
                uint imm10_5 = (instr >> 25) & 0x3F;
                uint imm4_1 = (instr >> 8) & 0xF;

                uint offset_13bit = (imm12 << 12) | (imm11 << 11) | (imm10_5 << 5) | (imm4_1 << 1);

                int final_offset = ((int)(offset_13bit << 19)) >> 19;

                cpu.PC = (uint)((int)cpu.PC + final_offset);

                Console.WriteLine($"Desvio tomado! Offset calculado: {final_offset}. Novo PC: {cpu.PC:X8}");
            }
            else
            {
                cpu.iterar_pc();
                Console.WriteLine("Sem Branch.");
            }
            Console.WriteLine($"PC depois da operação: {cpu.PC:X8} \n\n");
        }


        public static void BLTU(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BLTU\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC:X8}");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;


            // if (x[rs1] <u x[rs2]) pc += sext(offset)
            if (cpu.Registradores[rs1_index] < cpu.Registradores[rs2_index])
            {
                uint imm12 = (instr >> 31) & 0x1;
                uint imm11 = (instr >> 7) & 0x1;
                uint imm10_5 = (instr >> 25) & 0x3F;
                uint imm4_1 = (instr >> 8) & 0xF;

                uint offset_13bit = (imm12 << 12) | (imm11 << 11) | (imm10_5 << 5) | (imm4_1 << 1);
                int final_offset = ((int)(offset_13bit << 19)) >> 19;

                cpu.PC = (uint)((int)cpu.PC + final_offset);
                Console.WriteLine($"BLTU: Branch tomado! Offset: {final_offset}. Novo PC: {cpu.PC:X8}");
            }
            else
            {
                cpu.iterar_pc();
                Console.WriteLine("Sem Branch.");
            }
            Console.WriteLine($"PC depois da operação: {cpu.PC:X8}\n\n");
        }

        public static void BGEU(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BGEU\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC:X8}");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;


            // if (x[rs1] >=u x[rs2]) pc += sext(offset)
            if (cpu.Registradores[rs1_index] >= cpu.Registradores[rs2_index])
            {
                uint imm12 = (instr >> 31) & 0x1;
                uint imm11 = (instr >> 7) & 0x1;
                uint imm10_5 = (instr >> 25) & 0x3F;
                uint imm4_1 = (instr >> 8) & 0xF;

                uint offset_13bit = (imm12 << 12) | (imm11 << 11) | (imm10_5 << 5) | (imm4_1 << 1);
                int final_offset = ((int)(offset_13bit << 19)) >> 19;

                cpu.PC = (uint)((int)cpu.PC + final_offset);
                Console.WriteLine($"BGEU: Branch tomado! Offset: {final_offset}. Novo PC: {cpu.PC:X8}");
            }
            else
            {
                cpu.iterar_pc();
                Console.WriteLine("Sem Branch.");
            }
            Console.WriteLine($"PC depois da operação: {cpu.PC:X8}\n\n");
        }


        public static void SB(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação SB\n");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;
            uint endereco_base = cpu.Registradores[rs1_index];
            uint dado = cpu.Registradores[rs2_index];

            uint imm_11_5 = (instr >> 25) & 0b1111111;
            uint imm_4_0 = (instr >> 7) & 0b11111;
            uint offset_12bit = (imm_11_5 << 5) | imm_4_0;
            int final_offset = ((int)(offset_12bit << 20)) >> 20;
            uint endereco_final = (uint)((int)endereco_base + final_offset);

            byte byte_para_salvar = (byte)(dado & 0xFF);
            cpu.Barramento.EnviarDadoMemoria(endereco_final, byte_para_salvar, Memoria.TamanhoAcesso.Byte);

            Console.WriteLine($"Operação: Memória[rs1(0x{endereco_base:X8}) + IMM({final_offset})] = rs2(0x{dado:X8}) & 0xFF");
            Console.WriteLine($"Calculado: Memória[0x{endereco_final:X8}] = 0x{byte_para_salvar:X2}");

            cpu.iterar_pc();
        }


        public static void SH(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação SH\n");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;
            uint endereco_base = cpu.Registradores[rs1_index];
            uint dado = cpu.Registradores[rs2_index];

            uint imm_11_5 = (instr >> 25) & 0b1111111;
            uint imm_4_0 = (instr >> 7) & 0b11111;
            uint offset_12bit = (imm_11_5 << 5) | imm_4_0;
            int final_offset = ((int)(offset_12bit << 20)) >> 20;
            uint endereco_final = (uint)((int)endereco_base + final_offset);

            ushort halfword_para_salvar = (ushort)(dado & 0xFFFF);

            cpu.Barramento.EnviarDadoMemoria(endereco_final, halfword_para_salvar, Memoria.TamanhoAcesso.Half);

            Console.WriteLine($"Operação: Memória[rs1(0x{endereco_base:X8}) + IMM({final_offset})] = rs2(0x{dado:X8}) & 0xFFFF");
            Console.WriteLine($"Calculado: Memória[0x{endereco_final:X8}] = 0x{halfword_para_salvar:X4}");

            cpu.iterar_pc();
        }


        public static void SW(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação SW\n");

            uint rs1_index = (instr >> 15) & 0x1F;
            uint rs2_index = (instr >> 20) & 0x1F;
            uint endereco_base = cpu.Registradores[rs1_index];
            uint dado = cpu.Registradores[rs2_index];

            uint imm_11_5 = (instr >> 25) & 0b1111111;
            uint imm_4_0 = (instr >> 7) & 0b11111;
            uint offset_12bit = (imm_11_5 << 5) | imm_4_0;
            int final_offset = ((int)(offset_12bit << 20)) >> 20;
            uint endereco_final = (uint)((int)endereco_base + final_offset);

            uint word_para_salvar = dado;
            cpu.Barramento.EnviarDadoMemoria(endereco_final, word_para_salvar, Memoria.TamanhoAcesso.Word);

            Console.WriteLine($"Operação: Memória[rs1(0x{endereco_base:X8}) + IMM({final_offset})] = rs2(0x{dado:X8})");
            Console.WriteLine($"Calculado: Memória[0x{endereco_final:X8}] = 0x{word_para_salvar:X8}");

            cpu.iterar_pc();
        }
    }
}