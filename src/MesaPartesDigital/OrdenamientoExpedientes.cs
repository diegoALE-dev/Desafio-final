
using System;
using static DatosExpedientes;

public static class OrdenamientoExpedientes
{
    public static void OrdenarExpedientes()
    {
        if (totalExpedientes < 2)
        {
            Console.WriteLine("\n[!] Se requieren al menos 2 expedientes para ordenar.");
            return;
        }

        // Ordenamos las posiciones para que todos los datos de la ficha sigan juntos.
        int[] indices = new int[totalExpedientes];
        for (int i = 0; i < totalExpedientes; i++)
        {
            indices[i] = i;
        }

        for (int i = 0; i < totalExpedientes - 1; i++)
        {
            for (int j = i + 1; j < totalExpedientes; j++)
            {
                if (string.Compare(codigos[indices[i]], codigos[indices[j]],
                    StringComparison.OrdinalIgnoreCase) > 0)
                {
                    int auxiliar = indices[i];
                    indices[i] = indices[j];
                    indices[j] = auxiliar;
                }
            }
        }

        Console.WriteLine("\n[✓] ¡Expedientes ordenados correctamente por Código!");

        for (int i = 0; i < indices.Length; i++)
        {
            int indice = indices[i];
            Console.WriteLine(
                $"Código: {codigos[indice]} | DNI: {dni[indice]} | " +
                $"Nombre: {nombres[indice]} | Correo: {correos[indice]} | " +
                $"Asunto: {asuntos[indice]}");
        }
    }
}
