using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBiblioteca.Clases
{
    //Clase prestamo en la biblioteca
    public class Prestamo
    {
        public Libro LibroPrestado { get; set; }
        public Usuario Usuario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        //Inicializador de prestamo
        public Prestamo(Libro libroPrestado, Usuario usuario, DateTime fechaPrestamo, DateTime fechaDevolucion)
        {
            LibroPrestado = libroPrestado;
            Usuario = usuario;
            FechaPrestamo = fechaPrestamo;
            FechaDevolucion = fechaDevolucion;
        }
        //Muestra la info del prestamo
        public void MostrarInformacion()
        {
            Console.WriteLine($"Libro: {LibroPrestado.Titulo}, Usuario: {Usuario.Nombre} {Usuario.Apellido}, " +
                              $"Fecha de Préstamo: {FechaPrestamo.ToShortDateString()}, Fecha de Devolución: {FechaDevolucion.ToShortDateString()}");
        }
    }
}
