using System;

namespace Poo2
{
    public class EstudianteBecado : Estudiante
    {
        private string TipoBeca;

        public EstudianteBecado(string nombre, int edad, string cedula, string carrera, string tipoBeca)
            : base(nombre, edad, cedula, carrera)
        {
            this.TipoBeca = tipoBeca;
        }

        public override void MostrarInfo()  //POLIMORFISMO IMPLEMENTACION CONCRETA
        {
            Console.WriteLine($"Estudiante Becado: {GetNombre()}, Edad: {Edad},  Cedula: {GetCedula()}, Carrera: {Carrera}, Tipo de beca: {TipoBeca}");
        }
    }
}