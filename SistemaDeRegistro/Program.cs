
using System.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.ComponentModel;

namespace SistemaDeRegistro
{
    public class Program
    {
        //cOLECCIONES DE LISTADO, QUEUE FIFO Y STACK LIFO
        static List<Persona> personas = new List<Persona>();
        static Queue<Persona> Cola= new Queue<Persona>();
        static Stack<Persona> PilaRegistrado= new Stack<Persona>();

        static void Main(string[] args)
        {
		bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n ------- MENU DEL PACIENTE -----");
                Console.WriteLine("1. AGREGAR PACIENTE");
                Console.WriteLine("2. LISTAR PACIENTES ACTUALES");
                Console.WriteLine("3. BUSCAR PACIENTE POR CEDULA");
                Console.WriteLine("4. FILTRAR PACIENTE POR EDAD");
                Console.WriteLine("5. ATENDER PACIENTE");
                Console.WriteLine("6. REGISTRAR PACIENTE ATENDIDO");
                Console.WriteLine("7. SALIR");
                Console.WriteLine("INGRESE UNA OPCION: ");

                string opcion = Console.ReadLine() ?? "";
                switch(opcion)
                {
                    case "1": AgregarPaciente(); break;
                    case "2": ListarPaciente(); break;
                    case "3":
                    Console.WriteLine("Ingrese la cedula a buscar:");
                    string cedula = Console.ReadLine() ?? "";
                    BuscarPorCedula(cedula);
                    break;
                    case "4":
                    Console.WriteLine("ingrese edad: ");
                    int edad = int.Parse(Console.ReadLine() ?? "0");
                    FiltradoPorEdad(edad); break;
                    case "5": AtenderPaciente(); break;
                    case "6": RegistrarPacienteAtendido(); break;
                    case "7": salir= true; break;
                    default: Console.WriteLine("No válido. Intente de nuevo"); break;
                }

            }

        }

            public static void AgregarPaciente()
            {
                Console.WriteLine("Ingrese si es paciente asegurado (s/n): ");
                string EsAsegurado = (Console.ReadLine() ?? "").ToLower();
                Console.WriteLine("Ingrese el nombre del paciente: ");
                string? nombre= Console.ReadLine();
                Console.WriteLine("Ingrese la edad del paciente: ");
                if (!int.TryParse(Console.ReadLine(), out int edad))
                {
                    Console.WriteLine("Edad no válida");
                    return;
                }

                Console.WriteLine("Ingrese la cedula del paciente: ");
                string cedula = Console.ReadLine();
                Console.WriteLine("Ingrese estado civil del paciente: ");
                string estadocivil= Console.ReadLine();

                Persona nuevo;
                if (EsAsegurado == "s")
                {
                    Console.WriteLine("Ingrese el tipo de afiliación:");
                    string tipoAfiliacion = Console.ReadLine();

            //HERENCIA DE PACIENTE Y PERSONA
                    nuevo = new PacienteAsegurado(nombre, edad, cedula, estadocivil, tipoAfiliacion);
                }
                else
                {
                    nuevo = new Paciente(nombre, edad, cedula, estadocivil);
                }

                personas.Add(nuevo);
                Cola.Enqueue(nuevo);
                PilaRegistrado.Push(nuevo);
                Console.WriteLine("Paciente agregado correctamente");

            }

            public static void ListarPaciente()
            {
                foreach (Persona p in personas)
                {
                    Console.WriteLine($"{p.GetNombre()}, {p.GetEdad()},{p.GetCedula()}");
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

            public static void FiltradoPorEdad(int edadAdmitida)
            {
                var filtrado = personas
                                .Where(p => p.GetEdad() >= edadAdmitida);
                
                foreach (var p in filtrado)
                {
                    Console.WriteLine($"{p.GetNombre()}, {p.GetEdad()}, {p.GetCedula()}");
                }
            }

            public static void AtenderPaciente()
            {
                if (Cola.Count > 0)
                {
                Persona atendida= Cola.Dequeue();
                Console.WriteLine($"Atendiendo al Paciente {atendida.GetNombre()}, {atendida.GetEdad()}, {atendida.GetCedula()}");

                }
                else
                {
                    Console.WriteLine("No hay pacientes en cola");
                }
            }
        public static void RegistrarPacienteAtendido()
        {
            if(PilaRegistrado.Count >0 )
            {
                Persona registrado= PilaRegistrado.Pop();
                Console.WriteLine($"Registrado: {registrado.GetNombre()}, {registrado.GetEdad()}, {registrado.GetCedula()}");
            }
        }
    }
}