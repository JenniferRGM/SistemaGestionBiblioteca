using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBiblioteca.Clases

{
    //Clase para publicacion
    public class Publicacion
    {
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string ISBN { get; set; }
        public int AñoPublicacion { get; set; }

        //Inicializar la publicación
        public Publicacion(string titulo, string autor, string isbn, int añoPublicacion)
        {
            Titulo = titulo;
            Autor = autor;
            ISBN = isbn;
            AñoPublicacion = añoPublicacion;
        }
        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Título: {Titulo}, Autor: {Autor}, ISBN: {ISBN}, Año: {AñoPublicacion}");

        }
    }
}
