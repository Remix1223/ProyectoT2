List<Plato> platos = new List<Plato>();
Dictionary<string, Categoria> categorias = new Dictionary<string, Categoria>();
int siguientePlato = 1;
int siguienteCategoria = 1;
string opcion = "";

while (opcion != "6")
{
    Console.WriteLine();
    Console.WriteLine("MENU DEL RESTAURANTE");
    Console.WriteLine("1. Agregar plato");
    Console.WriteLine("2. Buscar plato");
    Console.WriteLine("3. Modificar plato");
    Console.WriteLine("4. Eliminar plato");
    Console.WriteLine("5. Listar platos");
    Console.WriteLine("6. Salir");
    Console.Write("Opcion: ");
    opcion = (Console.ReadLine() ?? "").Trim();

    List<Plato>? mostrar = null;

    try
    {
        switch (opcion)
        {
            case "1":
                {
                    Console.Write("Nombre: ");
                    string nombre = (Console.ReadLine() ?? "").Trim();
                    Console.Write("Categoria: ");
                    string categoria = (Console.ReadLine() ?? "").Trim();
                    Console.Write("Precio: ");
                    decimal precio;
                    while (!decimal.TryParse(Console.ReadLine(), out precio))
                    {
                        Console.Write("Valor invalido, intente de nuevo: ");
                    }

                    if (nombre == "" || categoria == "")
                    {
                        throw new Exception("Los campos no pueden estar vacios.");
                    }

                    if (precio <= 0)
                    {
                        throw new Exception("El precio debe ser mayor que cero.");
                    }

                    string clave = categoria.ToLower();

                    if (!categorias.ContainsKey(clave))
                    {
                        categorias.Add(clave, new Categoria(siguienteCategoria, categoria));
                        siguienteCategoria++;
                    }

                    platos.Add(new Plato(siguientePlato, nombre, categorias[clave], new Precio(precio)));
                    siguientePlato++;
                    Console.WriteLine("Plato agregado.");
                    break;
                }
            case "2":
                {
                    Console.WriteLine("1. Por ID");
                    Console.WriteLine("2. Por nombre");
                    Console.WriteLine("3. Por categoria");
                    Console.WriteLine("4. Por rango de precio");
                    Console.Write("Buscar por: ");
                    string criterio = (Console.ReadLine() ?? "").Trim();

                    mostrar = new List<Plato>();

                    if (criterio == "1")
                    {
                        Console.Write("ID: ");
                        int idBuscado;
                        while (!int.TryParse(Console.ReadLine(), out idBuscado))
                        {
                            Console.Write("Numero invalido, intente de nuevo: ");
                        }

                        foreach (Plato p in platos)
                        {
                            if (p.Id == idBuscado)
                            {
                                mostrar.Add(p);
                            }
                        }
                    }
                    else if (criterio == "2")
                    {
                        Console.Write("Nombre: ");
                        string texto = (Console.ReadLine() ?? "").Trim().ToLower();

                        foreach (Plato p in platos)
                        {
                            if (p.Nombre.ToLower().Contains(texto))
                            {
                                mostrar.Add(p);
                            }
                        }
                    }
                    else if (criterio == "3")
                    {
                        Console.Write("Categoria: ");
                        string texto = (Console.ReadLine() ?? "").Trim().ToLower();

                        foreach (Plato p in platos)
                        {
                            if (p.Categoria.Nombre.ToLower().Contains(texto))
                            {
                                mostrar.Add(p);
                            }
                        }
                    }
                    else if (criterio == "4")
                    {
                        Console.Write("Precio minimo: ");
                        decimal minimo;
                        while (!decimal.TryParse(Console.ReadLine(), out minimo))
                        {
                            Console.Write("Valor invalido, intente de nuevo: ");
                        }

                        Console.Write("Precio maximo: ");
                        decimal maximo;
                        while (!decimal.TryParse(Console.ReadLine(), out maximo))
                        {
                            Console.Write("Valor invalido, intente de nuevo: ");
                        }

                        if (minimo > maximo)
                        {
                            throw new Exception("El precio minimo no puede ser mayor que el maximo.");
                        }

                        foreach (Plato p in platos)
                        {
                            if (p.Precio.Valor >= minimo && p.Precio.Valor <= maximo)
                            {
                                mostrar.Add(p);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opcion invalida.");
                        mostrar = null;
                    }
                    break;
                }
            case "3":
                {
                    Console.Write("ID del plato a modificar: ");
                    int id;
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("Numero invalido, intente de nuevo: ");
                    }

                    int indice = platos.FindIndex(x => x.Id == id);

                    if (indice == -1)
                    {
                        throw new Exception("No existe un plato con ese ID.");
                    }

                    Console.Write("Nuevo nombre: ");
                    string nombre = (Console.ReadLine() ?? "").Trim();
                    Console.Write("Nueva categoria: ");
                    string categoria = (Console.ReadLine() ?? "").Trim();
                    Console.Write("Nuevo precio: ");
                    decimal precio;
                    while (!decimal.TryParse(Console.ReadLine(), out precio))
                    {
                        Console.Write("Valor invalido, intente de nuevo: ");
                    }

                    if (nombre == "" || categoria == "")
                    {
                        throw new Exception("Los campos no pueden estar vacios.");
                    }

                    if (precio <= 0)
                    {
                        throw new Exception("El precio debe ser mayor que cero.");
                    }

                    string clave = categoria.ToLower();

                    if (!categorias.ContainsKey(clave))
                    {
                        categorias.Add(clave, new Categoria(siguienteCategoria, categoria));
                        siguienteCategoria++;
                    }

                    platos[indice] = platos[indice] with { Nombre = nombre, Categoria = categorias[clave], Precio = new Precio(precio) };
                    Console.WriteLine("Plato modificado.");
                    break;
                }
            case "4":
                {
                    Console.Write("ID del plato a eliminar: ");
                    int id;
                    while (!int.TryParse(Console.ReadLine(), out id))
                    {
                        Console.Write("Numero invalido, intente de nuevo: ");
                    }

                    int indice = platos.FindIndex(x => x.Id == id);

                    if (indice == -1)
                    {
                        throw new Exception("No existe un plato con ese ID.");
                    }

                    platos.RemoveAt(indice);
                    Console.WriteLine("Plato eliminado.");
                    break;
                }
            case "5":
                {
                    mostrar = platos;
                    break;
                }
            case "6":
                {
                    Console.WriteLine("Fin del programa.");
                    break;
                }
            default:
                {
                    Console.WriteLine("Opcion invalida.");
                    break;
                }
        }

        if (mostrar != null)
        {
            if (mostrar.Count == 0)
            {
                Console.WriteLine("No hay platos.");
            }

            foreach (Plato p in mostrar)
            {
                Console.WriteLine(p.Id + " - " + p.Nombre + " - " + p.Categoria.Nombre + " - " + p.Precio.Valor);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error: " + ex.Message);
    }
}