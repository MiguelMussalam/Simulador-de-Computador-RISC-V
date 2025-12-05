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
        Barramento barramento = new();
        CPU cpu = new(barramento);


        Console.WriteLine("Escolha uma opção de inicialização de memória:");
        Console.WriteLine("1 - Instruções para escrever caracteres ASCII");
        Console.WriteLine("2 - Instruções para escrever Hello");
        Console.WriteLine("3 - Instruções para testar todas as operações RV32I");
        string escolhaUsuario = Console.ReadLine();
        Console.WriteLine(escolhaUsuario);

        if (escolhaUsuario == "1")
        {
            Memoria Memoria = new(1, barramento);
            barramento.ReferenciarModulos(cpu, Memoria);
        }
        else if (escolhaUsuario == "2")
        {
            Memoria Memoria = new(2, barramento);
            barramento.ReferenciarModulos(cpu, Memoria);
        }
        else if (escolhaUsuario == "3")
        {
            Memoria Memoria = new(3, barramento);
            barramento.ReferenciarModulos(cpu, Memoria);
        }
        else
        {
            Console.WriteLine("Opção inválida. Inicializando com instruções para escrever caracteres ASCII por padrão.");
            Memoria Memoria = new(1, barramento);
            barramento.ReferenciarModulos(cpu, Memoria);
        }


        while (true)
        {
            Console.WriteLine($"PC: {cpu.PC:X8}");
            Decodificador.Executar(cpu, barramento.LerDadoMemoria(cpu.PC));
        }
    }
}