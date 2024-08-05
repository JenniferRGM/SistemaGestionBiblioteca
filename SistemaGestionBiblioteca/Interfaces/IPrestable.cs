using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionBiblioteca.Interfaces
{
    public interface IPrestable
    {
        void Prestar();
        void Devolver();
    }
}
