
using System.Security.Cryptography;
using System.Text;

namespace SistemaDeRegistro
{
    public class Persona
    {
    private string Nombre;
    public int Edad { get; set; }
    private string Cedula;

    public Persona(string nombre, int edad, string cedula)
        {
            this.Nombre= nombre;
            this.Edad= edad;
            this.Cedula = cedula;
        }

        public void SetNombre(string nuevoNombre)
        {
            this.Nombre= nuevoNombre;
        }

        public string GetNombre()
        {
            return this.Nombre;
        }

        public int GetEdad()
        {
            return this.Edad;
        }

        public string GetCedula()
        {
            return this.Cedula;
        }

    }
}

