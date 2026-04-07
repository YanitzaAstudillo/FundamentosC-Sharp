
using System;

namespace SistemaDeRegistro
{
    public class Paciente : Persona
    {
        public Paciente(string nombre, int edad, string cedula, string estadoCivil)
            : base(nombre, edad, cedula, estadoCivil)
        {
        }
    

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Paciente: {this.GetNombre()}, Edad: {this.Edad}, Cedula: {this.GetCedula()}, EstadoCivil: {this.GetEstadoCivil}");
        }
    }
}
