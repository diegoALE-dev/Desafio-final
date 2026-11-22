using System;
using static DatosExpedientes;

public static class RegistroExpedientes
{
    private static string dnis = "";
    private static string nombres = "";
    private static string apellidos = "";
    private static string correos = "";
    private static string asuntos = "";

    public static void RegistrarExpediente()
    {
        if (totalExpedientes >= CapacidadMaxima)
        {
            Console.WriteLine("[!] Capacidad máxima alcanzada.");
            return;
        }

        Console.WriteLine("--- Registro de Nuevo Expediente ---");

        Console.Write("Ingrese DNI (8 dígitos): ");
        string dni = Console.ReadLine() ?? "";

        if (dni.Length != 8 || !System.Text.RegularExpressions.Regex.IsMatch(dni, @"^\d{8}$"))
        {
            Console.WriteLine("Error: El DNI debe tener exactamente 8 dígitos numéricos.");
            return;
        }

        Console.Write("Ingrese Nombres: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("Ingrese Apellidos: ");
        string apellido = Console.ReadLine() ?? "";

        Console.Write("Ingrese Correo Electrónico: ");
        string correo = Console.ReadLine() ?? "";

        Console.Write("Ingrese Asunto del trámite: ");
        string asunto = Console.ReadLine() ?? "";

        // Mantenemos las asignaciones originales de tu compañero
        dnis = dni;
        nombres = nombre;
        apellidos = apellido;
        correos = correo;
        asuntos = asunto;

        int indice = totalExpedientes;
        DateTime fecha = DateTime.Today;
        codigos[indice] = GenerarCodigoExpediente(fecha);
        DatosExpedientes.dni[indice] = dni;
        DatosExpedientes.nombres[indice] = nombre + " " + apellido;
        DatosExpedientes.correos[indice] = correo;
        DatosExpedientes.asuntos[indice] = asunto;
        fechasRegistro[indice] = fecha;
        estados[indice] = EstadoTramite.Registrado;

        totalExpedientes++;
        if (!ArchivoExpedientes.GuardarEnArchivo())
        {
            totalExpedientes--;
            return;
        }

        Console.WriteLine($"Expediente registrado exitosamente. Código: {codigos[indice]}");
        Console.WriteLine($"Total de expedientes registrados: {totalExpedientes}");
    }
}
