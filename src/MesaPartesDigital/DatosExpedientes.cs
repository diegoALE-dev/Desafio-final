using System;

public enum EstadoTramite
{
    Registrado,
    EnRevision,
    Observado,
    Atendido
}

public static class DatosExpedientes
{
    public const int CapacidadMaxima = 100;

    // El mismo índice identifica al expediente en todos los arreglos.
    public static string[] codigos = new string[CapacidadMaxima];
    public static string[] dni = new string[CapacidadMaxima];
    public static string[] nombres = new string[CapacidadMaxima];
    public static string[] correos = new string[CapacidadMaxima];
    public static string[] asuntos = new string[CapacidadMaxima];
    public static string[] telefonos = new string[CapacidadMaxima];
    public static string[] direcciones = new string[CapacidadMaxima];
    public static int?[] folios = new int?[CapacidadMaxima];
    public static string[] tiposTramite = new string[CapacidadMaxima];
    public static DateTime?[] fechasRegistro = new DateTime?[CapacidadMaxima];
    public static EstadoTramite?[] estados = new EstadoTramite?[CapacidadMaxima];
    public static string[] motivosObservacion = new string[CapacidadMaxima];

    public static int totalExpedientes = 0;

    public static string GenerarCodigoExpediente(DateTime fechaRegistro)
    {
        int correlativo = 1;
        string codigo;

        // El primer correlativo libre funciona aunque el archivo llegue desordenado.
        do
        {
            codigo = $"EXP-{fechaRegistro.Year:D4}-{correlativo:D3}";
            correlativo++;
        } while (ExisteCodigo(codigo));

        return codigo;
    }

    public static bool ExisteCodigo(string codigo)
    {
        for (int i = 0; i < totalExpedientes; i++)
        {
            if (string.Equals(codigos[i], codigo, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }

        return false;
    }

    public static bool EsTextoSeguro(string texto)
    {
        return texto == null || (!texto.Contains('|') && !texto.Contains('\r') && !texto.Contains('\n'));
    }
}
