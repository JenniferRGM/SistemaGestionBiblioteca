using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionBiblioteca.Clases;

namespace SistemaGestionBiblioteca
{
    // Programa principal para interactuar con el sistema de la biblioteca
    class Program
    {
        static void Main(string[] args)
        {
            var biblioteca = new Biblioteca();

            // Menú
            while (true)
            {
                Console.WriteLine("\nSistema de Gestión de Biblioteca");
                Console.WriteLine("1. Agregar Libro");
                Console.WriteLine("2. Modificar Libro");
                Console.WriteLine("3. Eliminar Libro");
                Console.WriteLine("4. Buscar Libro por Título");
                Console.WriteLine("5. Buscar Libro por Autor");
                Console.WriteLine("6. Buscar Libro por ISBN");
                Console.WriteLine("7. Mostrar Todos los Libros");
                Console.WriteLine("8. Prestar Libro");
                Console.WriteLine("9. Devolver Libro");
                Console.WriteLine("10. Agregar Usuario");
                Console.WriteLine("11. Modificar Usuario");
                Console.WriteLine("12. Eliminar Usuario");
                Console.WriteLine("13. Salir");
                Console.Write("Seleccione una opción: ");

                var opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1": // Agregar Libro
                        Console.Write("Ingrese el título del libro: ");
                        var titulo = Console.ReadLine();
                        Console.Write("Ingrese el autor del libro: ");
                        var autor = Console.ReadLine();
                        Console.Write("Ingrese el ISBN del libro: ");
                        var isbn = Console.ReadLine();
                        Console.Write("Ingrese el año de publicación: ");
                        var año = int.Parse(Console.ReadLine());
                        Console.Write("Ingrese el número de páginas: ");
                        var paginas = int.Parse(Console.ReadLine());

                        var nuevoLibro = new Libro(titulo, autor, isbn, año, paginas);
                        biblioteca.AgregarLibro(nuevoLibro);
                        break;

                    case "2": // Modificar Libro
                        Console.Write("Ingrese el ISBN del libro a modificar: ");
                        var isbnModificar = Console.ReadLine();
                        Console.Write("Ingrese el nuevo título del libro: ");
                        var nuevoTitulo = Console.ReadLine();
                        Console.Write("Ingrese el nuevo autor del libro: ");
                        var nuevoAutor = Console.ReadLine();
                        Console.Write("Ingrese el nuevo año de publicación: ");
                        var nuevoAnio = int.Parse(Console.ReadLine());
                        Console.Write("Ingrese el nuevo número de páginas: ");
                        var nuevasPaginas = int.Parse(Console.ReadLine());

                        biblioteca.ModificarLibro(isbnModificar, nuevoTitulo, nuevoAutor, nuevoAnio, nuevasPaginas);
                        break;

                    case "3": // Eliminar Libro
                        Console.Write("Ingrese el ISBN del libro a eliminar: ");
                        var isbnEliminar = Console.ReadLine();
                        biblioteca.EliminarLibro(isbnEliminar);
                        break;

                    case "4": // Buscar Libro por Título
                        Console.Write("Ingrese el título del libro a buscar: ");
                        var buscarTitulo = Console.ReadLine();

                        var libroEncontrado = biblioteca.BuscarLibroPorTitulo(buscarTitulo);
                        if (libroEncontrado != null)
                        {
                            libroEncontrado.MostrarInformacion();
                        }
                        else
                        {
                            Console.WriteLine("Libro no encontrado.");
                        }
                        break;

                    case "5": // Buscar Libro por Autor
                        Console.Write("Ingrese el autor del libro a buscar: ");
                        var buscarAutor = Console.ReadLine();

                        var librosPorAutor = biblioteca.BuscarLibroPorAutor(buscarAutor);
                        if (librosPorAutor.Count > 0)
                        {
                            Console.WriteLine("Libros encontrados:");
                            foreach (var libro in librosPorAutor)
                            {
                                libro.MostrarInformacion();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron libros para ese autor.");
                        }
                        break;

                    case "6": // Buscar Libro por ISBN
                        Console.Write("Ingrese el ISBN del libro a buscar: ");
                        var buscarISBN = Console.ReadLine();

                        var libroPorISBN = biblioteca.BuscarLibroPorISBN(buscarISBN);
                        if (libroPorISBN != null)
                        {
                            libroPorISBN.MostrarInformacion();
                        }
                        else
                        {
                            Console.WriteLine("Libro no encontrado.");
                        }
                        break;

                    case "7": // Mostrar Todos los Libros
                        biblioteca.MostrarTodosLosLibros();
                        break;

                    case "8": // Prestar Libro
                        Console.Write("Ingrese el título del libro a prestar: ");
                        var tituloPrestamo = Console.ReadLine();
                        Console.Write("Ingrese el número de socio del usuario: ");
                        var numeroSocioPrestamo = int.Parse(Console.ReadLine());

                        biblioteca.PrestarLibro(tituloPrestamo, numeroSocioPrestamo);
                        break;

                    case "9": // Devolver Libro
                        Console.Write("Ingrese el título del libro a devolver: ");
                        var tituloDevolucion = Console.ReadLine();
                        Console.Write("Ingrese el número de socio del usuario: ");
                        var numeroSocioDevolucion = int.Parse(Console.ReadLine());

                        biblioteca.DevolverLibro(tituloDevolucion, numeroSocioDevolucion);
                        break;

                    case "10": // Agregar Usuario
                        Console.Write("Ingrese el nombre del usuario: ");
                        var nombreUsuario = Console.ReadLine();
                        Console.Write("Ingrese el apellido del usuario: ");
                        var apellidoUsuario = Console.ReadLine();
                        Console.Write("Ingrese el número de socio: ");
                        var numeroSocio = int.Parse(Console.ReadLine());

                        var nuevoUsuario = new Usuario(nombreUsuario, apellidoUsuario, numeroSocio);
                        biblioteca.AgregarUsuario(nuevoUsuario);
                        break;

                    case "11": // Modificar Usuario
                        Console.Write("Ingrese el número de socio del usuario a modificar: ");
                        var numeroSocioModificar = int.Parse(Console.ReadLine());
                        Console.Write("Ingrese el nuevo nombre del usuario: ");
                        var nuevoNombreUsuario = Console.ReadLine();
                        Console.Write("Ingrese el nuevo apellido del usuario: ");
                        var nuevoApellidoUsuario = Console.ReadLine();

                        biblioteca.ModificarUsuario(numeroSocioModificar, nuevoNombreUsuario, nuevoApellidoUsuario);
                        break;

                    case "12": // Eliminar Usuario
                        Console.Write("Ingrese el número de socio del usuario a eliminar: ");
                        var numeroSocioEliminar = int.Parse(Console.ReadLine());
                        biblioteca.EliminarUsuario(numeroSocioEliminar);
                        break;

                    case "13": // Salir
                        Console.WriteLine("Gracias por usar el Sistema de Gestión de Biblioteca.");
                        return;

                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            }
        }
    }
}

