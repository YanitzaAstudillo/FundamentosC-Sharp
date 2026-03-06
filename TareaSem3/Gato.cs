using System;
using System.Collections.Generic;

namespace Poo3
{
    class Gato : Animal
    {
        public Gato(string nombre, int edad) : base(nombre, edad) { }

        public override void EmitirSonido()
        {
            Console.WriteLine($"{Nombre} dice: miau");
        }
    }
}
