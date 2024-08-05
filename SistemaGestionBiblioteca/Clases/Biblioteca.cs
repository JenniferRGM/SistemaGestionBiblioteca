using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SistemaGestionBiblioteca.Clases
{
    // Clase principal que representa la biblioteca
    public class Biblioteca
    {
        private List<Libro> libros;
        private List<Usuario> usuarios;
        private List<Prestamo> prestamos;

        // Inicializa las listas y carga los datos de la BD
        public Biblioteca()
        {
            libros = new List<Libro>();
            usuarios = new List<Usuario>();
            prestamos = new List<Prestamo>();

            // Cargar datos iniciales desde la base de datos
            CargarLibrosDesdeBaseDeDatos();
            CargarUsuariosDesdeBaseDeDatos();
        }

        // Método para agregar un libro
        public void AgregarLibro(Libro libro)
        {
            libros.Add(libro);
            Console.WriteLine($"Libro '{libro.Titulo}' agregado a la biblioteca.");

            // Insertar en la base de datos
            InsertarLibroEnBaseDeDatos(libro);
        }

        // Método para buscar libro por título
        public Libro BuscarLibroPorTitulo(string titulo)
        {
            return libros.Find(libro => libro.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase));
        }
        //Metodo para buscar libro por autor
        public List<Libro> BuscarLibroPorAutor(string autor)
        {
            return libros.FindAll(libro => libro.Autor.Equals(autor, StringComparison.OrdinalIgnoreCase));
        }
        //Metodo para buscar libro por ISBN
        public Libro BuscarLibroPorISBN(string isbn)
        {
            return libros.Find(libro => libro.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
        }

        // Método para mostrar todos los libros
        public void MostrarTodosLosLibros()
        {
            Console.WriteLine("Lista de Libros en la Biblioteca:");
            foreach (var libro in libros)
            {
                libro.MostrarInformacion();
            }
        }

        // Método para prestar libro
        public void PrestarLibro(string titulo, int numeroSocio)
        {
            var libro = BuscarLibroPorTitulo(titulo);
            if (libro == null)
            {
                Console.WriteLine("Libro no encontrado.");
                return;
            }

            var usuario = usuarios.Find(u => u.NumeroSocio == numeroSocio);
            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            var prestamo = new Prestamo(libro, usuario, DateTime.Now, DateTime.Now.AddDays(14));
            prestamos.Add(prestamo);
            libro.Prestar();

            // Insertar préstamo en la BD
            InsertarPrestamoEnBaseDeDatos(prestamo);
        }

        // Método para devolver libro
        public void DevolverLibro(string titulo, int numeroSocio)
        {
            var prestamo = prestamos.Find(p => p.LibroPrestado.Titulo.Equals(titulo, StringComparison.OrdinalIgnoreCase)
                                             && p.Usuario.NumeroSocio == numeroSocio);

            if (prestamo == null)
            {
                Console.WriteLine("Préstamo no encontrado.");
                return;
            }

            prestamos.Remove(prestamo);
            prestamo.LibroPrestado.Devolver();

            // Actualizar la devolución en la BD
            EliminarPrestamoDeBaseDeDatos(prestamo);
        }

        // Conexión con la BD
        private const string connectionString = "Data Source=DESKTOP-TQUE363\\SQLEXPRESS;Initial Catalog=BibliotecaBD;Integrated Security=True";

        // Método para cargar los libros desde la BD
        private void CargarLibrosDesdeBaseDeDatos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM Libros", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var libro = new Libro(
                                reader["Titulo"].ToString(),
                                reader["Autor"].ToString(),
                                reader["ISBN"].ToString(),
                                Convert.ToInt32(reader["AñoPublicacion"]),
                                Convert.ToInt32(reader["NumeroPaginas"])
                            );
                            libros.Add(libro);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                }
            }
        }
        //Metodo para agregar usuarios

        public void AgregarUsuario(Usuario usuario)
        {
            usuarios.Add(usuario);
            Console.WriteLine($"Usuario '{usuario.Nombre} {usuario.Apellido}' agregado.");
            InsertarUsuarioEnBaseDeDatos(usuario);
        }
        //Metodo para modificar usuarios

        public void ModificarUsuario(int numeroSocio, string nuevoNombre, string nuevoApellido)
        {
            var usuario = usuarios.Find(u => u.NumeroSocio == numeroSocio);
            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            usuario.Nombre = nuevoNombre;
            usuario.Apellido = nuevoApellido;
            Console.WriteLine($"Usuario '{numeroSocio}' modificado.");
            ActualizarUsuarioEnBaseDeDatos(usuario);
        }
        //Metodo para eliminar usuarios

        public void EliminarUsuario(int numeroSocio)
        {
            var usuario = usuarios.Find(u => u.NumeroSocio == numeroSocio);
            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            usuarios.Remove(usuario);
            Console.WriteLine($"Usuario '{usuario.Nombre} {usuario.Apellido}' eliminado.");
            EliminarUsuarioDeBaseDeDatos(numeroSocio);
        }
        // Método para insertar un usuario en la BD
        private void InsertarUsuarioEnBaseDeDatos(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Usuarios (Nombre, Apellido, NumeroSocio) VALUES (@Nombre, @Apellido, @NumeroSocio)", connection);

                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@NumeroSocio", usuario.NumeroSocio);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar el usuario en la base de datos: " + ex.Message);
                }
            }
        }

        // Método para actualizar un usuario en la BD
        private void ActualizarUsuarioEnBaseDeDatos(Usuario usuario)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido WHERE NumeroSocio = @NumeroSocio", connection);

                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                    command.Parameters.AddWithValue("@NumeroSocio", usuario.NumeroSocio);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar el usuario en la base de datos: " + ex.Message);
                }
            }
        }

        // Método para eliminar un usuario en la BD
        private void EliminarUsuarioDeBaseDeDatos(int numeroSocio)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Usuarios WHERE NumeroSocio = @NumeroSocio", connection);

                    command.Parameters.AddWithValue("@NumeroSocio", numeroSocio);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar el usuario de la base de datos: " + ex.Message);
                }
            }
        }

        // Método para cargar usuarios desde la BD
        private void CargarUsuariosDesdeBaseDeDatos()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT * FROM Usuarios", connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario(
                                reader["Nombre"].ToString(),
                                reader["Apellido"].ToString(),
                                Convert.ToInt32(reader["NumeroSocio"])
                            );
                            usuarios.Add(usuario);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al conectar con la base de datos: " + ex.Message);
                }
            }
        }

        // Método para agregar un libro en la BD
        private void InsertarLibroEnBaseDeDatos(Libro libro)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Libros (Titulo, Autor, ISBN, AñoPublicacion, NumeroPaginas) " +
                                                 "VALUES (@Titulo, @Autor, @ISBN, @AñoPublicacion, @NumeroPaginas)", connection);

                    command.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    command.Parameters.AddWithValue("@Autor", libro.Autor);
                    command.Parameters.AddWithValue("@ISBN", libro.ISBN);
                    command.Parameters.AddWithValue("@AñoPublicacion", libro.AñoPublicacion);
                    command.Parameters.AddWithValue("@NumeroPaginas", libro.NumeroPaginas);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar el libro en la base de datos: " + ex.Message);
                }
            }
        }

        public void ModificarLibro(string isbn, string nuevoTitulo, string nuevoAutor, int nuevoAnio, int nuevasPaginas)
        {
            var libro = libros.Find(l => l.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
            if (libro == null)
            {
                Console.WriteLine("Libro no encontrado.");
                return;
            }

            libro.Titulo = nuevoTitulo;
            libro.Autor = nuevoAutor;
            libro.AñoPublicacion = nuevoAnio;
            libro.NumeroPaginas = nuevasPaginas;
            Console.WriteLine($"Libro '{isbn}' modificado.");

            // Actualizar en la base de datos
            ActualizarLibroEnBaseDeDatos(libro);
        }
        public void EliminarLibro(string isbn)
        {
            var libro = libros.Find(l => l.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
            if (libro == null)
            {
                Console.WriteLine("Libro no encontrado.");
                return;
            }

            libros.Remove(libro);
            Console.WriteLine($"Libro '{libro.Titulo}' eliminado de la biblioteca.");

            // Eliminar de la base de datos
            EliminarLibroDeBaseDeDatos(isbn);
        }
        // Método para actualizar un libro en la BD
        private void ActualizarLibroEnBaseDeDatos(Libro libro)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("UPDATE Libros SET Titulo = @Titulo, Autor = @Autor, AñoPublicacion = @AñoPublicacion, NumeroPaginas = @NumeroPaginas WHERE ISBN = @ISBN", connection);

                    command.Parameters.AddWithValue("@Titulo", libro.Titulo);
                    command.Parameters.AddWithValue("@Autor", libro.Autor);
                    command.Parameters.AddWithValue("@AñoPublicacion", libro.AñoPublicacion);
                    command.Parameters.AddWithValue("@NumeroPaginas", libro.NumeroPaginas);
                    command.Parameters.AddWithValue("@ISBN", libro.ISBN);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al actualizar el libro en la base de datos: " + ex.Message);
                }
            }
        }

        // Método para eliminar un libro en la BD
        private void EliminarLibroDeBaseDeDatos(string isbn)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Libros WHERE ISBN = @ISBN", connection);

                    command.Parameters.AddWithValue("@ISBN", isbn);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar el libro de la base de datos: " + ex.Message);
                }
            }
        }


        // Método para agregar el préstamo en la BD
        private void InsertarPrestamoEnBaseDeDatos(Prestamo prestamo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("INSERT INTO Prestamos (ISBN, NumeroSocio, FechaPrestamo, FechaDevolucion) " +
                                                 "VALUES (@ISBN, @NumeroSocio, @FechaPrestamo, @FechaDevolucion)", connection);

                    command.Parameters.AddWithValue("@ISBN", prestamo.LibroPrestado.ISBN);
                    command.Parameters.AddWithValue("@NumeroSocio", prestamo.Usuario.NumeroSocio);
                    command.Parameters.AddWithValue("@FechaPrestamo", prestamo.FechaPrestamo);
                    command.Parameters.AddWithValue("@FechaDevolucion", prestamo.FechaDevolucion);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al insertar el préstamo en la base de datos: " + ex.Message);
                }
            }
        }

        // Método para eliminar un préstamo en la BD
        private void EliminarPrestamoDeBaseDeDatos(Prestamo prestamo)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    var command = new SqlCommand("DELETE FROM Prestamos WHERE ISBN = @ISBN AND NumeroSocio = @NumeroSocio", connection);

                    command.Parameters.AddWithValue("@ISBN", prestamo.LibroPrestado.ISBN);
                    command.Parameters.AddWithValue("@NumeroSocio", prestamo.Usuario.NumeroSocio);

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al eliminar el préstamo de la base de datos: " + ex.Message);
                }
            }
        }
    }
}
