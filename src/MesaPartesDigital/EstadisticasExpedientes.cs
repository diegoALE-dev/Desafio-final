using System;
using static DatosExpedientes;

public static class EstadisticasExpedientes
{
    public static void MostrarEstadisticas()
    {
        int solicitudesMultiples = 0;
        int deduccionesPrediales = 0;
        int otrosTipos = 0;

        int registrados = 0;
        int enRevision = 0;
        int observados = 0;
        int atendidos = 0;
        int sinEstado = 0;

        for (int i = 0; i < totalExpedientes; i++)
        {
            switch (tiposTramite[i])
            {
                case "Solicitud múltiple":
                    solicitudesMultiples++;
                    break;

                case "Deducción del impuesto predial":
                    deduccionesPrediales++;
                    break;

                default:
                    otrosTipos++;
                    break;
            }

            switch (estados[i])
            {
                case EstadoTramite.Registrado:
                    registrados++;
                    break;

                case EstadoTramite.EnRevision:
                    enRevision++;
                    break;

                case EstadoTramite.Observado:
                    observados++;
                    break;

                case EstadoTramite.Atendido:
                    atendidos++;
                    break;

                default:
                    sinEstado++;
                    break;
            }
        }

        Console.WriteLine("\n=====================================");
        Console.WriteLine("     ESTADÍSTICAS DE EXPEDIENTES");
        Console.WriteLine("=====================================");

        Consola.EscribirResultado(
            $"Total de expedientes: {totalExpedientes}");

        Console.WriteLine("\n--- POR TIPO DE TRÁMITE ---");

        Consola.EscribirResultado(
            $"Solicitud múltiple: {solicitudesMultiples}");

        Consola.EscribirResultado(
            $"Deducción del impuesto predial: {deduccionesPrediales}");

        if (otrosTipos > 0)
        {
            Consola.EscribirResultado(
                $"Otros tipos: {otrosTipos}");
        }

        Console.WriteLine("\n--- POR ESTADO DEL TRÁMITE ---");

        Consola.EscribirResultado($"Registrados: {registrados}");
        Consola.EscribirResultado($"En revisión: {enRevision}");
        Consola.EscribirResultado($"Observados: {observados}");
        Consola.EscribirResultado($"Atendidos: {atendidos}");

        if (sinEstado > 0)
        {
            Consola.EscribirResultado(
                $"Sin estado definido: {sinEstado}");
        }

        Console.WriteLine("\n--- CAPACIDAD DEL SISTEMA ---");

        Consola.EscribirResultado(
            $"Capacidad máxima: {CapacidadMaxima}");

        Consola.EscribirResultado(
            $"Espacios disponibles: {CapacidadMaxima - totalExpedientes}");

        Console.WriteLine("=====================================");
    }
}