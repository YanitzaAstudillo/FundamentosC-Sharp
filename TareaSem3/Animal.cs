
using System;

namespace Poo3
{
    abstract class Animal
    {
        private string nombre;
        private int edad;

        public int Edad
        {
            get { return edad; }
            set
            {
                if (value > 0)
                    edad = value;
                else
                    throw new ArgumentException("debe ser mayor que cero");
            }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public Animal(string nombre, int edad)
        {
            Nombre = nombre;
            Edad = edad;
        }

        public void SetNombre(string nuevoNombre)
        {
            Nombre = nuevoNombre;
        }

        public string GetNombre()
        {
            return Nombre;
        }

        public abstract void EmitirSonido();
    }
}