using System;
using System.Collections.Generic;
using System.Reflection.Emit;
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
                return;
            }

            Console.WriteLine("Operação SLLI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] << shamt
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] <<
                (int)((instr >> 20) & 0b11111));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) << shamt({(int)((instr >> 20) & 0b11111)})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SRLI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SRLI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] >> shamt
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] >>
                (int)((instr >> 20) & 0b11111));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) >> shamt({(int)((instr >> 20) & 0b11111)})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SRAI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SRAI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] >> shamt (aritmético)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] >>
                (int)((instr >> 20) & 0b11111));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) >> shamt({(int)((instr >> 20) & 0b11111)})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void XORI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação XORI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] ^ imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] ^
                (((int)instr) >> 20));
            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) ^ imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void ORI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação ORI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] | imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] |
                (((int)instr) >> 20));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) | imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void ANDI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação ANDI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] & imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] &
                (((int)instr) >> 20));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) & imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }

        public static void SLTI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SLTI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = (x[rs1] < imm) ? 1 : 0
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(((int)cpu.Registradores[(instr >> 15) & 0b11111] <
                (((int)instr) >> 20)) ? 1 : 0);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) < imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SLTIU(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SLTIU\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = (x[rs1] < imm) ? 1 : 0 (unsigned)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((cpu.Registradores[(instr >> 15) & 0b11111] <
                (uint)(((int)instr) >> 20)) ? 1 : 0);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) < imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }

        public static void ADDI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação ADDI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] + imm
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] +
                (((int)instr) >> 20));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) + imm({((int)instr) >> 20})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void AUIPC(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }
            Console.WriteLine("Operação AUIPC\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = PC + (imm << 12)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.PC +
                (((int)instr & 0xFFFFF000)));

            Console.WriteLine($"Operação: PC({cpu.PC}) + imm<<12({((int)instr & 0xFFFFF000)})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }

        public static void SLL(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }
            Console.WriteLine("Operação SLL\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] << (x[rs2] & 0b11111)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] << (int)(cpu.Registradores[(instr >> 20) & 0b11111] & 0b11111));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) << (rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}) & 0b11111)");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SRL(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }
            Console.WriteLine("Operação SRL\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] >> (x[rs2] & 0b11111)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] >> (int)(cpu.Registradores[(instr >> 20) & 0b11111] & 0b11111));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) >> (rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}) & 0b11111)");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SRA(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }
            Console.WriteLine("Operação SRA\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] >> (x[rs2] & 0b11111) (aritmético)
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] >> (int)(cpu.Registradores[(instr >> 20) & 0b11111] & 0b11111));

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) >> (rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}) & 0b11111)");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void ADD(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação ADD\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] + x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] +
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) + rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SUB(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SUB\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] - x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((int)cpu.Registradores[(instr >> 15) & 0b11111] -
                (int)cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) - rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void XOR(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }
            Console.WriteLine("Operação XOR\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");
            // x[rd] = x[rs1] ^ x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] ^
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) ^ rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void OR(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação OR\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] | x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] |
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) | rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void AND(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação AND\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = x[rs1] & x[rs2]
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(cpu.Registradores[(instr >> 15) & 0b11111] &
                cpu.Registradores[(instr >> 20) & 0b11111]);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) & rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SLT(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SLT\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = (x[rs1] < x[rs2]) signed
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(((int)cpu.Registradores[(instr >> 15) & 0b11111] < (int)cpu.Registradores[(instr >> 20) & 0b11111]) ? 1 : 0);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) < rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void SLTU(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação SLTU\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = (x[rs1] < x[rs2]) unsigned
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)((cpu.Registradores[(instr >> 15) & 0b11111] < cpu.Registradores[(instr >> 20) & 0b11111]) ? 1 : 0);

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) < rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void LUI(CPU cpu, uint instr)
        {
            if (((instr >> 7) & 0b11111) == 0b0)
            {
                Console.WriteLine("Registrador[0] é imutável.");
                return;
            }

            Console.WriteLine("Operação LUI\n");
            Console.WriteLine($"rd antes da operação: {cpu.Registradores[((instr >> 7) & 0b11111)]}");

            // x[rd] = imm << 12
            cpu.Registradores[(instr >> 7) & 0b11111] =
                (uint)(((int)instr & 0xFFFFF000));

            Console.WriteLine($"Operação: imm<<12({((int)instr & 0xFFFFF000)})");
            Console.WriteLine($"rd depois da operação: {cpu.Registradores[(instr >> 7) & 0b11111]}\n\n");
        }


        public static void BEQ(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BEQ\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC}");

            // if (x[rs1] == x[rs2]) PC += imm
            if (cpu.Registradores[(instr >> 15) & 0b11111] == cpu.Registradores[(instr >> 20) & 0b11111])
            {
                cpu.PC += (uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12));
            }

            Console.WriteLine($"Operação: se rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) == rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}), então: PC({cpu.PC}) += IMM({(uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12))}) ");
            Console.WriteLine($"PC depois da operação: {cpu.PC}\n\n");
        }


        public static void BNE(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BNE\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC}");

            // if (x[rs1] != x[rs2]) PC += imm
            if (cpu.Registradores[(instr >> 15) & 0b11111] != cpu.Registradores[(instr >> 20) & 0b11111])
            {
                cpu.PC += (uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12));
            }

            Console.WriteLine($"Operação: se rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) != rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}), então: PC({cpu.PC}) += IMM({(uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12))}) ");
            Console.WriteLine($"PC depois da operação: {cpu.PC}\n\n");
        }


        public static void BLT(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BLT\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC}");

            // if (x[rs1] < x[rs2]) signed PC += imm
            if ((int)cpu.Registradores[(instr >> 15) & 0b11111] < (int)cpu.Registradores[(instr >> 20) & 0b11111])
            {
                cpu.PC += (uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12));
            }

            Console.WriteLine($"Operação: se rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) < rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}), então: Signed PC({cpu.PC}) += IMM({(uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12))}) ");
            Console.WriteLine($"PC depois da operação: {cpu.PC}\n\n");
        }


        public static void BGE(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BGE\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC}");

            // if (x[rs1] >= x[rs2]) signed PC += imm
            if ((int)cpu.Registradores[(instr >> 15) & 0b11111] >= (int)cpu.Registradores[(instr >> 20) & 0b11111])
            {
                cpu.PC += (uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12));
            }
            Console.WriteLine($"Operação: se rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) >= rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]}), então: Signed PC({cpu.PC}) += IMM({(uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12))}) ");
            Console.WriteLine($"PC depois da operação: {cpu.PC}\n\n");
        }


        public static void BLTU(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BLTU\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC}");

            // if (x[rs1] < x[rs2]) unsigned PC += imm
            if (cpu.Registradores[(instr >> 15) & 0b11111] < cpu.Registradores[(instr >> 20) & 0b11111])
            {
                cpu.PC += (uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12));
            }

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) < rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"PC depois da operação: {cpu.PC}\n\n");
        }


        public static void BGEU(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação BGEU\n");
            Console.WriteLine($"PC antes da operação: {cpu.PC}");
            // if (x[rs1] >= x[rs2]) unsigned PC += imm
            if (cpu.Registradores[(instr >> 15) & 0b11111] >= cpu.Registradores[(instr >> 20) & 0b11111])
            {
                cpu.PC += (uint)((((int)(instr >> 7) & 0b1) << 11) |
                                 (((int)(instr >> 25) & 0b111111) << 5) |
                                 (((int)(instr >> 8) & 0b1111) << 1) |
                                 (((int)(instr >> 31) & 0b1) << 12));
            }

            Console.WriteLine($"Operação: rs1({(uint)cpu.Registradores[(instr >> 15) & 0b11111]}) >= rs2({(uint)cpu.Registradores[(instr >> 20) & 0b11111]})");
            Console.WriteLine($"PC depois da operação: {cpu.PC}\n\n");
        }


        public static void SB(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação SB\n");
            Console.WriteLine($"Memória antes da operação: 'ula vai pedir pro barramento a info - futuro'");
        }


        public static void SH(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação SH\n");
            Console.WriteLine($"Memória antes da operação: 'ula vai pedir pro barramento a info - futuro'");
        }


        public static void SW(CPU cpu, uint instr)
        {
            Console.WriteLine("Operação SW\n");
            Console.WriteLine($"Memória antes da operação: 'ula vai pedir pro barramento a info - futuro'");
        }
    }
}
