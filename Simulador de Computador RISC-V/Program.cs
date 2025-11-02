using Simulador_de_Computador_RISC_V;
internal class Program
{
    static void Main()
    {
        // memoria(true) -> Print de todos caracteres ASCII
        // memoria(false) -> print de "hello"
        Memoria memoria = new(true);
        CPU cpu = new();

        for(int i = 0; i < 10; i++)
        {
            Console.WriteLine($"Espaço da memoria {cpu.PC}");
            cpu.IniciarInstrucoes(memoria);
        }
    }
}