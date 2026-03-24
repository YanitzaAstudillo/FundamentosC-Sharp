
using System;

namespace SistemaDeRegistro
{
    public class Paciente : Persona
    {
        protected string EstadoCivil;

        public Paciente(string nombre, int edad, string cedula, string estadocivil) : base(nombre, edad, cedula)
        {
            this.EstadoCivil = estadocivil;
        }

        public virtual void MostrarInformacion()
        {
            Console.WriteLine($"Paciente: {this.GetNombre()}, Edad: {this.Edad}, Cedula: {this.GetCedula()}, Carrera: {this.EstadoCivil}");
        }
    }
}
