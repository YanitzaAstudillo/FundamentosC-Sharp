
using System;
using System.Collections.Generic;


namespace Poo3
{
    class Perro: Animal
    {
        public Perro(string nombre, int edad) : base(nombre, edad) { }

        public override void EmitirSonido()
        {
            Console.WriteLine($"{Nombre} dice: woff");
        }
    }
}
