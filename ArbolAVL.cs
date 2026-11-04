// Árbol AVL usado para guardar, buscar, actualizar y eliminar productos.
namespace DonJuanMarket_Elias_Hernandez;

public class ArbolAVL
{
    private NodoAVL? raiz;

    private int ObtenerAltura(NodoAVL? nodo) => nodo?.Altura ?? 0;

    private void ActualizarAltura(NodoAVL nodo)
    {
        nodo.Altura = 1 + Math.Max(ObtenerAltura(nodo.Izquierdo), ObtenerAltura(nodo.Derecho));
    }

    private int FactorBalance(NodoAVL? nodo)
    {
        if (nodo == null) return 0;
        return ObtenerAltura(nodo.Izquierdo) - ObtenerAltura(nodo.Derecho);
    }

    // Rotación simple hacia la derecha para corregir un caso izquierda-izquierda.
    private NodoAVL RotacionDerecha(NodoAVL y)
    {
        NodoAVL x = y.Izquierdo!;
        NodoAVL? temporal = x.Derecho;

        x.Derecho = y;
        y.Izquierdo = temporal;

        ActualizarAltura(y);
        ActualizarAltura(x);
        return x;
    }

    // Rotación simple hacia la izquierda para corregir un caso derecha-derecha.
    private NodoAVL RotacionIzquierda(NodoAVL x)
    {
        NodoAVL y = x.Derecho!;
        NodoAVL? temporal = y.Izquierdo;

        y.Izquierdo = x;
        x.Derecho = temporal;

        ActualizarAltura(x);
        ActualizarAltura(y);
        return y;
    }

    // Revisa el factor de balance y aplica la rotación que corresponda.
    private NodoAVL Balancear(NodoAVL nodo)
    {
        ActualizarAltura(nodo);
        int balance = FactorBalance(nodo);

        if (balance > 1)
        {
            if (FactorBalance(nodo.Izquierdo) < 0)
                nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo!);

            return RotacionDerecha(nodo);
        }

        if (balance < -1)
        {
            if (FactorBalance(nodo.Derecho) > 0)
                nodo.Derecho = RotacionDerecha(nodo.Derecho!);

            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    public bool Insertar(Producto producto)
    {
        bool insertado = false;
        raiz = InsertarRecursivo(raiz, producto, ref insertado);
        return insertado;
    }

    private NodoAVL InsertarRecursivo(NodoAVL? nodo, Producto producto, ref bool insertado)
    {
        if (nodo == null)
        {
            insertado = true;
            return new NodoAVL(producto);
        }

        int comparacion = string.Compare(producto.Codigo, nodo.Producto.Codigo, StringComparison.OrdinalIgnoreCase);

        if (comparacion < 0)
            nodo.Izquierdo = InsertarRecursivo(nodo.Izquierdo, producto, ref insertado);
        else if (comparacion > 0)
            nodo.Derecho = InsertarRecursivo(nodo.Derecho, producto, ref insertado);
        else
            return nodo;

        return Balancear(nodo);
    }

    public Producto? Buscar(string codigo)
    {
        NodoAVL? actual = raiz;
        while (actual != null)
        {
            int comparacion = string.Compare(codigo, actual.Producto.Codigo, StringComparison.OrdinalIgnoreCase);
            if (comparacion == 0) return actual.Producto;
            actual = comparacion < 0 ? actual.Izquierdo : actual.Derecho;
        }
        return null;
    }

    public bool Actualizar(string codigo, string nombre, string categoria, double precio, int stock)
    {
        Producto? producto = Buscar(codigo);
        if (producto == null) return false;
        producto.ActualizarDatos(nombre, categoria, precio, stock);
        return true;
    }

    public bool Eliminar(string codigo)
    {
        bool eliminado = false;
        raiz = EliminarRecursivo(raiz, codigo, ref eliminado);
        return eliminado;
    }

    private NodoAVL? EliminarRecursivo(NodoAVL? nodo, string codigo, ref bool eliminado)
    {
        if (nodo == null) return null;

        int comparacion = string.Compare(codigo, nodo.Producto.Codigo, StringComparison.OrdinalIgnoreCase);

        if (comparacion < 0)
            nodo.Izquierdo = EliminarRecursivo(nodo.Izquierdo, codigo, ref eliminado);
        else if (comparacion > 0)
            nodo.Derecho = EliminarRecursivo(nodo.Derecho, codigo, ref eliminado);
        else
        {
            eliminado = true;

            // Caso de cero o un hijo.
            if (nodo.Izquierdo == null) return nodo.Derecho;
            if (nodo.Derecho == null) return nodo.Izquierdo;

            // Caso de dos hijos: usamos el sucesor más pequeño del lado derecho.
            NodoAVL sucesor = ObtenerMinimo(nodo.Derecho);
            nodo.Producto = sucesor.Producto;
            bool eliminoSucesor = false;
            nodo.Derecho = EliminarRecursivo(nodo.Derecho, sucesor.Producto.Codigo, ref eliminoSucesor);
        }

        return Balancear(nodo);
    }

    private NodoAVL ObtenerMinimo(NodoAVL nodo)
    {
        NodoAVL actual = nodo;
        while (actual.Izquierdo != null)
            actual = actual.Izquierdo;
        return actual;
    }

    public List<Producto> ObtenerEnOrden()
    {
        List<Producto> productos = new();
        RecorridoEnOrden(raiz, productos);
        return productos;
    }

    // El recorrido izquierda-nodo-derecha devuelve los códigos ordenados.
    private void RecorridoEnOrden(NodoAVL? nodo, List<Producto> productos)
    {
        if (nodo == null) return;
        RecorridoEnOrden(nodo.Izquierdo, productos);
        productos.Add(nodo.Producto);
        RecorridoEnOrden(nodo.Derecho, productos);
    }
}
