// Min Heap propio para atender clientes por prioridad y respetar FIFO en empates.
namespace DonJuanMarket_Elias_Hernandez;

public class ColaPrioridad
{
    private readonly List<Cliente> heap = new();
    private long contadorLlegada = 0;

    public int Cantidad => heap.Count;

    // Menor prioridad numérica significa que el cliente debe pasar primero.
    private bool EsMasPrioritario(Cliente a, Cliente b)
    {
        if (a.Prioridad != b.Prioridad)
            return a.Prioridad < b.Prioridad;

        if (a.HoraLlegada != b.HoraLlegada)
            return a.HoraLlegada < b.HoraLlegada;

        return a.OrdenLlegada < b.OrdenLlegada;
    }

    public void Agregar(Cliente cliente)
    {
        cliente.OrdenLlegada = contadorLlegada++;
        heap.Add(cliente);
        BubbleUp(heap.Count - 1);
    }

    // Sube el cliente hasta encontrar la posición que le corresponde.
    private void BubbleUp(int indice)
    {
        while (indice > 0)
        {
            int padre = (indice - 1) / 2;
            if (!EsMasPrioritario(heap[indice], heap[padre])) break;
            Intercambiar(indice, padre);
            indice = padre;
        }
    }

    public Cliente? VerSiguiente()
    {
        return heap.Count == 0 ? null : heap[0];
    }

    public Cliente? Extraer()
    {
        if (heap.Count == 0) return null;

        Cliente resultado = heap[0];
        int ultimo = heap.Count - 1;
        heap[0] = heap[ultimo];
        heap.RemoveAt(ultimo);

        if (heap.Count > 0)
            BubbleDown(0);

        return resultado;
    }

    // Baja la raíz para volver a dejar el menor elemento en la posición 0.
    private void BubbleDown(int indice)
    {
        while (true)
        {
            int izquierdo = indice * 2 + 1;
            int derecho = indice * 2 + 2;
            int menor = indice;

            if (izquierdo < heap.Count && EsMasPrioritario(heap[izquierdo], heap[menor]))
                menor = izquierdo;

            if (derecho < heap.Count && EsMasPrioritario(heap[derecho], heap[menor]))
                menor = derecho;

            if (menor == indice) break;

            Intercambiar(indice, menor);
            indice = menor;
        }
    }

    private void Intercambiar(int a, int b)
    {
        (heap[a], heap[b]) = (heap[b], heap[a]);
    }

    // Devuelve una copia para mostrar la cola sin modificar el Heap.
    public List<Cliente> ObtenerVistaOrdenada()
    {
        List<Cliente> copia = new(heap);
        copia.Sort((a, b) =>
        {
            if (a.Prioridad != b.Prioridad)
                return a.Prioridad.CompareTo(b.Prioridad);
            int hora = a.HoraLlegada.CompareTo(b.HoraLlegada);
            return hora != 0 ? hora : a.OrdenLlegada.CompareTo(b.OrdenLlegada);
        });
        return copia;
    }
}
