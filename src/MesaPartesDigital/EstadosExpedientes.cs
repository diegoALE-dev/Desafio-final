
using System;
using static DatosExpedientes;

public static class EstadosExpedientes
{
    public static void ActualizarEstado(int indice)
    {
        // Comprobar que el índice exista.
        if (indice < 0 || indice >= totalExpedientes)
        {
            Console.WriteLine("[!] Expediente no encontrado.");
            return;
        }

        EstadoTramite? estadoAnterior = estados[indice];
        string motivoAnterior = motivosObservacion[indice];

        Console.WriteLine("\n--- ACTUALIZAR ESTADO DEL TRÁMITE ---");
        Console.WriteLine("Código: " + codigos[indice]);

        // Expedientes antiguos sin estado.
        if (estadoAnterior == null)
        {
            Console.WriteLine("Estado actual: Sin información");
            Console.WriteLine("1. Confirmar como Registrado");
            Console.WriteLine("0. Volver");
            Console.Write("Seleccione: ");

            string opcion = Console.ReadLine();

            if (opcion == "0" || opcion == null)
                return;

            if (opcion != "1")
            {
                Console.WriteLine("[!] Opción inválida.");
                return;
            }

            estados[indice] = EstadoTramite.Registrado;
        }
        else
        {
            Console.WriteLine("Estado actual: " + estadoAnterior);

            switch (estadoAnterior.Value)
            {
                case EstadoTramite.Registrado:

                    Console.WriteLine("1. Pasar a En revisión");
                    Console.WriteLine("0. Volver");
                    Console.Write("Seleccione: ");

                    string opcionRegistrado = Console.ReadLine();

                    if (opcionRegistrado == "0" || opcionRegistrado == null)
                        return;

                    if (opcionRegistrado != "1")
                    {
                        Console.WriteLine("[!] Opción inválida.");
                        return;
                    }

                    estados[indice] = EstadoTramite.EnRevision;
                    break;

                case EstadoTramite.EnRevision:

                    Console.WriteLine("1. Pasar a Observado");
                    Console.WriteLine("2. Pasar a Atendido");
                    Console.WriteLine("0. Volver");
                    Console.Write("Seleccione: ");

                    string opcionRevision = Console.ReadLine();

                    if (opcionRevision == "0" || opcionRevision == null)
                        return;

                    switch (opcionRevision)
                    {
                        case "1":

                            Console.Write("Ingrese motivo de observación: ");
                            string motivo = Console.ReadLine();

                            if (motivo == null)
                                return;

                            motivo = motivo.Trim();

                            if (string.IsNullOrWhiteSpace(motivo) ||
                                !EsTextoSeguro(motivo))
                            {
                                Console.WriteLine("[!] Motivo inválido.");
                                return;
                            }

                            motivosObservacion[indice] = motivo;
                            estados[indice] = EstadoTramite.Observado;
                            break;

                        case "2":
                            estados[indice] = EstadoTramite.Atendido;
                            break;

                        default:
                            Console.WriteLine("[!] Opción inválida.");
                            return;
                    }

                    break;

                case EstadoTramite.Observado:

                    Console.WriteLine("Motivo: " + motivosObservacion[indice]);
                    Console.WriteLine("1. Volver a En revisión");
                    Console.WriteLine("0. Volver");
                    Console.Write("Seleccione: ");

                    string opcionObservado = Console.ReadLine();

                    if (opcionObservado == "0" || opcionObservado == null)
                        return;

                    if (opcionObservado != "1")
                    {
                        Console.WriteLine("[!] Opción inválida.");
                        return;
                    }

                    estados[indice] = EstadoTramite.EnRevision;
                    break;

                case EstadoTramite.Atendido:

                    Console.WriteLine("[✓] Este trámite ya fue atendido.");
                    Console.WriteLine("El proceso ha finalizado.");
                    return;
            }
        }

        // Guardar el nuevo estado.
        if (!ArchivoExpedientes.GuardarEnArchivo())
        {
            estados[indice] = estadoAnterior;
            motivosObservacion[indice] = motivoAnterior;

            Console.WriteLine("[!] No se pudo guardar el cambio.");
            Console.WriteLine("[!] Se conservó el estado anterior.");
            return;
        }

        Console.WriteLine("\n[✓] Estado actualizado correctamente.");
        Console.WriteLine("Nuevo estado: " + estados[indice]);
    }
}
