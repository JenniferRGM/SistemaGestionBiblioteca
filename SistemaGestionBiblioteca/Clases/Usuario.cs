using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBiblioteca.Clases
{
    //Clase que representa un usuario de la biblioteca
    public class Usuario
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int NumeroSocio { get; set; }

        //Inicializador de un usuario
        public Usuario(string nombre, string apellido, int numeroSocio)
        {
            Nombre = nombre;
            Apellido = apellido;
            NumeroSocio = numeroSocio;
        }
        //Muestra la informacion del usuario
        public void MostrarInformacion()
        {
            Console.WriteLine($"Nombre: {Nombre}, Apellido: {Apellido}, Número de Socio: {NumeroSocio}");
        }
    }
}
