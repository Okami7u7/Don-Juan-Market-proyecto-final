// Integra el inventario AVL y la Cola de Prioridad del sistema.
namespace DonJuanMarket_Elias_Hernandez;

public class Supermercado
{
    public ArbolAVL Inventario { get; } = new();
    public ColaPrioridad ColaClientes { get; } = new();

    public bool RegistrarProducto(Producto producto) => Inventario.Insertar(producto);
    public Producto? BuscarProducto(string codigo) => Inventario.Buscar(codigo);
    public bool ActualizarProducto(string codigo, string nombre, string categoria, double precio, int stock)
        => Inventario.Actualizar(codigo, nombre, categoria, precio, stock);
    public bool EliminarProducto(string codigo) => Inventario.Eliminar(codigo);
    public List<Producto> ObtenerInventario() => Inventario.ObtenerEnOrden();
    public void RegistrarCliente(Cliente cliente) => ColaClientes.Agregar(cliente);
    public Cliente? SiguienteCliente() => ColaClientes.VerSiguiente();
    public Cliente? AtenderCliente() => ColaClientes.Extraer();
    public List<Cliente> ObtenerClientesEnEspera() => ColaClientes.ObtenerVistaOrdenada();
}
