using System;
using static DatosExpedientes;

public static class OrdenamientoExpedientes
{
    public static void OrdenarExpedientes()
    {
        if (totalExpedientes == 0)
        {
            Console.WriteLine("[!] No hay expedientes registrados.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n--- LISTAR Y ORDENAR EXPEDIENTES ---");
            Console.WriteLine("1. Código ascendente");
            Console.WriteLine("2. Fecha más antigua primero");
            Console.WriteLine("3. Fecha más reciente primero");
            Console.WriteLine("0. Volver");
            Console.Write("Elige una opción: ");

            string entrada = Console.ReadLine()?.Trim();
            if (entrada == null || entrada == "0") return;

            if (!int.TryParse(entrada, out int criterio) || criterio < 1 || criterio > 3)
            {
                Console.WriteLine("Elige una opción del 0 al 3.");
                continue;
            }

            int[] indices = ObtenerIndicesOrdenados(criterio);
            for (int i = 0; i < indices.Length; i++)
            {
                BusquedaExpedientes.MostrarResumen(indices[i]);
            }
        }
    }

    public static int[] ObtenerIndicesOrdenados(int criterio)
    {
        int[] indices = new int[totalExpedientes];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = i;
        }

        for (int i = 0; i < indices.Length - 1; i++)
        {
            for (int j = i + 1; j < indices.Length; j++)
            {
                if (CompararPosiciones(indices[i], indices[j], criterio) > 0)
                {
                    int auxiliar = indices[i];
                    indices[i] = indices[j];
                    indices[j] = auxiliar;
                }
            }
        }

        return indices;
    }

    private static int CompararPosiciones(int primero, int segundo, int criterio)
    {
        if (criterio == 2 || criterio == 3)
        {
            DateTime? fechaPrimera = fechasRegistro[primero];
            DateTime? fechaSegunda = fechasRegistro[segundo];

            // Las fichas sin fecha se colocan al final en ambos listados.
            if (!fechaPrimera.HasValue && fechaSegunda.HasValue) return 1;
            if (fechaPrimera.HasValue && !fechaSegunda.HasValue) return -1;

            if (fechaPrimera.HasValue && fechaSegunda.HasValue)
            {
                int comparacion = DateTime.Compare(fechaPrimera.Value, fechaSegunda.Value);
                if (comparacion != 0)
                {
                    return criterio == 2 ? comparacion : -comparacion;
                }
            }
        }

        return string.Compare(codigos[primero], codigos[segundo], StringComparison.OrdinalIgnoreCase);
    }
}