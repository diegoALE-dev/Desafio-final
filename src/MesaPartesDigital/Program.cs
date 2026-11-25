using System;

public static class Program
{
    public static void Main(string[] args)
    {
        ArchivoExpedientes.CargarDesdeArchivo();

        while (true)
        {
            Console.WriteLine("\n===========================================================");
            Console.WriteLine("  MESA DE PARTES DIGITAL - MUNICIPALIDAD DE LOS OLIVOS");
            Console.WriteLine("===========================================================");
            Console.WriteLine("1. Registrar expediente");
            Console.WriteLine("2. Buscar y consultar expediente");
            Console.WriteLine("3. Actualizar estado del trámite");
            Console.WriteLine("4. Listar y ordenar expedientes");
            Console.WriteLine("5. Estadísticas de expedientes");
            Console.WriteLine("0. Salir");
            Console.Write("Seleccione una opción: ");

            switch (Console.ReadLine()?.Trim())
            {
                case "1":
                    RegistroExpedientes.RegistrarExpediente();
                    break;
                case "2":
                    BusquedaExpedientes.BuscarExpediente();
                    break;
                case "3":
                    ActualizarExpedientePorCodigo();
                    break;
                case "4":
                    OrdenamientoExpedientes.OrdenarExpedientes();
                    break;
                case "5":
                    EstadisticasExpedientes.MostrarEstadisticas();
                    break;
                case "0":
                case null:
                    Console.WriteLine("\nCerrando el sistema de la Municipalidad de Los Olivos...");
                    return;
                default:
                    Console.WriteLine("[!] Elige una opción del 0 al 5.");
                    break;
            }
        }
    }

    private static void ActualizarExpedientePorCodigo()
    {
        Console.Write("Código de expediente: ");
        string codigo = Console.ReadLine();
        if (codigo == null) return;

        int indice = BusquedaExpedientes.BuscarIndicePorCodigo(codigo);
        if (indice == -1)
        {
            Console.WriteLine("[!] Expediente no encontrado.");
            return;
        }

        EstadosExpedientes.ActualizarEstado(indice);
    }
}
