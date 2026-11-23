using System;

public static class Consola
{
    public static void EscribirResultado(string mensaje)
    {
        if (Console.IsOutputRedirected)
        {
            Console.WriteLine(mensaje);
            return;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        try
        {
            Console.WriteLine(mensaje);
        }
        finally
        {
            Console.ResetColor();
        }
    }
}
