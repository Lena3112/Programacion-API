using System;

class Producto
{
    public int Id { get; set; } //Encapsulamiento, en las clases Producto e Inventario, las propiedades y métodos encapsulan el comportamiento y los datos. 
    public string Nombre { get; set; } //Los datos (Id, Nombre, etc.) están dentro de la clase y solo se accede a ellos mediante métodos (MostrarDetalles(), EditarProducto(), etc.).
    public string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public int CantidadDisponible { get; set; }


    //Abstracción, las clases Producto e Inventario abstraen los conceptos del mundo real. 
    //Producto representa cualquier artículo con propiedades típicas como nombre, precio, etc.
    //Inventario representa una colección de productos con operaciones como agregar, editar, eliminar, mostrar.
    public Producto(int id, string nombre, string descripcion, decimal precio, int cantidadDisponible)
    {
        Id = id;
        Nombre = nombre;
        Descripcion = descripcion;
        Precio = precio;
        CantidadDisponible = cantidadDisponible;
    }

    public void MostrarDetalles()
    {
        Console.WriteLine($" ID: {Id},\n Nombre: {Nombre},\n Descripción: {Descripcion},\n Precio: {Precio:C},\n Cantidad disponible: {CantidadDisponible}");
    }
}

class Inventario
{
    private Producto[] productos;
    private int cantidadProductos;

    public Inventario(int capacidad)
    {
        productos = new Producto[capacidad];
        cantidadProductos = 0;
    }

    public void AgregarProducto(Producto producto)
    {
        if (cantidadProductos < productos.Length)
        {
            productos[cantidadProductos] = producto;
            cantidadProductos++;
        }
        else
        {
            Console.WriteLine("El inventario está lleno. No se puede agregar más productos.");
            //Polimorfismo, Permite usar un mismo método con comportamientos distintos dependiendo del contexto.
        }
    }

    public void EditarProducto(int id, string nuevoNombre, string nuevaDescripcion, decimal nuevoPrecio, int nuevaCantidad)
    {
        for (int i = 0; i < cantidadProductos; i++)
        {
            if (productos[i].Id == id)
            {
                productos[i].Nombre = nuevoNombre;
                productos[i].Descripcion = nuevaDescripcion;
                productos[i].Precio = nuevoPrecio;
                productos[i].CantidadDisponible = nuevaCantidad;
                Console.WriteLine("Producto editado correctamente.");
                return;
            }
        }
        Console.WriteLine("Producto no encontrado.");
    }

    public void EliminarProducto(int id)
    {
        for (int i = 0; i < cantidadProductos; i++)
        {
            if (productos[i].Id == id)
            {
                for (int j = i; j < cantidadProductos - 1; j++)
                {
                    productos[j] = productos[j + 1];
                }
                productos[cantidadProductos - 1] = null;
                cantidadProductos--;
                Console.WriteLine("Producto eliminado correctamente.");
                return;
            }
        }
        Console.WriteLine("Producto no encontrado.");
    }

    public void FiltrarPorId(int id)
    {
        for (int i = 0; i < cantidadProductos; i++)
        {
            if (productos[i].Id == id)
            {
                productos[i].MostrarDetalles();
                return;
            }
        }
        Console.WriteLine("Producto no encontrado.");
    }

    public void MostrarInventario()
    {
        if (cantidadProductos == 0)
        {
            Console.WriteLine("El inventario está vacío.");
            return;
        }

        Console.WriteLine("Productos en inventario:");
        for (int i = 0; i < cantidadProductos; i++)
        {
            productos[i].MostrarDetalles();
        }
    }
}

class Program
{
    static void Main()
    {
        Inventario inventario = new Inventario(5); // Inventario con capacidad de 5 productos

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Agregar producto");
            Console.WriteLine("2. Editar producto");
            Console.WriteLine("3. Eliminar producto");
            Console.WriteLine("4. Filtrar producto por ID");
            Console.WriteLine("5. Mostrar inventario");
            Console.WriteLine("6. Salir\n");
            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el ID del producto: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el nombre del producto: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingrese la descripción del producto: ");
                    string descripcion = Console.ReadLine();
                    Console.Write("Ingrese el precio del producto: ");
                    decimal precio = decimal.Parse(Console.ReadLine());
                    Console.Write("Ingrese la cantidad disponible: ");
                    int cantidad = int.Parse(Console.ReadLine());

                    Producto nuevoProducto = new Producto(id, nombre, descripcion, precio, cantidad);
                    inventario.AgregarProducto(nuevoProducto);
                    break;

                case "2":
                    Console.Write("Ingrese el ID del producto a editar: ");
                    int idEditar = int.Parse(Console.ReadLine());
                    Console.Write("Ingrese el nuevo nombre del producto: ");
                    string nuevoNombre = Console.ReadLine();
                    Console.Write("Ingrese la nueva descripción del producto: ");
                    string nuevaDescripcion = Console.ReadLine();
                    Console.Write("Ingrese el nuevo precio del producto: ");
                    decimal nuevoPrecio = decimal.Parse(Console.ReadLine());
                    Console.Write("Ingrese la nueva cantidad disponible: ");
                    int nuevaCantidad = int.Parse(Console.ReadLine());

                    inventario.EditarProducto(idEditar, nuevoNombre, nuevaDescripcion, nuevoPrecio, nuevaCantidad);
                    break;

                case "3":
                    Console.Write("Ingrese el ID del producto a eliminar: ");
                    int idEliminar = int.Parse(Console.ReadLine());
                    inventario.EliminarProducto(idEliminar);
                    break;

                case "4":
                    Console.Write("Ingrese el ID del producto a filtrar: ");
                    int idFiltrar = int.Parse(Console.ReadLine());
                    inventario.FiltrarPorId(idFiltrar);
                    break;

                case "5":
                    inventario.MostrarInventario();
                    break;

                case "6":
                    return;

                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }
}

