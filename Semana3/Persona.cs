
using System.Security.Cryptography;
using System.Text;

namespace Poo2
{
    public class Persona
    {
        private string Nombre;

        public int Edad { get; set; }

        //CONSTRUCTOR SIN PARAMETROS
        public Persona()
        {
            this.Nombre= "Juan";
        }

        public Persona(string nombre, int edad)
        {
            this.Nombre= nombre;
            this.Edad= edad;
        }

        public void SetNombre(string nuevoNombre)
        {
            this.Nombre= nuevoNombre;
        }

        public string GetNombre()
        {
            return this.Nombre;
        }
    }
}