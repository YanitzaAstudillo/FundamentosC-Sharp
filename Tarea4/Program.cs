
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;

namespace Poo2
{
    public class Program
    {
        //COLECCIONES EN PROGRAMACION DE OBJ//
        static List<Persona> personas = new List<Persona>();
        static Queue<Persona> ColaAtencion= new Queue<Persona>();

        static Stack<Persona> PilaAtencion= new Stack<Persona>();
        
        static void Main(string[] args)
        {
            
            /* Persona persona1 = new Persona();
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
          */
            /* Estudiante estudiante1 = new Estudiante("Ingenieria Informatica");
            estudiante1.SetNombre("Jeffry");
            estudiante1.Edad = 35;
            estudiante1.MostrarInfo(); */

        //LOGICA BOOLEANA EN COMPROBACION LOGIC Y ESTRUCTURA DE CONTROL C/CONDICION FALSA ANTES DE EJECUTAR EL CODIGO
            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n ------- MENU DEL ESTUDIANTE -----");
                Console.WriteLine("1. AGREGAR ESTUDIANTE");
                Console.WriteLine("2. LISTAR ESTUDIANTES ACTUALES");
                Console.WriteLine("3. BUSCAR ESTUDIANTE POR CEDULA");
                Console.WriteLine("4. FILTRAR ESTUDIANTE POR EDAD");
                Console.WriteLine("5. ATENDER ESTUDIANTE");
                Console.WriteLine("6. REGISTRAR ESTUDIANTE ATENDIDO");
                Console.WriteLine("7. SALIR");
                Console.WriteLine("INGRESE UNA OPCION: ");

            //SWICHT PARA CREACION DEL MENU CON ACEPTACION INT Y STRINGS DE LOS DIFERENTES CASOS Y BREAK PARA TERMINAR SU EJEC
                string opcion = Console.ReadLine() ?? "";
                switch(opcion)
                {
                    case "1": AgregarEstudiante(); break;
                    case "2": ListarEstudiante(); break;
                    case "3":
                    Console.WriteLine("Ingrese la cedula a buscar:");
                    string cedula = Console.ReadLine() ?? "";
                    BuscarPorCedula(cedula);
                    break;
                    case "4":
                    Console.WriteLine("Ingrese la edad:");
                    int edad = int.Parse(Console.ReadLine() ?? "0");
                    FiltrarPorEdad(edad);
                    break;
                    case "5": AtenderEstudiante();break;
                    case "6": RegistrarAtendido();break;
                    case "7": salir= true; break;
                    default: Console.WriteLine("Opción no válida. Intente de nuevo"); break;
                }

            }

        }

        //LECTURAS DE TEXTO Y NUMERO DESDE SU ENTRADA ?? COMPROBANDO SU NULIDAD "" Y EVITANDO VALORES INVALIDOS !int
            public static void AgregarEstudiante()
            {
                Console.WriteLine("Ingrese si es estudiante becado (s/n): ");
                string EsBecado = (Console.ReadLine() ?? "").ToLower();
                Console.WriteLine("Ingrese el nombre del estudiante: ");
                string nombre= Console.ReadLine()!;
                Console.WriteLine("Ingrese la edad del estudiante: ");
                if (!int.TryParse(Console.ReadLine(), out int edad))
                {
                    Console.WriteLine("Edad no válida");
                    return;
                }
                Console.WriteLine("Ingrese la cedula del estudiante: ");
                string cedula = Console.ReadLine();
                Console.WriteLine("Ingrese la carrera del estudiante: ");
                string carrera = Console.ReadLine();

                Persona nueva;
                if (EsBecado == "s")
                {
                    Console.WriteLine("Ingrese el tipo de beca:");
                    string tipoBeca = Console.ReadLine();

                    nueva= new EstudianteBecado(nombre, edad, cedula, carrera, tipoBeca);
                }
                else
                {
                    nueva= new Estudiante (nombre, edad, cedula, carrera);
                }
                personas.Add(nueva);
                ColaAtencion.Enqueue(nueva);
                PilaAtencion.Push(nueva);
                Console.WriteLine("Estudiante agregado correctamente");
            }

            //METODO DE CLASE PARA OBTENER LOS GET EN LINQ PARA SU FILTRADO EN LA COLECCION. FOREACH PARA RECORRER C/ELEMENTO DEL LISTADO//
            public static void ListarEstudiante()
            {
                foreach (Persona p in personas)
                {
                    Console.WriteLine($"{p.GetNombre()}, {p.GetEdad()}, {p.GetCedula()}");
                }
            }

            public static void BuscarPorCedula(string cedula)
            {
                foreach (Persona p in personas)
                {
                    if (p.GetCedula()==cedula)
                    
                    Console.WriteLine(p.GetNombre());
                    
                }
            }

            //METODO WHERE DEL LINQ PARA FILTRAR ELEMENTOS DE LA COLECCION PERSONAS C/LAMBDA//
            public static void FiltrarPorEdad(int edadMinima)
            {
                var resultado = personas
                                .Where(p => p.GetEdad() > edadMinima);

                foreach (var p in resultado)
                {
                    Console.WriteLine($"{p.GetNombre()}, {p.GetEdad()}, {p.GetCedula()}");
                }
            }

            //COLA QUEUE PARA LA SIMULACION DEL PRIMER ESTUDIANTE ATENDIDO C/USO DE CADENA INTERPOLADA//
            public static void AtenderEstudiante()
            {
                if (ColaAtencion.Count > 0)
                {
                    Persona atendido = ColaAtencion.Dequeue();
                    Console.WriteLine($"Atendiendo a: {atendido.GetNombre()}, {atendido.GetEdad()}, {atendido.GetCedula()}");
                }
                else
                {
                    Console.WriteLine("No hay estudiantes en cola");
                }
            }

            //COLECCION PILA STACK C/COUNT PARA ELIMINAR ELEMENTO SUPERIOR//
            public static void RegistrarAtendido()
            {
                if (PilaAtencion.Count >0)
                {
                    Persona registrada= PilaAtencion.Pop();
                    Console.WriteLine($"Registrado: {registrada.GetNombre()}, {registrada.GetEdad()}, {registrada.GetCedula()}");
                }
            }       
    }
}