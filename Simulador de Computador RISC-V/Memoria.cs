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

        private const uint ENTRADA_SAIDA_COMECO = 0x9FC00;
        private const uint ENTRADA_SAIDA_FIM = 0x9FFFF;
        private const uint CONSOLE_ADDR = 0x9FC00;

        public enum TamanhoAcesso { Byte, Half, Word }

        public uint[] Ram;
        public uint[] Vram;
        public uint[] EntradaSaidaProg;
        private int enderecoAtual;
        public Barramento barramento;

        public Memoria(byte tipo_teste, Barramento barramento)
        {
            this.barramento = barramento;

            Ram = new uint[131072]; // 131.072 palavras de 32 bits      512KB
            Vram = new uint[16384]; // 16.384 palavras de 32 bits       64KB
            EntradaSaidaProg = new uint[256]; // 256 palavras de 32     bits 1KB

            enderecoAtual = 0;

            //Console.WriteLine("Memoria");
            // Zera toda a memória
            //for (int i = 0; i < Ram.Length; i++)
            //{
            //    Ram[i] = 0;
            //    Console.WriteLine($"[{i}]: {Ram[i]}");
            //    //if (i % 2 == 0 && i != 0) Console.WriteLine($"Memória[{i - 1}] inicializada com valor: {Ram[i - 1]} ; Memória[{i}] inicializada com valor: {Ram[i]}");
            //}

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
        // --- Setup ---
        /* 0x00 */ 0x00000093, // addi x1, x0, 0          ; x1 = 0 (será nosso caractere a ser impresso)
        /* 0x04 */ 0x08000193, // addi x3, x0, 128        ; x3 = 128 (limite do loop)

        // --- A CORREÇÃO PRINCIPAL ESTÁ AQUI ---
        // Antes: lui x4, 0xC0000 (apontava para um endereço inválido)
        // Agora: apontamos para o endereço do console
        /* 0x08 */ 0x0009F237, // lui  x4, 0x9F          ; x4 = 0x9F000 (endereço base para a área de E/S)
        
        // --- Loop (começa no PC=0x0C) ---
        // A instrução 'sb' agora usa um offset para chegar ao endereço 0x9FC00
        /* 0x0C */ 0x00120E23, // sb   x1, 192(x4)       ; Mem[0x9F000 + 192 (0xC0)] = x1. Escreve no CONSOLE_ADDR.
        /* 0x10 */ 0x00108093, // addi x1, x1, 1          ; x1++ (próximo caractere)
        /* 0x14 */ 0xFE309CE3, // bne  x1, x3, -8        ; Se x1 != 128, pule 2 instruções para trás (para a instrução 'sb')
        
        // --- Fim ---
        /* 0x18 */ 0x0000006F  // jal x0, 0 (loop infinito para terminar o programa)
            };

            AdicionarArrayInstrucoes(instrucoes);
        }

        private void AdicionarInstrucoesHelloWorld()
        {
            // Instruções para imprimir "Hello"
            uint[] instrucoes = {
                // --- Setup ---
                /* 0x00 */ 0x0001C1B7, // lui   a3, 0x1C        ; a3 = 0x1C000 (endereço aproximado dos dados)
                /* 0x04 */ 0xFE818193, // addi  a3, a3, -24     ; a3 = 0x1C000 - 24 = 0x1BFE8. Ajuste para apontar para "Hello!" (explicação abaixo)
                /* 0x08 */ 0x0009F2B7, // lui   a5, 0x9F        ; a5 = 0x9F000 (endereço base do console)
                /* 0x0C */ 0xC0028293, // addi  a5, a5, 192     ; a5 = 0x9F000 + 192 = 0x9FC00 (endereço exato do console)
        
                // --- Loop (começa no PC=0x10) ---
                /* 0x10 */ 0x0006C603, // lbu   a2, 0(a3)       ; Carrega um byte do endereço da string em a2
                /* 0x14 */ 0x00000C63, // beqz  a2, 12          ; Se o byte for 0 (fim da string), pula para o fim do programa (+12 bytes)
                /* 0x18 */ 0x00C28023, // sb    a2, 0(a5)       ; Escreve o byte (caractere) no console
                /* 0x1C */ 0x00168693, // addi  a3, a3, 1       ; Avança o ponteiro da string
                /* 0x20 */ 0xFF1FF06F, // jal   zero, -16       ; Pula de volta para o início do loop (para o 'lbu')

                // --- Dados (a string em si) ---
                // O assembler normalmente alinha isso, mas vamos colocar manualmente aqui.
                // O endereço final de 'jal' é 0x20. As instruções ocupam até 0x24.
                // O PC para o dado seria 0x28, 0x2C, etc.
                // Nosso ponteiro 'a3' foi ajustado para apontar para cá (endereço 0x28).
                /* 0x24 */ 0x6C6C6548, // "lleH" (Hello!)
                /* 0x28 */ 0x000A216F, // "o!\n\0" (invertido, com nova linha e terminador nulo)
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
                Vram[(endereco - VRAM_COMECO) / 4] = dado;
            }
            else if (endereco == CONSOLE_ADDR)
            {
                Console.Write((char)(dado & 0xFF)); // Escrever uma word no console imprime o primeiro byte
            }
            else
                throw new Exception($"Falha de Escrita: Endereço 0x{endereco:X8} fora do mapa.");
        }

        private void EscreverHalfword(uint endereco, ushort dado)
        {
            if (endereco % 2 != 0)
                throw new Exception($"Falha de alinhamento: Escrita de Halfword em 0x{endereco:X8}");

            if (endereco >= RAM_COMECO && endereco <= RAM_FIM)
            {
                uint indice_palavra = (endereco - RAM_COMECO) / 4;
                uint offset_bytes = (endereco - RAM_COMECO) % 4; // Será 0 ou 2

                uint palavra_atual = Ram[indice_palavra];
                uint mascara = ~(0xFFFFu << (int)(offset_bytes * 8));
                uint dado_posicionado = (uint)dado << (int)(offset_bytes * 8);

                Ram[indice_palavra] = (palavra_atual & mascara) | dado_posicionado;
            }
            else if (endereco >= VRAM_COMECO && endereco <= VRAM_FIM)
            {
                uint indice_palavra = (endereco - VRAM_COMECO) / 4;
                uint offset_bytes = (endereco - VRAM_COMECO) % 4;

                uint palavra_atual = Vram[indice_palavra];
                uint mascara = ~(0xFFFFu << (int)(offset_bytes * 8));
                uint dado_posicionado = (uint)dado << (int)(offset_bytes * 8);

                Vram[indice_palavra] = (palavra_atual & mascara) | dado_posicionado;
            }
            else if (endereco == CONSOLE_ADDR)
            {
                Console.Write((char)(dado & 0xFF)); // Trata como escrita de byte
            }
            else
                throw new Exception($"Falha de Escrita: Endereço 0x{endereco:X8} fora do mapa.");
        }

        private void EscreverByte(uint endereco, byte dado)
        {
            if (endereco >= RAM_COMECO && endereco <= RAM_FIM)
            {
                uint indice_palavra = (endereco - RAM_COMECO) / 4;
                uint offset_bytes = (endereco - RAM_COMECO) % 4;

                uint palavra_atual = Ram[indice_palavra];
                uint mascara = ~(0xFFu << (int)(offset_bytes * 8));
                uint dado_posicionado = (uint)dado << (int)(offset_bytes * 8);

                Ram[indice_palavra] = (palavra_atual & mascara) | dado_posicionado;
            }
            else if (endereco >= VRAM_COMECO && endereco <= VRAM_FIM)
            {
                uint indice_palavra = (endereco - VRAM_COMECO) / 4;
                uint offset_bytes = (endereco - VRAM_COMECO) % 4;

                uint palavra_atual = Vram[indice_palavra];
                uint mascara = ~(0xFFu << (int)(offset_bytes * 8));
                uint dado_posicionado = (uint)dado << (int)(offset_bytes * 8);

                Vram[indice_palavra] = (palavra_atual & mascara) | dado_posicionado;
            }
            else if (endereco == CONSOLE_ADDR)
            {
                Console.Write((char)dado);
            }
            else
                throw new Exception($"Falha de Escrita: Endereço 0x{endereco:X8} fora do mapa.");
        }
    }
}