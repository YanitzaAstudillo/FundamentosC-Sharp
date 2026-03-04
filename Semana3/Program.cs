
using System;

namespace Poo2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Persona persona1 = new Persona();
            Console.WriteLine(persona1.GetNombre());

            persona1.SetNombre("Maria");
            string nombrePersona1 = persona1.GetNombre();
            persona1.Edad = 23;
            Console.WriteLine("Modificando atributo Nombre...");
            Console.WriteLine(persona1.GetNombre());
            Console.WriteLine($"La Persona1 se llama {nombrePersona1}, y tiene {persona1.Edad} años");

            Persona persona2 = new Persona();
            persona2.SetNombre("Jose");
            string nombrePersona2 = persona2.GetNombre();
            persona2.Edad = 30;

            Console.WriteLine($"La Persona2 se llama {persona2.GetNombre()}, y tiene {persona2.Edad} años");
         
            /* Estudiante estudiante1 = new Estudiante("Ingenieria Informatica");
            estudiante1.SetNombre("Jeffry");
            estudiante1.Edad = 35;
            estudiante1.MostrarInfo(); */

        }
    }
}