// Representa un producto del inventario de Don Juan Market.
namespace DonJuanMarket_Elias_Hernandez;

public class Producto
{
    public string Codigo { get; private set; }
    public string Nombre { get; private set; }
    public string Categoria { get; private set; }
    public double Precio { get; private set; }
    public int Stock { get; private set; }

    public Producto(string codigo, string nombre, string categoria, double precio, int stock)
    {
        Codigo = codigo;
        Nombre = nombre;
        Categoria = categoria;
        Precio = precio;
        Stock = stock;
    }

    public void ActualizarDatos(string nombre, string categoria, double precio, int stock)
    {
        Nombre = nombre;
        Categoria = categoria;
        Precio = precio;
        Stock = stock;
    }

    public override string ToString()
    {
        return $"{Codigo,-10} {Nombre,-25} {Categoria,-18} Q{Precio,8:F2} {Stock,6}";
    }
}
