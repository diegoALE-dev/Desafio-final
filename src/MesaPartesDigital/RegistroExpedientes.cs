
using System;
using System.Text.RegularExpressions;
using static DatosExpedientes;

public static class RegistroExpedientes
{
    public static void RegistrarExpediente()
    {
        // Comprobar capacidad antes de solicitar información.
        if (totalExpedientes >= CapacidadMaxima)
        {
            Console.WriteLine("[!] Capacidad máxima alcanzada.");
            return;
        }

        Console.WriteLine("\n--- REGISTRO DE NUEVO EXPEDIENTE ---");

        // Seleccionar tipo de trámite.
        string tipo = SeleccionarTipoTramite();

        if (tipo == null)
            return;

        // Solicitar y validar los datos.
        string numeroDni = LeerDni();
        if (numeroDni == null) return;

        string nombre = LeerTextoObligatorio("Ingrese nombres: ");
        if (nombre == null) return;

        string apellido = LeerTextoObligatorio("Ingrese apellidos: ");
        if (apellido == null) return;

        string correo = LeerCorreo();
        if (correo == null) return;

        string telefono = LeerTelefono();
        if (telefono == null) return;

        string direccion = LeerTextoObligatorio("Ingrese dirección: ");
        if (direccion == null) return;

        int? cantidadFolios = LeerFolios();
        if (cantidadFolios == null) return;

        Console.Write("Ingrese asunto (opcional): ");
        string asunto = Console.ReadLine() ?? "";

        if (!EsTextoSeguro(asunto))
        {
            Console.WriteLine("[!] El asunto contiene caracteres no permitidos.");
            return;
        }

        // Preparar el expediente.
        int indice = totalExpedientes;
        DateTime fecha = DateTime.Today;

        codigos[indice] = GenerarCodigoExpediente(fecha);
        dni[indice] = numeroDni;
        nombres[indice] = nombre + " " + apellido;
        correos[indice] = correo;
        telefonos[indice] = telefono;
        direcciones[indice] = direccion;
        folios[indice] = cantidadFolios;
        tiposTramite[indice] = tipo;
        asuntos[indice] = asunto;
        fechasRegistro[indice] = fecha;
        estados[indice] = EstadoTramite.Registrado;
        motivosObservacion[indice] = "";

        totalExpedientes++;

        // Confirmar solamente si se guardó correctamente.
        if (!ArchivoExpedientes.GuardarEnArchivo())
        {
            totalExpedientes--;

            codigos[indice] = "";
            dni[indice] = "";
            nombres[indice] = "";
            correos[indice] = "";
            telefonos[indice] = "";
            direcciones[indice] = "";
            folios[indice] = null;
            tiposTramite[indice] = "";
            asuntos[indice] = "";
            fechasRegistro[indice] = null;
            estados[indice] = null;
            motivosObservacion[indice] = "";

            Console.WriteLine("[!] No se pudo registrar el expediente.");
            return;
        }

        Console.WriteLine("\n[✓] Expediente registrado correctamente.");
        Console.WriteLine("Código: " + codigos[indice]);
        Console.WriteLine("Tipo de trámite: " + tiposTramite[indice]);
        Console.WriteLine("Estado: Registrado");
    }

    // CATÁLOGO DE TRÁMITES
    private static string SeleccionarTipoTramite()
    {
        while (true)
        {
            Console.WriteLine("\n--- TIPOS DE TRÁMITE ---");
            Console.WriteLine("1. Solicitud múltiple");
            Console.WriteLine("2. Deducción del impuesto predial");
            Console.WriteLine("0. Cancelar");
            Console.Write("Seleccione una opción: ");

            string opcion = Console.ReadLine();

            if (opcion == null)
                return null;

            switch (opcion.Trim())
            {
                case "1":
                    return "Solicitud múltiple";

                case "2":
                    return "Deducción del impuesto predial";

                case "0":
                    Console.WriteLine("Registro cancelado.");
                    return null;

                default:
                    Console.WriteLine("[!] Opción incorrecta. Intente nuevamente.");
                    break;
            }
        }
    }

    // VALIDAR DNI
    private static string LeerDni()
    {
        while (true)
        {
            Console.Write("Ingrese DNI (8 dígitos): ");
            string valor = Console.ReadLine();

            if (valor == null) return null;

            valor = valor.Trim();

            if (Regex.IsMatch(valor, @"^\d{8}$"))
                return valor;

            Console.WriteLine("[!] El DNI debe tener exactamente 8 números.");
        }
    }

    // VALIDAR TEXTOS OBLIGATORIOS
    private static string LeerTextoObligatorio(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string valor = Console.ReadLine();

            if (valor == null) return null;

            valor = valor.Trim();

            if (!string.IsNullOrWhiteSpace(valor) && EsTextoSeguro(valor))
                return valor;

            Console.WriteLine("[!] Ingrese un texto válido.");
        }
    }

    // VALIDAR CORREO
    private static string LeerCorreo()
    {
        while (true)
        {
            Console.Write("Ingrese correo electrónico: ");
            string valor = Console.ReadLine();

            if (valor == null) return null;

            valor = valor.Trim();

            if (Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]+$") &&
                EsTextoSeguro(valor))
                return valor;

            Console.WriteLine("[!] Ingrese un correo válido.");
        }
    }

    // VALIDAR TELÉFONO
    private static string LeerTelefono()
    {
        while (true)
        {
            Console.Write("Ingrese teléfono (7 a 15 dígitos): ");
            string valor = Console.ReadLine();

            if (valor == null) return null;

            valor = valor.Trim();

            if (Regex.IsMatch(valor, @"^\d{7,15}$"))
                return valor;

            Console.WriteLine("[!] Ingrese un teléfono numérico válido.");
        }
    }

    // VALIDAR FOLIOS
    private static int? LeerFolios()
    {
        while (true)
        {
            Console.Write("Ingrese cantidad de folios: ");
            string valor = Console.ReadLine();

            if (valor == null) return null;

            if (int.TryParse(valor, out int cantidad) && cantidad > 0)
                return cantidad;

            Console.WriteLine("[!] Ingrese un número entero mayor que cero.");
        }
    }
}
