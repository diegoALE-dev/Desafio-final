# Mesa de Partes Digital - Municipalidad de Los Olivos

Aplicación de consola en C# que propone digitalizar el registro y la consulta de expedientes de una mesa de partes. Es un proyecto académico del curso **Fundamentos de Programación (CIIN1205P)** de la Universidad Privada del Norte.

## Objetivo

Organizar la información de los trámites mediante un prototipo que permita registrar expedientes, consultar su estado, ordenarlos y conservarlos en un archivo de texto.

## Funcionalidades

- Selección del tipo de trámite en el catálogo y registro de DNI, nombres, apellidos, correo electrónico, teléfono, dirección, número de folios y asunto opcional.
- Validación del DNI con exactamente ocho dígitos numéricos, conservando los ceros iniciales.
- Generación de un código único con el formato `EXP-AAAA-NNN`, como `EXP-2026-001`.
- Búsqueda por código o DNI y filtros por estado o tipo de trámite. El mismo DNI puede tener varios expedientes.
- Actualización por código entre los estados Registrado, En revisión, Observado y Atendido. Al observar se registra un motivo y al subsanar se borra.
- Listado por código, por fecha más antigua o por fecha más reciente.
- Almacenamiento y recuperación de hasta 100 expedientes mediante un archivo de texto.

## Tecnologías y conceptos utilizados

- C# y .NET 10.
- Entrada y salida mediante `Console.ReadLine()` y `Console.WriteLine()`.
- Estructuras de control `if`, `switch`, `while` y `for`.
- Métodos y clases estáticas para organizar el código.
- Arreglos paralelos de texto, números, fechas y estados.

El programa utiliza búsqueda binaria recursiva por código, búsqueda secuencial por DNI y para los filtros, ordenamiento con ciclos anidados y archivos de texto mediante `System.IO`. El proyecto no requiere paquetes NuGet adicionales.

## Organización del código

```text
src/
└── MesaPartesDigital/
    ├── MesaPartesDigital.csproj
    ├── Program.cs
    ├── DatosExpedientes.cs
    ├── RegistroExpedientes.cs
    ├── EstadosExpedientes.cs
    ├── BusquedaExpedientes.cs
    ├── OrdenamientoExpedientes.cs
    ├── Consola.cs
    └── ArchivoExpedientes.cs
```

| Archivo | Propósito |
| --- | --- |
| `MesaPartesDigital.csproj` | Configuración del proyecto de consola y versión de .NET |
| `Program.cs` | Menú principal y selección de opciones |
| `DatosExpedientes.cs` | Arreglos de la ficha, generación de códigos, estados, contador y capacidad máxima |
| `RegistroExpedientes.cs` | Catálogo, registro y validación de los datos |
| `EstadosExpedientes.cs` | Transiciones de estado y motivo de observación |
| `BusquedaExpedientes.cs` | Búsquedas, filtros y visualización de fichas |
| `OrdenamientoExpedientes.cs` | Listado ordenado por código o fecha |
| `Consola.cs` | Resultados y confirmaciones en verde |
| `ArchivoExpedientes.cs` | Módulo destinado a la lectura y escritura de datos |

Los arreglos comparten el mismo índice para relacionar toda la ficha de cada expediente. La capacidad máxima es de **100 registros**.

## Requisitos

- Visual Studio con soporte para proyectos de consola de .NET 10.
- SDK de .NET 10 instalado.

## Ejecución en Visual Studio

1. Abrir `src/MesaPartesDigital/MesaPartesDigital.csproj`.
2. Seleccionar **Compilar > Compilar solución**.
3. Ejecutar con **Depurar > Iniciar sin depurar** o **Ctrl + F5**.

Opciones del menú principal

```text
1. Registrar expediente
2. Buscar y consultar expediente
3. Actualizar estado del trámite
4. Listar y ordenar expedientes
0. Salir
```

Desde PowerShell, abierto en la raíz del repositorio, también puedes usar:

```powershell
dotnet build src/MesaPartesDigital/MesaPartesDigital.csproj
dotnet run --project src/MesaPartesDigital/MesaPartesDigital.csproj
```

## Almacenamiento

`expedientes.txt` se lee en el directorio desde el que se ejecuta la aplicación. Los expedientes del parcial pueden tener cinco campos. Un guardado correcto escribe doce campos por línea y conserva sus códigos y datos originales.

```text
codigo|dni|nombre|correo|asunto|telefono|direccion|folios|tipo|fecha|estado|motivo
EXP-2026-001|00123456|Ana Prueba|ana@example.org|Solicitud de licencia|987654321|Av. Ejemplo 123|3|Solicitud múltiple|2026-11-22|Registrado|
```

La última columna guarda el motivo mientras el expediente está Observado. Los campos que no existían en el parcial quedan vacíos. Si hay líneas inválidas, códigos repetidos o filas que exceden el límite, la aplicación permite consultar los expedientes válidos que alcanzó a cargar y bloquea el guardado. Así conserva el contenido original para que el equipo revise el archivo. Usa datos ficticios para las pruebas.
