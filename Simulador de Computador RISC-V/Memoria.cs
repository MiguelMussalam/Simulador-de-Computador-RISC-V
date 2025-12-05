using System;
using System.Collections.Generic;
using System.Text;

namespace Simulador_de_Computador_RISC_V
{
    public class Memoria
    {
        // -- Mapeamento memoria --
        private const uint RAM_COMECO = 0x00000;
        private const uint RAM_FIM = 0x7FFFF;

        private const uint VRAM_COMECO = 0x80000;
        private const uint VRAM_FIM = 0x8FFFF;

        public enum TamanhoAcesso { Byte, Half, Word }

        public uint[] Ram;
        public uint[] Vram;
        public uint[] ExpansaoFutura;
        private int enderecoAtual;
        public Barramento barramento;

        public Memoria(byte tipo_teste, Barramento barramento)
        {
            this.barramento = barramento;

            Ram = new uint[131072]; // 131.072 palavras de 32 bits      512KB
            Vram = new uint[16384]; // 16.384 palavras de 32 bits       64KB
            ExpansaoFutura = new uint[256]; // 256 palavras de 32     bits 1KB

            enderecoAtual = 0;

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
                0x02000093, // addi x1, x0, 32
                0x07F00193, // addi x3, x0, 127

                0x000802B7, // LUI x5, 0x00080   → x5 = 0x00080000 (VRAM)

                0x00000013, // NOP (pode deixar vazio)

                0x00128023, // sb   x1, 0(x5)
                0x00108093, // addi x1, x1, 1
                0x00428293, // addi x5, x5, 4
                0xFE3094E3, // bne  x1, x3, -12
                0x0000006F  // jal  x0, 0

            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarInstrucoesHelloWorld()
        {
            // Instruções para imprimir "Hello"
            uint[] instrucoes = {
                // Setup
                0x00000197, // auipc x3, 0          ; x3 = 0x0
                0x02418193, // addi  x3, x3, 36     ; x3 = 0x24 (endereço dos dados)
                /* 0x08 */ 0x000802B7, // lui   x5, 0x80       ; x5 = 0x80000 (ponteiro de escrita VRAM)
        
                // Loop
                0x0001A303, // lb    x6, 0(x3)      ; x6 = Mem[x3] (caractere)
                0x00030663, // beq   x6, x0, 12      ; Se x6 == 0, pula para o fim
                0x00628023, // sb    x6, 0(x5)      ; VRAM[x5] = x6
                0x00118193, // addi  x3, x3, 1       ; Avança ponteiro da string
                0x00128293, // addi  x5, x5, 1       ; Avança ponteiro da VRAM
                0xFE0FF0E3, // jal   x0, -20         ; Volta para o 'lb'
        
                // Fim
                0x0000006F, // jal x0, 0

                // Dados
                0x6C6C6548, // "lleH"
                0x00216F6F  // "\0!o"
            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarTesteOperacoesRV32I()
        {
                    // Instruções para teste de todas as operações RV32I
                    uint[] instrucoes = {
                // ==========================================================
                // Bloco 1: Setup e Teste de Imediatos (ADDI, XORI, ORI, ANDI)
                // ==========================================================
                0x00500293, // addi x5, x0, 5          ; x5 = 5
                0xFFB00313, // addi x6, x0, -5         ; x6 = -5 (0xFFFFFFFB)
                0x0F02C393, // xori x7, x5, 240        ; x7 = 5 ^ 240 = 245 (0xF5)
                0x0F02E413, // ori  x8, x5, 240        ; x8 = 5 | 240 = 245 (0xF5)
                0x0F02F493, // andi x9, x5, 240        ; x9 = 5 & 240 = 0   (0x00)

                // ==========================================================
                // Bloco 2: Teste de LUI e AUIPC
                // ==========================================================
                0x000F0537, // lui   x10, 0xF0         ; x10 = 0xF0000
                0x00000597, // auipc x11, 0            ; x11 = PC atual = 0x18
        
                // ==========================================================
                // Bloco 3: Teste de Armazenamento (SW, SH, SB)
                // Vamos armazenar os resultados anteriores na memória.
                // ==========================================================
                0x00602023, // sw  x6, 0(x0)           ; Mem[0] = x6 (-5)
                0x00702223, // sw  x7, 4(x0)           ; Mem[4] = x7 (245)
                0x00802423, // sw  x8, 8(x0)           ; Mem[8] = x8 (245)
                0x00902623, // sw  x9, 12(x0)          ; Mem[12] = x9 (0)
                0x00A02823, // sw  x10, 16(x0)         ; Mem[16] = x10 (0xF0000)
                0x00B02A23, // sw  x11, 20(x0)         ; Mem[20] = x11 (0x18)

                // Teste de SH e SB
                0x00501023, // sh x5, 24(x0)           ; Mem[24] = 0x0005 (halfword de x5)
                0x00600223, // sb x6, 28(x0)           ; Mem[28] = 0xFB (byte de x6)
        
                // ==========================================================
                // Bloco 4: Teste de Carga (LBU)
                // ==========================================================
                0x01C04603, // lbu x12, 28(x0)         ; x12 = Mem[28] = 0xFB (251)
        
                // ==========================================================
                // Bloco 5: Teste de Shifts (SLLI, SRLI, SRAI)
                // ==========================================================
                0x002C1693, // slli x13, x24, 2        ; x24 (t4) tem 0, então x13=0
                                                                // (Vamos assumir que você colocou valores antes de testar)
                                                                // Vamos usar x6 que tem -5 (0xFFFFFFFB)
                0x00231693, // slli x13, x6, 2         ; x13 = -5 << 2 = -20 (0xFFFFFFEC)
                0x00235713, // srli x14, x6, 2         ; x14 = -5 >>l 2 = 0x3FFFFFFE
                0x40235793, // srai x15, x6, 2         ; x15 = -5 >>a 2 = -2 (0xFFFFFFFE)

                // ==========================================================
                // Bloco 6: Teste de Operações R (ADD, SUB, SLT, SLTU)
                // ==========================================================
                // Vamos somar x5 (5) e x6 (-5). Resultado deve ser 0.
                0x00628833, // add x16, x5, x6         ; x16 = 5 + (-5) = 0
                // Vamos subtrair x6 (-5) de x5 (5). Resultado deve ser 10.
                0x406288B3, // sub x17, x5, x6         ; x17 = 5 - (-5) = 10
                // Comparar x6 (-5) < x5 (5)? (signed) Deve ser 1.
                0x0062A933, // slt x18, x6, x5         ; x18 = (-5 < 5) ? 1 : 0 => 1
                // Comparar x6 (-5) < x5 (5)? (unsigned) Deve ser 0.
                0x0062B9B3, // sltu x19, x6, x5        ; x19 = (big_num < 5) ? 1 : 0 => 0
        
                // ==========================================================
                // Bloco 7: Teste de Branches
                // ==========================================================
                // x16 (t1) é 0. beq x16, x0, 8 -> deve pular
                0x00080463, // beq x16, x0, 8          ; Pula para 0x68
                0xDEADBEEF, // addi x0, x0, 0          ; Instrução que deve ser pulada (NOP)
                // x17 (t2) é 10. bne x17, x0, 8 -> deve pular
                0x00089463, // bne x17, x0, 8          ; Pula para 0x70
                0xDEADBEEF, // addi x0, x0, 0          ; Instrução que deve ser pulada (NOP)

                // ==========================================================
                // Bloco 8: Teste de JAL
                // ==========================================================
                0x0080006F, // jal x1, 8               ; Pula para 0x78. x1 = 0x74
                0xDEADBEEF, // addi x0, x0, 0          ; Instrução que deve ser pulada (NOP)
        
                // Fim
                0x0000006F  // jal x0, 0               ; Loop infinito para parar
            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarArrayInstrucoes(uint[] instrucoes)
        {
            foreach (uint instrucao in instrucoes)
            {
                if (enderecoAtual < Ram.Length)
                {
                    Ram[enderecoAtual] = instrucao;
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
            if (enderecoAtual < Ram.Length)
            {
                Ram[enderecoAtual] = instrucao;
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
            if (endereco >= 0 && endereco < Ram.Length)
            {
                return Ram[endereco];
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
            for (int i = 0; i < Ram.Length; i++)
            {
                Ram[i] = 0;
            }
            enderecoAtual = 0;
        }

        public void Escrever(uint endereco, uint dado, TamanhoAcesso tamanho)
        {
            switch (tamanho)
            {
                case TamanhoAcesso.Word:
                    EscreverWord(endereco, dado);
                    break;

                case TamanhoAcesso.Half:
                    // Extrai os 16 bits inferiores do dado para passar para o método auxiliar
                    EscreverHalfword(endereco, (ushort)(dado & 0xFFFF));
                    break;

                case TamanhoAcesso.Byte:
                    // Extrai os 8 bits inferiores do dado para passar para o método auxiliar
                    EscreverByte(endereco, (byte)(dado & 0xFF));
                    break;
            }
        }

        // --- MÉTODOS AUXILIARES PRIVADOS ---

        private void EscreverWord(uint endereco, uint dado)
        {
            if (endereco % 4 != 0)
                throw new Exception($"Falha de alinhamento: Escrita de Word em 0x{endereco:X8}");

            if (endereco >= RAM_COMECO && endereco <= RAM_FIM)
            {
                Ram[(endereco - RAM_COMECO) / 4] = dado;
            }
            else if (endereco >= VRAM_COMECO && endereco <= VRAM_FIM)
            {
                uint indice = (endereco - VRAM_COMECO) / 4;

                Vram[indice] = dado;

                AtualizarVRAM_Interativo((char)(dado & 0xFF));
            }
            else
                throw new Exception($"Falha de Escrita: Endereço 0x{endereco:X8} fora do mapa.");
        }


        private void EscreverHalfword(uint endereco, ushort dado)
        {
            if (endereco >= RAM_COMECO && endereco <= RAM_FIM)
            {
                // Escrita normal na RAM
                uint indice_palavra = (endereco - RAM_COMECO) / 4;
                uint offset_bytes = (endereco - RAM_COMECO) % 4;

                uint palavra_atual = Ram[indice_palavra];
                uint mascara = ~(0xFFFFu << (int)(offset_bytes * 8));
                uint dado_posicionado = (uint)dado << (int)(offset_bytes * 8);

                Ram[indice_palavra] = (palavra_atual & mascara) | dado_posicionado;
            }
            else if (endereco >= VRAM_COMECO && endereco <= VRAM_FIM)
            {
                // ---- CORREÇÃO AQUI ----
                if ((endereco - VRAM_COMECO) % 4 != 0)
                    throw new Exception("VRAM só aceita escrita word-addressed.");

                uint indice = (endereco - VRAM_COMECO) / 4;

                // Salva o halfword como palavra inteira
                Vram[indice] = (uint)dado;

                AtualizarVRAM_Interativo((char)(dado & 0xFF));
            }
            else
                throw new Exception($"Falha de Escrita: Endereço 0x{endereco:X8} fora do mapa.");
        }


        private void EscreverByte(uint endereco, byte dado)
        {
            if (endereco >= RAM_COMECO && endereco <= RAM_FIM)
            {
                // Escrita normal na RAM (byte-addressed)
                uint indice_palavra = (endereco - RAM_COMECO) / 4;
                uint byte_offset = (endereco - RAM_COMECO) % 4;

                uint palavra_atual = Ram[indice_palavra];
                uint mascara = ~(0xFFu << (int)(byte_offset * 8));
                uint dado_posicionado = (uint)dado << (int)(byte_offset * 8);

                Ram[indice_palavra] = (palavra_atual & mascara) | dado_posicionado;
            }
            else if (endereco >= VRAM_COMECO && endereco <= VRAM_FIM)
            {
                // ---- CORREÇÃO AQUI ----
                if ((endereco - VRAM_COMECO) % 4 != 0)
                    throw new Exception("VRAM só aceita endereços múltiplos de 4 (word-addressed).");

                uint indice = (endereco - VRAM_COMECO) / 4;

                // Salva o dado inteiro no índice (ascii em 32 bits)
                Vram[indice] = (uint)dado;

                AtualizarVRAM_Interativo((char)dado);
            }
            else
            {
                throw new Exception($"Falha de Escrita de Byte: Endereço 0x{endereco:X8} não mapeado.");
            }
        }

        private void AtualizarVRAM_Interativo(char caractere)
        {

            for(int i = 0; i < Vram.Length; i++)
            {
                if (Vram[i] != 0)       
                {

                    Console.WriteLine($"Vram: 0x{Vram[i]:X8} ('{(char)(Vram[i] & 0xFF)}')");
                }
            }
            // Ação 1: Mostrar apenas o único caractere sendo adicionado.
            // Isso dá o feedback imediato da operação 'sb'.
            //Console.Write(caractere);

            // Ação 2: Esperar que o usuário pressione Enter para continuar a simulação.
            Console.WriteLine("  <-- Pressione Enter para o próximo passo...");
            Console.ReadLine();
        }
    }
}