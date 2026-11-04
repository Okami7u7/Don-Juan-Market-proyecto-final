// Nodo utilizado por el Árbol AVL. Guarda el producto y sus enlaces.
namespace DonJuanMarket_Elias_Hernandez;

public class NodoAVL
{
    public Producto Producto { get; set; }
    public NodoAVL? Izquierdo { get; set; }
    public NodoAVL? Derecho { get; set; }
    public int Altura { get; set; }

    public NodoAVL(Producto producto)
    {
        Producto = producto;
        Altura = 1;
    }
}
