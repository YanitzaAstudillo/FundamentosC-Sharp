
using System;
using Poo3;


namespace Poo3
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            List<Animal> animales = new List<Animal>();

            Animal animal1 = new Gato("Kiki", 4);
            Animal animal2 = new Perro("Nina", 10);
            Animal animal3 = new Pollo("Vaini", 2);

            animales.Add(animal1);
            animales.Add(animal2);
            animales.Add(animal3);

            foreach (Animal animal in animales)
            {
                Console.WriteLine($"El animal se llama {animal.GetNombre()} y tiene {animal.Edad} años");
                animal.EmitirSonido();
            }
        }
    }
}
