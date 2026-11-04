DON JUAN MARKET - PROYECTO FINAL
Procesos y Algoritmos II - 2026

Autor: Elias Hernandez

REQUISITOS
- .NET 9 SDK (o una versión compatible con C# 9 o superior).
- Consola de comandos.

ESTRUCTURA
- Program.cs: inicia el programa.
- Menu.cs: menú, entradas y validaciones.
- Producto.cs: datos del producto.
- NodoAVL.cs: nodo del árbol AVL.
- ArbolAVL.cs: inserción, búsqueda, actualización, eliminación, balanceo e in-order.
- Cliente.cs: datos y prioridad del cliente.
- ColaPrioridad.cs: Min Heap implementado desde cero.
- Supermercado.cs: integra las dos estructuras.

COMPILACIÓN Y EJECUCIÓN
1. Abrir una terminal dentro de esta carpeta.
2. Ejecutar: dotnet build
3. Ejecutar: dotnet run

NOTAS
- No se utiliza System.Collections.Generic.PriorityQueue.
- La Cola de Prioridad se implementa con una lista como arreglo del Min Heap.
- El AVL usa el código del producto como clave.
- En la Cola de Prioridad, un número menor de prioridad significa atención primero.
- Si hay empate, se respeta la hora/orden de llegada.
