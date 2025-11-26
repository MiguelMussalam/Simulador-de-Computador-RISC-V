using Simulador_de_Computador_RISC_V;
using Simulador_de_Computador_RISC_V.CPU;

// Inicialização de memória: 
// Ao iniciar como 1 = Instruções para escrever caracteres ASCII
// Ao iniciar como 2 = Instruções para escrever Hello
// Ao iniciar como 3 = Instruções para testar todas as operações RV32I

internal class Program
{
    static void Main()
    {
        Memoria memoria = new(3);
        CPU cpu = new();
        cpu.InicializarRandomicamenteRegistradores();

        for (int i = 0; i < 30; i++)
        {
            Console.WriteLine($"PC: {cpu.PC:X8}");
            Decodificador.Executar(cpu, memoria.memoria[cpu.PC / 4]);
            cpu.iterar_pc();
        }
    }
}