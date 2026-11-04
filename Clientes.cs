// Representa a una persona que espera ser atendida en la tienda.
namespace DonJuanMarket_Elias_Hernandez;

public enum TipoCliente
{
    VIP,
    ADULTO_MAYOR,
    REGULAR
}

public class Cliente
{
    public string Nombre { get; }
    public TipoCliente Tipo { get; }
    public int NumeroProductos { get; }
    public DateTime HoraLlegada { get; }
    public long OrdenLlegada { get; internal set; }

    public int Prioridad => Tipo switch
    {
        TipoCliente.VIP => 1,
        TipoCliente.ADULTO_MAYOR => 2,
        _ => 3
    };

    public Cliente(string nombre, TipoCliente tipo, int numeroProductos)
    {
        Nombre = nombre;
        Tipo = tipo;
        NumeroProductos = numeroProductos;
        HoraLlegada = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Nombre,-22} {Tipo,-15} Prioridad: {Prioridad}  Productos: {NumeroProductos,-4} Llegada: {HoraLlegada:HH:mm:ss}";
    }
}
