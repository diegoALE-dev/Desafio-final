
using System;
using static DatosExpedientes;

public static class Program
{
    public static void Main(string[] args)
    {
        ArchivoExpedientes.CargarDesdeArchivo();

        int opcDoc = 0;

        do
        {
            Console.WriteLine("\n===========================================================");
            Console.WriteLine("  MESA DE PARTES DIGITAL - MUNICIPALIDAD DE LOS OLIVOS");
            Console.WriteLine("===========================================================");
            Console.WriteLine("1. Registrar Expediente");
            Console.WriteLine("2. Buscar Expediente");
            Console.WriteLine("3. Ordenar Expedientes por Código");
            Console.WriteLine("4. Actualizar Estado del Expediente");
            Console.WriteLine("5. Visualizar Estados de Expedientes");
            Console.WriteLine("6. Salir");
            Console.Write("Seleccione una opción: ");

            string entrada = Console.ReadLine();

            if (entrada == null)
                break;

            if (!int.TryParse(entrada, out opcDoc))
            {
                Console.WriteLine("[!] Ingrese un número válido.");
                continue;
            }

            switch (opcDoc)
            {
                case 1:
                    RegistroExpedientes.RegistrarExpediente();
                    break;

                case 2:
                    BusquedaExpedientes.BuscarExpediente();
                    break;

                case 3:
                    OrdenamientoExpedientes.OrdenarExpedientes();
                    break;

                case 4:
                    if (totalExpedientes == 0)
                    {
                        Console.WriteLine("[!] No hay expedientes registrados.");
                        break;
                    }

                    Console.WriteLine("\n--- ACTUALIZAR ESTADO ---");

                    for (int i = 0; i < totalExpedientes; i++)
                    {
                        Console.WriteLine(
                            $"{i + 1}. Código: {codigos[i]} | Estado: {estados[i]?.ToString() ?? "Sin información"}");
                    }

                    Console.Write("\nSeleccione el expediente: ");

                    if (int.TryParse(Console.ReadLine(), out int numero) &&
                        numero >= 1 && numero <= totalExpedientes)
                    {
                        EstadosExpedientes.ActualizarEstado(numero - 1);
                    }
                    else
                    {
                        Console.WriteLine("[!] Selección inválida.");
                    }
                    break;

                case 5:
                    Console.WriteLine("\n--- ESTADOS DE EXPEDIENTES ---");

                    if (totalExpedientes == 0)
                    {
                        Console.WriteLine("[!] No hay expedientes registrados.");
                        break;
                    }

                    for (int i = 0; i < totalExpedientes; i++)
                    {
                        Console.WriteLine("\n--------------------------------");
                        Console.WriteLine("Código: " + codigos[i]);
                        Console.WriteLine("Solicitante: " + nombres[i]);
                        Console.WriteLine("Estado: " +
                            (estados[i]?.ToString() ?? "Sin información"));

                        if (estados[i] == EstadoTramite.Observado)
                        {
                            Console.WriteLine("Motivo: " + motivosObservacion[i]);
                        }
                    }

                    Console.WriteLine("--------------------------------");
                    break;

                case 6:
                    Console.WriteLine("\nCerrando el sistema de la Municipalidad de Los Olivos...");
                    break;

                default:
                    Console.WriteLine("[!] Opción inválida.");
                    break;
            }

        } while (opcDoc != 6);
    }
}
