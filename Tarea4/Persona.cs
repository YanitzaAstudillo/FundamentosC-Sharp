
using System.Security.Cryptography;
using System.Text;

namespace Poo2
{
    public class Persona
    {
        private string Nombre; //ENCAPSULADO

        public int Edad { get; set; }
        private string Cedula;

        //CONSTRUCTOR SIN PARAMETROS
        public Persona()
        {
            this.Nombre= "Juan";
            this.Cedula = "000";
            
        }

        public Persona(string nombre, int edad, string cedula)
        {
            this.Nombre= nombre;
            this.Edad= edad;
            this.Cedula = cedula;
        }

        public void SetNombre(string nuevoNombre)  //ACCESO CONTROLADO//
        {
            this.Nombre= nuevoNombre;
        }

        public string GetNombre()
        {
            return this.Nombre;
        }

        public string GetCedula()
        {
            return this.Cedula;
        }

        public int GetEdad()
        {
            return this.Edad;
        }
    }
}