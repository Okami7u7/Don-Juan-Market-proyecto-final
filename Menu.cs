// Maneja el menú de consola, validaciones y mensajes al usuario.
using System.Linq;

namespace DonJuanMarket_Elias_Hernandez;

public class Menu
{
    private readonly Supermercado supermercado = new();

    public void Ejecutar()
    {
        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            MostrarTitulo("DON JUAN MARKET - SISTEMA DE GESTIÓN");

            Console.WriteLine("1. Registrar producto");
            Console.WriteLine("2. Buscar producto");
            Console.WriteLine("3. Actualizar producto");
            Console.WriteLine("4. Eliminar producto");
            Console.WriteLine("5. Mostrar inventario ordenado");
            Console.WriteLine("6. Registrar cliente en la cola");
            Console.WriteLine("7. Ver clientes en espera");
            Console.WriteLine("8. Atender siguiente cliente");
            Console.WriteLine("9. Mostrar estadísticas");
            Console.WriteLine("10. Salir");

            Console.Write("\nSeleccione una opción: ");

            string opcion = Console.ReadLine() ?? "";
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    RegistrarProducto();
                    break;

                case "2":
                    BuscarProducto();
                    break;

                case "3":
                    ActualizarProducto();
                    break;

                case "4":
                    EliminarProducto();
                    break;

                case "5":
                    MostrarInventario();
                    break;

                case "6":
                    RegistrarCliente();
                    break;

                case "7":
                    MostrarCola();
                    break;

                case "8":
                    AtenderCliente();
                    break;

                case "9":
                    MostrarEstadisticas();
                    break;

                case "10":
                    salir = true;
                    Console.WriteLine("Programa finalizado.");
                    break;

                default:
                    Mensaje("Opción no válida.");
                    Pausa();
                    break;
            }
        }
    }

    private void RegistrarProducto()
    {
        MostrarTitulo("REGISTRAR PRODUCTO");

        // El código debe tener el formato PROD + números.
        string codigo = LeerCodigoProducto();

        // Verifica que el código no esté utilizado.
        if (supermercado.BuscarProducto(codigo) != null)
        {
            Mensaje($"Error: el código {codigo} ya está utilizado.");
            Pausa();
            return;
        }

        string nombre = LeerTexto("Nombre: ");
        string categoria = LeerTexto("Categoría: ");
        double precio = LeerDoublePositivo("Precio: ");
        int stock = LeerEnteroNoNegativo("Cantidad en stock: ");

        Producto producto = new(codigo, nombre, categoria, precio, stock);

        if (supermercado.RegistrarProducto(producto))
            Mensaje($"Producto {codigo} ingresado correctamente.");
        else
            Mensaje($"Error: no se pudo registrar el producto {codigo}.");

        Pausa();
    }

    private void BuscarProducto()
    {
        MostrarTitulo("BUSCAR PRODUCTO");

        string codigo = LeerCodigoProducto();

        Producto? producto = supermercado.BuscarProducto(codigo);

        if (producto == null)
        {
            Mensaje($"Error: no existe un producto con el código {codigo}.");
        }
        else
        {
            Console.WriteLine("Producto encontrado:");
            Console.WriteLine($"Código: {producto.Codigo}");
            Console.WriteLine($"Nombre: {producto.Nombre}");
            Console.WriteLine($"Categoría: {producto.Categoria}");
            Console.WriteLine($"Precio: Q{producto.Precio:F2}");
            Console.WriteLine($"Stock: {producto.Stock}");
        }

        Pausa();
    }

    private void ActualizarProducto()
    {
        MostrarTitulo("ACTUALIZAR PRODUCTO");

        string codigo = LeerCodigoProducto();

        Producto? existente = supermercado.BuscarProducto(codigo);

        if (existente == null)
        {
            Mensaje($"Error: no existe un producto con el código {codigo}.");
            Pausa();
            return;
        }

        Console.WriteLine($"Actual: {existente}");

        string nombre = LeerTexto("Nuevo nombre: ");
        string categoria = LeerTexto("Nueva categoría: ");
        double precio = LeerDoublePositivo("Nuevo precio: ");
        int stock = LeerEnteroNoNegativo("Nuevo stock: ");

        supermercado.ActualizarProducto(
            codigo,
            nombre,
            categoria,
            precio,
            stock
        );

        Mensaje($"Producto {codigo} actualizado correctamente.");

        Pausa();
    }

    private void EliminarProducto()
    {
        MostrarTitulo("ELIMINAR PRODUCTO");

        string codigo = LeerCodigoProducto();

        if (supermercado.EliminarProducto(codigo))
        {
            Mensaje($"Producto {codigo} eliminado correctamente.");
        }
        else
        {
            Mensaje($"Error: no existe un producto con el código {codigo}.");
        }

        Pausa();
    }

    private void MostrarInventario()
    {
        MostrarTitulo("INVENTARIO ORDENADO - RECORRIDO IN-ORDER");

        List<Producto> productos = supermercado.ObtenerInventario();

        if (productos.Count == 0)
        {
            Mensaje("El inventario está vacío.");
            Pausa();
            return;
        }

        Console.WriteLine(
            "{0,-10} {1,-25} {2,-18} {3,10} {4,6}",
            "Código",
            "Nombre",
            "Categoría",
            "Precio",
            "Stock"
        );

        Console.WriteLine(new string('-', 75));

        foreach (Producto producto in productos)
            Console.WriteLine(producto);

        Pausa();
    }

    private void RegistrarCliente()
    {
        MostrarTitulo("REGISTRAR CLIENTE");

        string nombre = LeerTexto("Nombre: ");

        Console.WriteLine("1. VIP");
        Console.WriteLine("2. Adulto mayor");
        Console.WriteLine("3. Regular");

        int opcion = LeerOpcion("Tipo de cliente: ", 1, 3);

        TipoCliente tipo = (TipoCliente)(opcion - 1);

        int productos = LeerEnteroNoNegativo(
            "Número de productos: "
        );

        Cliente cliente = new(nombre, tipo, productos);

        supermercado.RegistrarCliente(cliente);

        Mensaje("Cliente agregado a la Cola de Prioridad.");

        Pausa();
    }

    private void MostrarCola()
    {
        MostrarTitulo("CLIENTES EN ESPERA");

        List<Cliente> clientes =
            supermercado.ObtenerClientesEnEspera();

        if (clientes.Count == 0)
        {
            Mensaje("No hay clientes en espera.");
            Pausa();
            return;
        }

        int posicion = 1;

        foreach (Cliente cliente in clientes)
            Console.WriteLine($"{posicion++}. {cliente}");

        Pausa();
    }

    private void AtenderCliente()
    {
        MostrarTitulo("ATENDER CLIENTE");

        Cliente? cliente = supermercado.AtenderCliente();

        if (cliente == null)
        {
            Mensaje("No hay clientes en espera.");
        }
        else
        {
            Mensaje(
                $"Cliente atendido: {cliente.Nombre} ({cliente.Tipo})."
            );
        }

        Pausa();
    }

    private void MostrarEstadisticas()
    {
        MostrarTitulo("ESTADÍSTICAS DEL SISTEMA");

        // Obtenemos los datos actuales del inventario y de la cola.
        List<Producto> productos = supermercado.ObtenerInventario();
        List<Cliente> clientes = supermercado.ObtenerClientesEnEspera();

        // Estadísticas de productos.
        int totalProductos = productos.Count;

        int productosConStock =
            productos.Count(p => p.Stock > 0);

        int productosAgotados =
            productos.Count(p => p.Stock == 0);

        double valorInventario =
            productos.Sum(p => p.Precio * p.Stock);

        // Estadísticas de clientes.
        int totalClientes = clientes.Count;

        int clientesVip =
            clientes.Count(c => c.Tipo == TipoCliente.VIP);

        int adultosMayores =
            clientes.Count(c => c.Tipo == TipoCliente.ADULTO_MAYOR);

        int clientesRegulares =
            clientes.Count(c => c.Tipo == TipoCliente.REGULAR);

        Console.WriteLine("PRODUCTOS");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total de productos:       {totalProductos}");
        Console.WriteLine($"Productos con stock:      {productosConStock}");
        Console.WriteLine($"Productos agotados:       {productosAgotados}");
        Console.WriteLine($"Valor del inventario:     Q{valorInventario:F2}");

        Console.WriteLine("\nCLIENTES EN ESPERA");
        Console.WriteLine("----------------------------------------");
        Console.WriteLine($"Total de clientes:        {totalClientes}");
        Console.WriteLine($"Clientes VIP:             {clientesVip}");
        Console.WriteLine($"Adultos mayores:          {adultosMayores}");
        Console.WriteLine($"Clientes regulares:       {clientesRegulares}");

        Pausa();
    }

    // Valida que el código tenga el formato PROD + números.
    // Ejemplos: PROD001, PROD002, PROD25, PROD100.
    private static string LeerCodigoProducto()
    {
        while (true)
        {
            Console.Write("Código (PROD + números): ");

            string codigo =
                Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (System.Text.RegularExpressions.Regex.IsMatch(
                codigo,
                @"^PROD\d+$"))
            {
                return codigo;
            }

            Console.WriteLine(
                "Error: el código debe tener el formato PROD + números."
            );

            Console.WriteLine("Ejemplo: PROD003");
        }
    }

    private static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            string valor =
                Console.ReadLine()?.Trim() ?? "";

            if (!string.IsNullOrWhiteSpace(valor))
                return valor;

            Console.WriteLine(
                "El dato no puede quedar vacío."
            );
        }
    }

    private static double LeerDoublePositivo(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            if (double.TryParse(
                Console.ReadLine(),
                out double valor) && valor > 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Ingrese un número mayor que 0."
            );
        }
    }

    private static int LeerEnteroNoNegativo(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(
                Console.ReadLine(),
                out int valor) && valor >= 0)
            {
                return valor;
            }

            Console.WriteLine(
                "Ingrese un número entero mayor o igual a 0."
            );
        }
    }

    private static int LeerOpcion(
        string mensaje,
        int minimo,
        int maximo)
    {
        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(
                Console.ReadLine(),
                out int valor) &&
                valor >= minimo &&
                valor <= maximo)
            {
                return valor;
            }

            Console.WriteLine(
                $"Seleccione una opción entre {minimo} y {maximo}."
            );
        }
    }

    private static void MostrarTitulo(string titulo)
    {
        Console.WriteLine(
            "============================================================"
        );

        Console.WriteLine(titulo);

        Console.WriteLine(
            "============================================================"
        );
    }

    private static void Mensaje(string mensaje)
    {
        Console.WriteLine($"\n>> {mensaje}");
    }

    private static void Pausa()
    {
        Console.WriteLine(
            "\nPresione Enter para regresar al menú..."
        );

        Console.ReadLine();
    }
}