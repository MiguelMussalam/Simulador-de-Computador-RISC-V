using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Simulador_de_Computador_RISC_V.CPU
{
    public static class Decodificador
    {
        private static readonly Dictionary<(byte opcode, byte funct3, byte funct7), Action<CPU, uint>> TableCompleta =
            new()
            {
                // I 
                { (0b0010011, 0b001, 0b0000000), ULA.SLLI },
                { (0b0010011, 0b101, 0b0000000), ULA.SRLI },
                { (0b0010011, 0b101, 0b0100000), ULA.SRAI },
        
                // R
                { (0b0110011, 0b001, 0b0000000), ULA.SLL },
                { (0b0110011, 0b101, 0b0000000), ULA.SRL },
                { (0b0110011, 0b101, 0b0100000), ULA.SRA },
                { (0b0110011, 0b000, 0b0000000), ULA.ADD },
                { (0b0110011, 0b000, 0b0100000), ULA.SUB },
                { (0b0110011, 0b100, 0b0000000), ULA.XOR },
                { (0b0110011, 0b110, 0b0000000), ULA.OR },
                { (0b0110011, 0b111, 0b0000000), ULA.AND },
                { (0b0110011, 0b010, 0b0000000), ULA.SLT },
                { (0b0110011, 0b011, 0b0000000), ULA.SLTU }
            };

        private static readonly Dictionary<(byte opcode, byte funct3), Action<CPU, uint>> TableSemFunct7 =
            new()
            {
                // I
                { (0b0010011, 0b100), ULA.XORI },
                { (0b0010011, 0b110), ULA.ORI },
                { (0b0010011, 0b111), ULA.ANDI },
                { (0b0010011, 0b010), ULA.SLTI },
                { (0b0010011, 0b011), ULA.SLTIU },
                { (0b0010011, 0b000), ULA.ADDI },
        
                // B
                { (0b1100011, 0b000), ULA.BEQ },
                { (0b1100011, 0b001), ULA.BNE },
                { (0b1100011, 0b100), ULA.BLT },
                { (0b1100011, 0b101), ULA.BGE },
                { (0b1100011, 0b110), ULA.BLTU },
                { (0b1100011, 0b111), ULA.BGEU },

                //S
                { (0b0100011, 0b000), ULA.SB },
                { (0b0100011, 0b001), ULA.SH },
                { (0b0100011, 0b010), ULA.SW },
            };

        private static readonly Dictionary<byte, Action<CPU, uint>> TableOpcode =
            new()
            {
                // U
                { 0b0110111, ULA.LUI },
                { 0b0010111, ULA.AUIPC },
            };
        public static void Executar(CPU cpu, uint instr)
        {
            Console.WriteLine($"Instrução: {instr:X8}");
            byte opcode = (byte)(instr & 0x7F);

            // Instruções U - apenas opcode
            if (opcode == 0b0110111 || opcode == 0b0010111)
            {
                if (TableOpcode.TryGetValue(opcode, out var exec))
                {
                    exec(cpu, instr);
                    return;
                }
            }

            byte funct3 = (byte)((instr >> 12) & 0x7);

            // Instruções B - apenas opcode + funct3
            if (opcode == 0b1100011)
            {
                var key = (opcode, funct3);
                if (TableSemFunct7.TryGetValue(key, out var exec))
                {
                    exec(cpu, instr);
                    return;
                }
            }

            byte funct7 = (byte)((instr >> 25) & 0x7F);

            // Instruções R e algumas I - todos os campos
            var exactKey = (opcode, funct3, funct7);
            if (TableCompleta.TryGetValue(exactKey, out var execExact))
            {
                execExact(cpu, instr);
                return;
            }

            // Instruções I - apenas opcode + funct3
            if (opcode == 0b0010011)
            {
                var key = (opcode, funct3);
                if (TableSemFunct7.TryGetValue(key, out var exec))
                {
                    exec(cpu, instr);
                    return;
                }
            }

            Console.WriteLine($"Instrução não implementada.");
        }
    }
}
