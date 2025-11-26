using System;
using System.Collections.Generic;
using System.Text;

namespace Simulador_de_Computador_RISC_V
{
    public class Memoria
    {
        public uint[] memoria;
        private int enderecoAtual;

        public Memoria(byte tipo_teste)
        {
            // Inicializa memória com 4092 bytes (1023 palavras de 32 bits)
            memoria = new uint[1023]; // 1023 * 4 = 4092 bytes
            enderecoAtual = 0;

            // Zera toda a memória
            for (int i = 0; i < memoria.Length; i++)
            {
                memoria[i] = 0;
            }

            // Adiciona instruções conforme o tipo de teste
            switch (tipo_teste)
            {
                case 0b00000001: // ASCII
                    AdicionarInstrucoesASCII();
                    break;

                case 0b00000010: // Hello World
                    AdicionarInstrucoesHelloWorld();
                    break;

                case 0b00000011: // Teste de operações RV32I
                    AdicionarTesteOperacoesRV32I();
                    break;

                default:
                    // Memória vazia, apenas inicializada
                    break;
            }
        }

        private void AdicionarInstrucoesASCII()
        {
            // Instruções para imprimir todos os caracteres ASCII de 0 a 127
            uint[] instrucoes = {
                0x02800093, // addi x1, x0, 40
                0x00000113, // addi x2, x0, 0
                0x0ff00193, // addi x3, x0, 255
                0xc0000237, // lui x4, 0xC0000
                0x00208023, // sb x2, 0(x1)
                0x00220023, // sb x2, 0(x4)
                0x00108093, // addi x1, x1, 1
                0x00110113, // addi x2, x2, 1
                0xfe21d8e3, // bne x1, x3, -16
                0x0000006f  // jal x0, 0 (loop infinito)
            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarInstrucoesHelloWorld()
        {
            // Instruções para imprimir "Hello"
            uint[] instrucoes = {
                0x02000093, // addi x1, x0, 32
                0xc0000137, // lui x2, 0xC0000
                0x0000c183, // lbu x3, 0(x1)
                0x00018863, // beq x3, x0, 16
                0x00310023, // sb x3, 0(x2)
                0x00108093, // addi x1, x1, 1
                0xff1ff06f, // jal x0, -16
                0x6c6c6548  // "Hell" (dados)
            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarTesteOperacoesRV32I()
        {
            // Instruções para teste de todas as operações RV32I
            uint[] instrucoes = {
                // Teste de instruções I
                0x00429313, // slli x6, x5, 4
                0x0042D313, // srli x6, x5, 4
                0x4042D313, // srai x6, x5, 4
                0x00A2C313, // xori x6, x5, 10
                0x00A2E313, // ori x6, x5, 10
                0x00A2F313, // andi x6, x5, 10
                0x00A2A313, // slti x6, x5, 10
                0x00A2B313, // sltiu x6, x5, 10
                0x00A28313, // addi x6, x5, 10
                
                // Teste de instruções R
                0x00629333, // sll x6, x5, x6
                0x0062D333, // srl x6, x5, x6
                0x4062D333, // sra x6, x5, x6
                0x00628333, // add x6, x5, x6
                0x40628333, // sub x6, x5, x6
                0x0062C333, // xor x6, x5, x6
                0x0062E333, // or x6, x5, x6
                0x0062F333, // and x6, x5, x6
                0x0062A333, // slt x6, x5, x6
                0x0062B333, // sltu x6, x5, x6
                
                // Teste de instruções U
                0x00001337, // lui x6, 1
                0x00002337, // auipc x6, 2
                
                // Teste de instruções B
                0x00528463, // beq x5, x5, 8
                0x00529463, // bne x5, x5, 8
                0x0052C463, // blt x5, x5, 8
                0x0052D463, // bge x5, x5, 8
                0x0052E463, // bltu x5, x5, 8
                0x0052F463, // bgeu x5, x5, 8
            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarArrayInstrucoes(uint[] instrucoes)
        {
            foreach (uint instrucao in instrucoes)
            {
                if (enderecoAtual < memoria.Length)
                {
                    memoria[enderecoAtual] = instrucao;
                    enderecoAtual++;
                }
                else
                {
                    Console.WriteLine("Aviso: Memória insuficiente para todas as instruções");
                    break;
                }
            }
        }

        // Método para adicionar instruções manualmente após a inicialização
        public void AdicionarInstrucao(uint instrucao)
        {
            if (enderecoAtual < memoria.Length)
            {
                memoria[enderecoAtual] = instrucao;
                enderecoAtual++;
            }
            else
            {
                Console.WriteLine("Erro: Memória cheia");
            }
        }

        // Método para adicionar múltiplas instruções
        public void AdicionarInstrucoes(uint[] instrucoes)
        {
            AdicionarArrayInstrucoes(instrucoes);
        }

        // Método para ler uma instrução da memória
        public uint LerInstrucao(int endereco)
        {
            if (endereco >= 0 && endereco < memoria.Length)
            {
                return memoria[endereco];
            }
            else
            {
                Console.WriteLine($"Erro: Endereço de memória inválido: {endereco}");
                return 0;
            }
        }
        public int ObterEnderecoAtual()
        {
            return enderecoAtual;
        }

        public void Limpar()
        {
            for (int i = 0; i < memoria.Length; i++)
            {
                memoria[i] = 0;
            }
            enderecoAtual = 0;
        }
    }
}