# Mesa de Partes Digital - Municipalidad de Los Olivos

Aplicación de consola en C# que propone digitalizar el registro y la consulta de expedientes de una mesa de partes. Es un proyecto académico del curso **Fundamentos de Programación (CIIN1205P)** de la Universidad Privada del Norte.

## Objetivo

Organizar la información de los trámites mediante un prototipo que permita registrar expedientes, consultar y actualizar su estado, ordenarlos, generar estadísticas y conservarlos en un archivo de texto.

## Funcionalidades

- Selección del tipo de trámite en el catálogo y registro de DNI, nombres, apellidos, correo electrónico, teléfono, dirección, número de folios y asunto opcional.
- Validación del DNI con exactamente ocho dígitos numéricos, conservando los ceros iniciales.
- Generación de un código único con el formato `EXP-AAAA-NNN`, como `EXP-2026-001`.
- Búsqueda por código o DNI y filtros por estado o tipo de trámite. El mismo DNI puede tener varios expedientes.
- Actualización por código entre los estados Registrado, En revisión, Observado y Atendido. Al observar se registra un motivo y al subsanar se borra.
- Listado por código, por fecha más antigua o por fecha más reciente.
- Generación de estadísticas de expedientes por tipo de trámite y estado.
- Visualización del total de expedientes registrados, la capacidad máxima y los espacios disponibles en el sistema.
- Almacenamiento y recuperación de hasta 100 expedientes mediante un archivo de texto.

## Tecnologías y conceptos utilizados

- C# y .NET 10.
- Entrada y salida mediante `Console.ReadLine()` y `Console.WriteLine()`.
- Estructuras de control `if`, `switch`, `while` y `for`.
- Métodos y clases estáticas para organizar el código.
- Arreglos paralelos de texto, números, fechas y estados.

El programa utiliza búsqueda binaria recursiva por código, búsqueda secuencial por DNI y para los filtros, ordenamiento con ciclos anidados y archivos de texto mediante `System.IO`. El proyecto no requiere paquetes NuGet adicionales.

**Algoritmo de conteo y clasificación secuencial**

El módulo `EstadisticasExpedientes.cs` implementa el método `MostrarEstadisticas()`, que se ejecuta al seleccionar la opción **5. Estadísticas de expedientes**. Su funcionamiento es el siguiente:

1. Inicializa en cero los contadores de tipos de trámite y estados cada vez que se consulta la opción.
2. Recorre una sola vez los expedientes almacenados mediante un ciclo `for`, desde el índice `0` hasta `totalExpedientes - 1`. Solo procesa las posiciones ocupadas de los arreglos.
3. Utiliza dos estructuras `switch` dentro del mismo ciclo: una clasifica los tipos de trámite y otra los estados. Incrementa el contador correspondiente mediante `++`.
4. Muestra el total, la distribución por categorías y la capacidad disponible, calculada como `CapacidadMaxima - totalExpedientes`. Los resultados se presentan mediante `Consola.EscribirResultado()`.

Los tipos de trámite son **Solicitud múltiple** y **Deducción del impuesto predial**. Los estados son **Registrado**, **En revisión**, **Observado** y **Atendido**. Si existen registros con otros tipos o sin un estado definido, se muestran también los contadores **Otros tipos** y **Sin estado definido**, respectivamente. Esto permite incluir los expedientes antiguos que carecen de esos datos.

La complejidad temporal del algoritmo es **O(n)**, donde `n` es `totalExpedientes`, porque realiza un único recorrido. El espacio auxiliar es **O(1)**, ya que utiliza una cantidad fija de contadores. Cada consulta vuelve a calcular los resultados a partir de los datos actuales en memoria, por lo que incorpora los registros y cambios de estado guardados sin acumular los conteos de consultas anteriores.

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
    ├── EstadisticasExpedientes.cs
    ├── Consola.cs
    ├── expedientes-demo.txt
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
| `EstadisticasExpedientes.cs` | Conteo y clasificación de expedientes por tipo de trámite y estado, y visualización de la capacidad disponible mediante `MostrarEstadisticas()` |
| `Consola.cs` | Resultados y confirmaciones en verde |
| `expedientes-demo.txt` | Diez expedientes ficticios para practicar |
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
5. Estadísticas de expedientes
0. Salir
```

Desde PowerShell, abierto en la raíz del repositorio, también puedes usar:

```powershell
dotnet build src/MesaPartesDigital/MesaPartesDigital.csproj
dotnet run --project src/MesaPartesDigital/MesaPartesDigital.csproj
```

## Probar con los datos de demostración

Al ejecutar el programa en una carpeta donde aún no existe `expedientes.txt`, se cargan automáticamente los diez expedientes ficticios de `expedientes-demo.txt`. Si ya hay un archivo local, el programa carga ese archivo. El primer cambio guardado conserva los datos y las modificaciones en `expedientes.txt`.

Para comprobar los valores iniciales de las estadísticas, consulta primero la opción **5**, antes de registrar expedientes o modificar estados.

- En **4. Listar y ordenar expedientes**, prueba los tres criterios y compara el orden de los diez resultados. Dos expedientes comparten fecha para comprobar el desempate por código.
- En **2. Buscar y consultar expediente**, busca el DNI `00000001`. Aparecerán dos expedientes. También puedes filtrar por **Observado** y consultar sus motivos.
- En **3. Actualizar estado del trámite**, prueba el código ficticio `EXP-2026-004`, que comienza como **Observado**.
- En **5. Estadísticas de expedientes**, verifica el total y su distribución por tipo de trámite y estado. Con los diez expedientes originales, sin modificaciones, los resultados esperados son:

| Indicador | Resultado esperado |
| --- | --- |
| Total de expedientes | 10 |
| Solicitud múltiple | 5 |
| Deducción del impuesto predial | 5 |
| Registrados | 3 |
| En revisión | 3 |
| Observados | 2 |
| Atendidos | 2 |
| Capacidad máxima | 100 |
| Espacios disponibles | 90 |

En los datos de demostración, **Otros tipos** y **Sin estado definido** no aparecen porque sus contadores son cero. Tanto la suma de los tipos como la suma de los estados debe coincidir con el total de expedientes.

**Pruebas de actualización de las estadísticas**

En una demostración nueva con los diez expedientes originales, realiza esta secuencia de pruebas manuales. Los resultados indicados suponen que cada registro o cambio de estado se guarda correctamente:

1. Consulta la opción **5** dos veces seguidas. Los resultados deben coincidir con la tabla anterior en ambas consultas, sin duplicarse.
2. En la opción **1**, registra un nuevo expediente de tipo **Solicitud múltiple** con datos ficticios válidos. Vuelve a la opción **5**: el total debe ser **11**, las solicitudes múltiples **6**, los registrados **4** y los espacios disponibles **89**. La deducción predial debe mantenerse en **5** y los demás estados en **3** en revisión, **2** observados y **2** atendidos.
3. En la opción **3**, busca `EXP-2026-004` y selecciona **1. Volver a En revisión**. Consulta de nuevo la opción **5**: los observados deben disminuir a **1** y los expedientes en revisión aumentar a **4**. El total debe seguir en **11**, los registrados en **4**, los atendidos en **2** y los espacios disponibles en **89**. Las cantidades por tipo de trámite no cambian.
4. Sal con la opción **0** y vuelve a ejecutar el programa desde la misma carpeta. Consulta la opción **5** para comprobar que los datos guardados mantienen los resultados del paso anterior.

Para comprobar el caso sin expedientes, utiliza otra carpeta de prueba con un archivo `expedientes.txt` vacío antes de iniciar el programa. Como el archivo local existe, no se cargan los datos de demostración. La opción **5** debe mostrar **0** expedientes, **0** en cada tipo y estado principal, una capacidad máxima de **100** y **100** espacios disponibles.

Para comenzar otra demostración conservando tus pruebas anteriores, ejecuta el programa desde una carpeta nueva. Con la ruta del proyecto, puedes usar `dotnet run --project "<ruta al archivo MesaPartesDigital.csproj>"` desde esa carpeta.

## Almacenamiento

`expedientes.txt` se lee en el directorio desde el que se ejecuta la aplicación. Los expedientes del parcial pueden tener cinco campos. Un guardado correcto escribe doce campos por línea y conserva sus códigos y datos originales.

```text
codigo|dni|nombre|correo|asunto|telefono|direccion|folios|tipo|fecha|estado|motivo
EXP-2026-001|00123456|Ana Prueba|ana@example.org|Solicitud de licencia|987654321|Av. Ejemplo 123|3|Solicitud múltiple|2026-11-22|Registrado|
```

La última columna guarda el motivo mientras el expediente está Observado. Los campos que no existían en el parcial quedan vacíos. Si hay líneas inválidas, códigos repetidos o filas que exceden el límite, la aplicación permite consultar los expedientes válidos que alcanzó a cargar y bloquea el guardado. Así conserva el contenido original para que el equipo revise el archivo. Usa datos ficticios para las pruebas.
