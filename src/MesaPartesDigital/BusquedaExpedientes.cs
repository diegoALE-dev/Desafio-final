using System;
using static DatosExpedientes;

public static class BusquedaExpedientes
{
    public static void BuscarExpediente()
    {
        if (totalExpedientes == 0)
        {
            Console.WriteLine("[!] No hay expedientes registrados.");
            return;
        }

        while (true)
        {
            Console.WriteLine("\n--- BUSCAR Y CONSULTAR EXPEDIENTES ---");
            Console.WriteLine("1. Por código");
            Console.WriteLine("2. Por DNI");
            Console.WriteLine("3. Por estado");
            Console.WriteLine("4. Por tipo de trámite");
            Console.WriteLine("0. Volver");

            switch (ElegirNumero(4))
            {
                case 1: ConsultarPorCodigo(); break;
                case 2: ConsultarPorDni(); break;
                case 3: FiltrarPorEstado(); break;
                case 4: FiltrarPorTipo(); break;
                case 0: return;
            }
        }
    }

    private static void ConsultarPorCodigo()
    {
        Console.Write("Código de expediente: ");
        int indice = BuscarIndicePorCodigo(Console.ReadLine());
        if (indice == -1)
        {
            Console.WriteLine("[!] Expediente no encontrado.");
            return;
        }

        MostrarFicha(indice);
    }

    public static int BuscarIndicePorCodigo(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo)) return -1;

        int[] indices = OrdenamientoExpedientes.ObtenerIndicesOrdenados(1);
        return BuscarEnRango(codigo.Trim(), indices, 0, indices.Length - 1);
    }

    private static int BuscarEnRango(string codigo, int[] indices, int izquierda, int derecha)
    {
        if (izquierda > derecha) return -1;

        int centro = izquierda + (derecha - izquierda) / 2;
        int indice = indices[centro];
        int comparacion = string.Compare(codigo, codigos[indice], StringComparison.OrdinalIgnoreCase);
        if (comparacion == 0) return indice;

        if (comparacion < 0)
        {
            return BuscarEnRango(codigo, indices, izquierda, centro - 1);
        }

        return BuscarEnRango(codigo, indices, centro + 1, derecha);
    }

    private static void ConsultarPorDni()
    {
        Console.Write("DNI: ");
        string documento = Console.ReadLine()?.Trim();
        if (documento == null) return;

        int[] resultados = new int[totalExpedientes];
        int cantidad = 0;
        for (int i = 0; i < totalExpedientes; i++)
        {
            if (dni[i] == documento)
            {
                resultados[cantidad] = i;
                cantidad++;
            }
        }

        MostrarResultados(resultados, cantidad);
    }

    private static void FiltrarPorEstado()
    {
        Console.WriteLine("1. Registrado");
        Console.WriteLine("2. En revisión");
        Console.WriteLine("3. Observado");
        Console.WriteLine("4. Atendido");
        Console.WriteLine("5. Sin información");
        Console.WriteLine("0. Volver");

        EstadoTramite? estado;
        switch (ElegirNumero(5))
        {
            case 1: estado = EstadoTramite.Registrado; break;
            case 2: estado = EstadoTramite.EnRevision; break;
            case 3: estado = EstadoTramite.Observado; break;
            case 4: estado = EstadoTramite.Atendido; break;
            case 5: estado = null; break;
            default: return;
        }

        int[] resultados = new int[totalExpedientes];
        int cantidad = 0;
        for (int i = 0; i < totalExpedientes; i++)
        {
            if (estados[i] == estado)
            {
                resultados[cantidad] = i;
                cantidad++;
            }
        }

        MostrarResultados(resultados, cantidad);
    }

    private static void FiltrarPorTipo()
    {
        string[] tipos = new string[totalExpedientes];
        int cantidadTipos = 0;
        for (int i = 0; i < totalExpedientes; i++)
        {
            string tipo = TextoDisponible(tiposTramite[i]);
            int posicion = 0;
            while (posicion < cantidadTipos &&
                   !string.Equals(tipos[posicion], tipo, StringComparison.OrdinalIgnoreCase))
            {
                posicion++;
            }

            if (posicion == cantidadTipos)
            {
                tipos[cantidadTipos] = tipo;
                cantidadTipos++;
                Console.WriteLine($"{cantidadTipos}. {tipo}");
            }
        }

        Console.WriteLine("0. Volver");
        int seleccion = ElegirNumero(cantidadTipos);
        if (seleccion == 0) return;

        string elegido = tipos[seleccion - 1];
        int[] resultados = new int[totalExpedientes];
        int cantidad = 0;
        for (int i = 0; i < totalExpedientes; i++)
        {
            if (string.Equals(TextoDisponible(tiposTramite[i]), elegido, StringComparison.OrdinalIgnoreCase))
            {
                resultados[cantidad] = i;
                cantidad++;
            }
        }

        MostrarResultados(resultados, cantidad);
    }

    private static int ElegirNumero(int maximo)
    {
        while (true)
        {
            Console.Write("Elige una opción: ");
            string entrada = Console.ReadLine();
            if (entrada == null) return 0;

            if (int.TryParse(entrada, out int numero) && numero >= 0 && numero <= maximo)
            {
                return numero;
            }

            Console.WriteLine($"Elige un número del 0 al {maximo}.");
        }
    }

    private static void MostrarResultados(int[] resultados, int cantidad)
    {
        if (cantidad == 0)
        {
            Console.WriteLine("[!] No se encontraron coincidencias.");
            return;
        }

        if (cantidad == 1)
        {
            MostrarFicha(resultados[0]);
            return;
        }

        for (int i = 0; i < cantidad; i++)
        {
            Console.Write($"{i + 1}. ");
            MostrarResumen(resultados[i]);
        }

        Console.WriteLine("0. Volver");
        int seleccion = ElegirNumero(cantidad);
        if (seleccion > 0)
        {
            MostrarFicha(resultados[seleccion - 1]);
        }
    }

    public static void MostrarResumen(int indice)
    {
        string fecha = fechasRegistro[indice]?.ToString("dd/MM/yyyy") ?? "Sin información";
        Consola.EscribirResultado($"{codigos[indice]} | {TextoDisponible(tiposTramite[indice])} | " +
                          $"{fecha} | {NombreEstado(estados[indice])}");
    }

    private static void MostrarFicha(int indice)
    {
        Console.WriteLine("\n--- FICHA DEL EXPEDIENTE ---");
        MostrarResumen(indice);
        Consola.EscribirResultado($"DNI: {dni[indice]}");
        Consola.EscribirResultado($"Nombre: {TextoDisponible(nombres[indice])}");
        Consola.EscribirResultado($"Correo: {TextoDisponible(correos[indice])}");
        Consola.EscribirResultado($"Teléfono: {TextoDisponible(telefonos[indice])}");
        Consola.EscribirResultado($"Dirección: {TextoDisponible(direcciones[indice])}");
        Consola.EscribirResultado($"Folios: {folios[indice]?.ToString() ?? "Sin información"}");
        Consola.EscribirResultado($"Asunto: {TextoDisponible(asuntos[indice])}");

        if (estados[indice] == EstadoTramite.Observado)
        {
            Consola.EscribirResultado($"Motivo: {TextoDisponible(motivosObservacion[indice])}");
        }
    }

    private static string NombreEstado(EstadoTramite? estado)
    {
        switch (estado)
        {
            case EstadoTramite.Registrado: return "Registrado";
            case EstadoTramite.EnRevision: return "En revisión";
            case EstadoTramite.Observado: return "Observado";
            case EstadoTramite.Atendido: return "Atendido";
            default: return "Sin información";
        }
    }

    private static string TextoDisponible(string texto)
    {
        return string.IsNullOrWhiteSpace(texto) ? "Sin información" : texto;
    }
}