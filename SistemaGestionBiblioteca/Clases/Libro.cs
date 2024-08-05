using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaGestionBiblioteca.Interfaces;

namespace SistemaGestionBiblioteca.Clases
{
    //Clase derivada de publicacion
    public class Libro : Publicacion, IPrestable
    {
        public int NumeroPaginas { get; set; }

        public Libro (string titulo, string autor, string isbn, int añoPublicacion, int numeroPaginas)
           : base(titulo, autor, isbn, añoPublicacion)
        {
            NumeroPaginas = numeroPaginas;
        }

        //Metodo par mostras la informacion del libro
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Número de Páginas: {NumeroPaginas}");
        }
        //Interfaz de IPrestable
        public void Prestar()
        {
            Console.WriteLine($"El libro '{Titulo}' ha sido prestado.");
        }
        public void Devolver()
        {
            Console.WriteLine($"El libro '{Titulo}' ha sido devuelto.");
        }
    }
}
