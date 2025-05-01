using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

// Clase que representa un estudiante 
class Estudiante // Aplicación de la POO: Clase
{
    // Propiedades públicas con getters y setters (Encapsulamiento)
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
    public string CorreoElectronico { get; set; }
}

// Clase que representa un curso
class Curso // Aplicación de la POO: Clase
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Creditos { get; set; }
}

// Clase que agrupa toda la lógica del sistema (Abstracción, Encapsulamiento)
class EscuelaApp
{
    // Cadena de conexión (Encapsulada dentro de la clase)
    private string connectionString = "server=localhost;user=root;password=;database=cursos";

    // Método para registrar un nuevo estudiante en la base de datos
    public void AgregarEstudiante(Estudiante e)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var query = "INSERT INTO estudiantes (nombre_completo, correo_electronico) VALUES (@nombre, @correo)";
        var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@nombre", e.NombreCompleto);
        cmd.Parameters.AddWithValue("@correo", e.CorreoElectronico);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Estudiante registrado.");
    }

    // Método para registrar un nuevo curso
    public void AgregarCurso(Curso c)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var query = "INSERT INTO cursos (nombre, descripcion, creditos) VALUES (@nombre, @desc, @cred)";
        var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@nombre", c.Nombre);
        cmd.Parameters.AddWithValue("@desc", c.Descripcion);
        cmd.Parameters.AddWithValue("@cred", c.Creditos);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Curso registrado.");
    }

    // Método para inscribir un estudiante en un curso
    public void InscribirEstudiante(int estudianteId, int cursoId)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var query = "INSERT INTO inscripciones (estudiante_id, curso_id) VALUES (@eid, @cid)";
        var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@eid", estudianteId);
        cmd.Parameters.AddWithValue("@cid", cursoId);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Inscripción realizada.");
    }

    // Método para listar todos los cursos disponibles
    public void ListarCursos()
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var query = "SELECT * FROM cursos";
        var cmd = new MySqlCommand(query, conn);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("Cursos disponibles:");
        while (reader.Read())
        {
            Console.WriteLine($" ID: {reader["Id"]},\n Nombre: {reader["Nombre"]},\n Créditos: {reader["Creditos"]}\n");
        }
    }

    // Método para listar los cursos en los que está inscrito un estudiante
    public void ListarCursosDeEstudiante(int estudianteId)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var query = @"SELECT c.nombre, c.descripcion FROM cursos c
                      JOIN inscripciones i ON c.id = i.curso_id
                      WHERE i.estudiante_id = @eid";
        var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@eid", estudianteId);
        using var reader = cmd.ExecuteReader();
        Console.WriteLine("Cursos del estudiante:");
        while (reader.Read())
        {
            Console.WriteLine($" Nombre: {reader["Nombre"]},\n Descripción: {reader["Descripcion"]}\n");
        }
    }

    // Método para eliminar la inscripción de un estudiante en un curso
    public void EliminarInscripcion(int estudianteId, int cursoId)
    {
        using var conn = new MySqlConnection(connectionString);
        conn.Open();
        var query = "DELETE FROM inscripciones WHERE estudiante_id = @eid AND curso_id = @cid";
        var cmd = new MySqlCommand(query, conn);
        cmd.Parameters.AddWithValue("@eid", estudianteId);
        cmd.Parameters.AddWithValue("@cid", cursoId);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Inscripción eliminada.");
    }
}

// Clase principal del programa
class Program
{
    static void Main()
    {
        var app = new EscuelaApp(); // Objeto que accede a los métodos de negocio (Objeto de clase)

        while (true) // Bucle principal con menú
        {
            Console.WriteLine("\n--- MENÚ ---");
            Console.WriteLine("1. Registrar estudiante");
            Console.WriteLine("2. Registrar curso");
            Console.WriteLine("3. Inscribir estudiante en curso");
            Console.WriteLine("4. Listar cursos");
            Console.WriteLine("5. Listar cursos de un estudiante");
            Console.WriteLine("6. Eliminar inscripción");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: ");
            var opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    var est = new Estudiante(); // Creación de objeto (Instancia de clase)
                    Console.Write("Nombre completo: "); est.NombreCompleto = Console.ReadLine();
                    Console.Write("Correo: "); est.CorreoElectronico = Console.ReadLine();
                    app.AgregarEstudiante(est); // Uso de método (Abstracción)
                    break;

                case "2":
                    var curso = new Curso(); // Creación de objeto
                    Console.Write("Nombre: "); curso.Nombre = Console.ReadLine();
                    Console.Write("Descripción: "); curso.Descripcion = Console.ReadLine();
                    Console.Write("Créditos: "); curso.Creditos = int.Parse(Console.ReadLine());
                    app.AgregarCurso(curso);
                    break;

                case "3":
                    Console.Write("ID estudiante: "); int eid = int.Parse(Console.ReadLine());
                    Console.Write("ID curso: "); int cid = int.Parse(Console.ReadLine());
                    app.InscribirEstudiante(eid, cid);
                    break;

                case "4":
                    app.ListarCursos();
                    break;

                case "5":
                    Console.Write("ID del estudiante: "); int idEst = int.Parse(Console.ReadLine());
                    app.ListarCursosDeEstudiante(idEst);
                    break;

                case "6":
                    Console.Write("ID estudiante: "); int eId = int.Parse(Console.ReadLine());
                    Console.Write("ID curso: "); int cId = int.Parse(Console.ReadLine());
                    app.EliminarInscripcion(eId, cId);
                    break;

                case "7":
                    return; // Finaliza la aplicación
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
        }
    }
}
