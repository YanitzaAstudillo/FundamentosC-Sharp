
using System;
using System.Collections.Generic;

namespace Poo3
{
    class Pollo: Animal
    {
        public Pollo(string nombre, int edad) : base(nombre, edad) { }

        public override void EmitirSonido()
        {
            Console.WriteLine($"{Nombre} dice: pio");
        }
    }
}

