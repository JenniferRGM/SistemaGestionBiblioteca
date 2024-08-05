using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBiblioteca.Clases
{
    //Clase derivada de publicacion representa a revista
    public class Revista : Publicacion
    {
        public int NumeroVolumenes { get; set; }

        //Inicializador de revista
        public Revista(string titulo, string autor, string isbn, int añoPublicacion, int numeroVolumenes)
            : base(titulo, autor, isbn, añoPublicacion)
        {
            NumeroVolumenes = numeroVolumenes;
        }
        //Muestra la informacion de revista
        public override void MostrarInformacion()
        {
            base.MostrarInformacion();
            Console.WriteLine($"Número de Volúmenes: {NumeroVolumenes}");
        }

    }
}
