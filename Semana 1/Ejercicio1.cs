

using System;

class Program
{
    static void Main()
    {
        // Solicitar nombre//
        Console.Write("Ingresa tu nombre: ");
        string nombre = Console.ReadLine();

        // Solicitar edad solo números con el parse//
        Console.Write("Ingresa tu edad: ");
        int edad = int.Parse(Console.ReadLine());


        // Mostrar mensaje con nombre y edad//
        Console.WriteLine($"Hola {nombre}, tienes {edad} años");

        // Calcular edad dentro de 5 años con operador aritmetico//
        int edadFutura = edad + 5;
        Console.WriteLine($"Dentro de 5 años tendrás {edadFutura} años");

        // Estructura condicional del if//
        if (edad >= 18)
        {
            Console.WriteLine("Eres mayor de edad");
        }
        else
        {
            Console.WriteLine("Eres menor de edad");
        }
    }
}