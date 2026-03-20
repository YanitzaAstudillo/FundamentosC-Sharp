
using System;

namespace Poo2
{
    public class Estudiante : Persona
    {
        protected string Carrera;

        public Estudiante(string nombre, int edad, string cedula, string carrera) : base(nombre, edad, cedula)
        {
            this.Carrera = carrera;
        }

        public virtual void MostrarInfo()  //POLIMORFISMO ACCESIBLE MODIFICABLE X CLASES DERIVADAS
        {
            Console.WriteLine($"Estudiante: {this.GetNombre()}, Edad: {this.Edad}, Cedula: {this.GetCedula()}, Carrera: {this.Carrera}");
        }
    }
}