# Mesa de Partes Digital - Municipalidad de Los Olivos

Aplicación de consola en C# que propone digitalizar el registro y la consulta de expedientes de una mesa de partes. Es un proyecto académico del curso **Fundamentos de Programación (CIIN1205P)** de la Universidad Privada del Norte.

## Objetivo

Organizar la información de los trámites mediante un prototipo sencillo que permita registrar expedientes, validar el DNI del administrado, consultar registros, ordenarlos por código y conservarlos en archivos de texto.

## Funcionalidades previstas

- Registro del DNI, nombres y apellidos, correo electrónico y asunto del trámite.
- Validación del DNI con exactamente ocho dígitos numéricos, conservando los ceros iniciales.
- Generación de códigos con el formato `EXP-2026-001`.
- Búsqueda por código de expediente o DNI, con varias coincidencias para un mismo DNI.
- Ordenamiento ascendente por código de expediente.
- Almacenamiento y recuperación de hasta 100 expedientes mediante un archivo de texto.

## Estado actual

El menú registra los datos básicos del expediente, permite buscar por código o DNI y lista los expedientes ordenados por código. La ficha del desafío final ya admite teléfono, dirección, folios, tipo de trámite, fecha, estado y motivo de observación. El catálogo, el seguimiento de estados y la consulta ampliada se incorporan en las siguientes ramas del equipo.

## Tecnologías y conceptos utilizados

- C# y .NET 10.
- Entrada y salida mediante `Console.ReadLine()` y `Console.WriteLine()`.
- Estructuras de control `if`, `switch`, `while`, `for` y `do-while`.
- Métodos y clases estáticas para organizar el código.
- Arreglos paralelos de texto, números, fechas y estados.

Los algoritmos previstos utilizan búsqueda secuencial, ordenamiento con ciclos anidados y archivos de texto mediante `System.IO`. El proyecto no requiere paquetes NuGet adicionales.

## Organización del código

```text
src/
└── MesaPartesDigital/
    ├── MesaPartesDigital.csproj
    ├── Program.cs
    ├── DatosExpedientes.cs
    ├── RegistroExpedientes.cs
    ├── BusquedaExpedientes.cs
    ├── OrdenamientoExpedientes.cs
    └── ArchivoExpedientes.cs
```

| Archivo | Propósito |
| --- | --- |
| `MesaPartesDigital.csproj` | Configuración del proyecto de consola y versión de .NET |
| `Program.cs` | Menú principal y selección de opciones |
| `DatosExpedientes.cs` | Arreglos de la ficha, generación de códigos, estados, contador y capacidad máxima |
| `RegistroExpedientes.cs` | Módulo destinado al registro y la validación del DNI |
| `BusquedaExpedientes.cs` | Módulo destinado a la consulta por código o DNI |
| `OrdenamientoExpedientes.cs` | Módulo destinado al ordenamiento por código |
| `ArchivoExpedientes.cs` | Módulo destinado a la lectura y escritura de datos |

Los arreglos comparten el mismo índice para relacionar toda la ficha de cada expediente. La capacidad máxima es de **100 registros**.

## Requisitos

- Visual Studio con soporte para proyectos de consola de .NET 10.
- SDK de .NET 10 instalado.

## Ejecución en Visual Studio

1. Abrir `src/MesaPartesDigital/MesaPartesDigital.csproj`.
2. Seleccionar **Compilar > Compilar solución**.
3. Ejecutar con **Depurar > Iniciar sin depurar** o **Ctrl + F5**.

El menú presenta las siguientes opciones:

```text
1. Registrar Expediente
2. Buscar Expediente
3. Ordenar Expedientes por Código
4. Salir
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

La última columna guarda el motivo de observación y puede quedar vacía. Los campos que no existían en el parcial quedan vacíos. Si hay líneas inválidas, códigos repetidos o filas que exceden el límite, la aplicación permite consultar los expedientes válidos que alcanzó a cargar y bloquea el guardado. Así conserva el contenido original para que el equipo revise el archivo. Usa datos ficticios para las pruebas.
