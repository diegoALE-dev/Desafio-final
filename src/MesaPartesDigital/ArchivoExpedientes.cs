using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using static DatosExpedientes;

public static class ArchivoExpedientes
{
    private const string NombreArchivo = "expedientes.txt";
    private const string NombreArchivoDemo = "expedientes-demo.txt";
    private const string FormatoFecha = "yyyy-MM-dd";
    private static bool cargaCompleta = true;

    public static bool GuardarEnArchivo()
    {
        if (!cargaCompleta)
        {
            Console.WriteLine("[!] No se guardaron cambios porque la carga fue incompleta.");
            return false;
        }

        // Primero escribimos en la misma carpeta. El original se reemplaza al final.
        string temporal = NombreArchivo + "." + Path.GetRandomFileName() + ".tmp";

        try
        {
            using (StreamWriter escritor = new StreamWriter(temporal))
            {
                for (int i = 0; i < totalExpedientes; i++)
                {
                    string[] campos =
                    {
                        codigos[i], dni[i], nombres[i], correos[i], asuntos[i],
                        telefonos[i], direcciones[i], folios[i]?.ToString(CultureInfo.InvariantCulture),
                        tiposTramite[i], fechasRegistro[i]?.ToString(FormatoFecha, CultureInfo.InvariantCulture),
                        estados[i]?.ToString(), motivosObservacion[i]
                    };

                    foreach (string campo in campos)
                    {
                        if (!EsTextoSeguro(campo))
                        {
                            Console.WriteLine("[!] Los campos no pueden contener | ni saltos de línea.");
                            return false;
                        }
                    }

                    escritor.WriteLine(string.Join("|", campos));
                }
            }

            File.Move(temporal, NombreArchivo, true);
            return true;
        }
        catch (IOException)
        {
            Console.WriteLine("[!] No se pudo escribir el archivo de expedientes.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("[!] No hay permiso para guardar el archivo de expedientes.");
        }
        finally
        {
            if (File.Exists(temporal))
            {
                try
                {
                    File.Delete(temporal);
                }
                catch (IOException)
                {
                    Console.WriteLine("[!] No se pudo eliminar el archivo temporal.");
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("[!] No hay permiso para eliminar el archivo temporal.");
                }
            }
        }

        return false;
    }

    public static void CargarDesdeArchivo()
    {
        totalExpedientes = 0;
        cargaCompleta = true;

        try
        {
            IEnumerable<string> lineas = LeerExpedientesIniciales(out string archivo);
            int numeroLinea = 0;
            foreach (string linea in lineas)
            {
                numeroLinea++;
                if (string.IsNullOrWhiteSpace(linea))
                {
                    continue;
                }

                if (totalExpedientes == CapacidadMaxima)
                {
                    Console.WriteLine("[!] El archivo supera la capacidad de 100 expedientes.");
                    cargaCompleta = false;
                    break;
                }

                if (!CargarLinea(linea.Split('|')))
                {
                    Console.WriteLine($"[!] No se pudo cargar la línea {numeroLinea} de {archivo}.");
                    cargaCompleta = false;
                }
            }
        }
        catch (FileNotFoundException)
        {
            // Si falta también el archivo de demostración, se empieza con una lista vacía.
        }
        catch (IOException)
        {
            Console.WriteLine("[!] No se pudo leer el archivo de expedientes.");
            cargaCompleta = false;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("[!] No hay permiso para leer el archivo de expedientes.");
            cargaCompleta = false;
        }

        if (!cargaCompleta)
        {
            Console.WriteLine("[!] Puedes consultar lo cargado. El guardado quedó bloqueado.");
        }
    }

    private static IEnumerable<string> LeerExpedientesIniciales(out string archivo)
    {
        archivo = NombreArchivo;
        try
        {
            return File.ReadLines(archivo);
        }
        catch (FileNotFoundException)
        {
            archivo = NombreArchivoDemo;
            return File.ReadLines(Path.Combine(AppContext.BaseDirectory, archivo));
        }
    }

    private static bool CargarLinea(string[] campos)
    {
        if ((campos.Length != 5 && campos.Length != 12) ||
            string.IsNullOrWhiteSpace(campos[0]) || ExisteCodigo(campos[0]))
        {
            return false;
        }

        string telefono = "";
        string direccion = "";
        int? cantidadFolios = null;
        string tipo = "";
        DateTime? fecha = null;
        EstadoTramite? estado = null;
        string motivo = "";

        if (campos.Length == 12)
        {
            telefono = campos[5];
            direccion = campos[6];
            if (campos[7] != "")
            {
                if (!int.TryParse(campos[7], out int numeroFolios) || numeroFolios <= 0)
                {
                    return false;
                }

                cantidadFolios = numeroFolios;
            }

            tipo = campos[8];
            if (campos[9] != "")
            {
                if (!DateTime.TryParseExact(campos[9], FormatoFecha, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out DateTime fechaLeida))
                {
                    return false;
                }

                fecha = fechaLeida;
            }

            if (campos[10] != "")
            {
                if (!Enum.TryParse(campos[10], out EstadoTramite estadoLeido) ||
                    !Enum.IsDefined(estadoLeido) ||
                    estadoLeido.ToString() != campos[10])
                {
                    return false;
                }

                estado = estadoLeido;
            }

            motivo = campos[11];
        }

        int i = totalExpedientes;
        codigos[i] = campos[0];
        dni[i] = campos[1];
        nombres[i] = campos[2];
        correos[i] = campos[3];
        asuntos[i] = campos[4];
        telefonos[i] = telefono;
        direcciones[i] = direccion;
        folios[i] = cantidadFolios;
        tiposTramite[i] = tipo;
        fechasRegistro[i] = fecha;
        estados[i] = estado;
        motivosObservacion[i] = motivo;
        totalExpedientes++;
        return true;
    }
}
