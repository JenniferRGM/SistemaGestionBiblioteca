using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBiblioteca.Clases
{
    //Clase derivada de publicacion representa a un DVD
    public class DVD : Publicacion
    {  
        public double Duracion { get; set; } // Duración en minutos

        //Inicializador de DVD
        public DVD(string titulo, string autor, string isbn, int añoPublicacion, double duracion)
            : base(titulo, autor, isbn, añoPublicacion)
        {
            Duracion = duracion;
        }
        //Muestra la info del DVD
        public override void MostrarInformacion()
        {
            base.MostrarInformacion(); 
            Console.WriteLine($"Duración: {Duracion} minutos");
        }
    }
}
