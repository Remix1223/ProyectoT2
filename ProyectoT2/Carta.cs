public class Carta
{
    private List<Plato> platos = new List<Plato>();
    private Dictionary<string, Categoria> categorias = new Dictionary<string, Categoria>();
    private int idPlato = 1;
    private int idCategoria = 1;

    private Categoria ObtenerCategoria(string nombre)
    {
        string clave = nombre.ToLower();

        if (!categorias.ContainsKey(clave))
        {
            categorias.Add(clave, new Categoria(idCategoria, nombre));
            idCategoria++;
        }

        return categorias[clave];
    }

    public void Agregar(string nombre, string categoria, decimal precio)
    {
        if (nombre == "" || categoria == "")
        {
            throw new Exception("Los campos no pueden estar vacios.");
        }

        if (precio <= 0)
        {
            throw new Exception("El precio debe ser mayor que cero.");
        }

        platos.Add(new Plato(idPlato, nombre, ObtenerCategoria(categoria), new Precio(precio)));
        idPlato++;
    }

    public List<Plato> Listar()
    {
        return platos;
    }

    public Plato? BuscarPorId(int id)
    {
        foreach (Plato p in platos)
        {
            if (p.Id == id)
            {
                return p;
            }
        }

        return null;
    }

    public List<Plato> BuscarPorNombre(string texto)
    {
        List<Plato> resultado = new List<Plato>();

        foreach (Plato p in platos)
        {
            if (p.Nombre.ToLower().Contains(texto.ToLower()))
            {
                resultado.Add(p);
            }
        }

        return resultado;
    }

    public List<Plato> BuscarPorCategoria(string texto)
    {
        List<Plato> resultado = new List<Plato>();

        foreach (Plato p in platos)
        {
            if (p.Categoria.Nombre.ToLower().Contains(texto.ToLower()))
            {
                resultado.Add(p);
            }
        }

        return resultado;
    }

    public List<Plato> BuscarPorPrecio(decimal minimo, decimal maximo)
    {
        if (minimo > maximo)
        {
            throw new Exception("El precio minimo no puede ser mayor que el maximo.");
        }

        List<Plato> resultado = new List<Plato>();

        foreach (Plato p in platos)
        {
            if (p.Precio.Valor >= minimo && p.Precio.Valor <= maximo)
            {
                resultado.Add(p);
            }
        }

        return resultado;
    }

    public void Modificar(int id, string nombre, string categoria, decimal precio)
    {
        Plato? plato = BuscarPorId(id);

        if (plato == null)
        {
            throw new Exception("No existe un plato con ese ID.");
        }

        if (nombre == "" || categoria == "")
        {
            throw new Exception("Los campos no pueden estar vacios.");
        }

        if (precio <= 0)
        {
            throw new Exception("El precio debe ser mayor que cero.");
        }

        plato.Nombre = nombre;
        plato.Categoria = ObtenerCategoria(categoria);
        plato.Precio = new Precio(precio);
    }

    public void Eliminar(int id)
    {
        Plato? plato = BuscarPorId(id);

        if (plato == null)
        {
            throw new Exception("No existe un plato con ese ID.");
        }

        platos.Remove(plato);
    }
}