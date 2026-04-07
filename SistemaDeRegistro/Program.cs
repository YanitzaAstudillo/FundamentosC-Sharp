
using System.Linq;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.ComponentModel;

namespace SistemaDeRegistro
{
    public class Program  //PROGRAM SERÍA EL HOSPITAL EN ANALOGIA
    {
        //cOLECCIONES DE LISTADO, QUEUE FIFO Y STACK LIFO. SE AGREGA LA ESTRUCTURA DIC PARA LA BUSQUEDA RAPIDA DEL DATO CORRECTO CED
        static List<Persona> personas = new List<Persona>();
        static Queue<Persona> Cola= new Queue<Persona>();
        static Stack<Persona> PilaRegistrado= new Stack<Persona>();
        static Dictionary<string, Persona> personasPorCedula = new Dictionary<string, Persona>(); //STATIC SOLA INSTANCIA, PERSONASXCEDULA ES EL ARCH CENTRAL DE PAC

        static void Main(string[] args)
        {
            //LEE EL ARCH PERSONAS.TXT, CONVIERTE CADA LINEA EN OBJTOS PERSOAN, LLENA NUEVAMENTE LA LISTA. LLAMADA EN EL MAIN. 
            {
                LeerArchivo();
            }

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

            public static void AgregarPaciente()  //GUARDA EN EL DIC
            {
                Console.WriteLine("Ingrese si es paciente asegurado (s/n): ");
                string EsAsegurado = (Console.ReadLine() ?? "").ToLower();
                Console.WriteLine("Ingrese el nombre del paciente: ");
                string? nombre= Console.ReadLine();
                Console.WriteLine("Ingrese edad: ");

                if (!int.TryParse(Console.ReadLine(), out int edad))
                {
                    Console.WriteLine("Edad no válida");
                    return;
                }
    

                Console.WriteLine("Ingrese la cedula del paciente: ");
                string cedula = Console.ReadLine() ?? "";
            //SE AGREGA VALIDACION PARA EVITAR DUPLICADOS
                if (personasPorCedula.ContainsKey(cedula))
                {
                    Console.WriteLine("Ya existe un paciente con esa cédula, intente de nuevo");
                    return;
                }

                Console.WriteLine("Ingrese estado civil del paciente: ");
                string estadocivil= Console.ReadLine() ?? "";

                Persona nuevo;
                if (EsAsegurado == "s")
                {
                    Console.WriteLine("Ingrese el tipo de afiliación:");
                    string tipoAfiliacion = Console.ReadLine() ?? "";

            //HERENCIA DE PACIENTE Y PERSONA
                    nuevo = new PacienteAsegurado(nombre, edad, cedula, estadocivil, tipoAfiliacion);
                }
                else
                {
                    nuevo = new Paciente(nombre, edad, cedula, estadocivil);
                }
                //GUARDA EL PACIENTE EN LISTA, COLA DE ATENCION, HISTORIAL Y BUSQUEDA
                personas.Add(nuevo);
                Cola.Enqueue(nuevo);
                PilaRegistrado.Push(nuevo);
                Console.WriteLine("Paciente agregado correctamente");

                //PARA EL DICCIONARIO. EVITA DUPLICADOS Y CONSIGUE EL DATO CÉDULA A LA HORA DE EJECUTAR LA BUSQUEDA
                personasPorCedula[nuevo.GetCedula()] = nuevo;

                GuardarArchivo();

            }
            //PARA EL METODO LISTA PAC SE USA UN FOREACH QUE PASA POR CADA ELEMENTO DE LA CADENA
            //EN LUGAR DE CONCATENAR STRINGS MANUALMENTE, SE HACE USO DE CADENA INTERPOLADA {$}
            public static void ListarPaciente()
            {
                foreach (Persona p in personas)
                {
                    Console.WriteLine($"{p.GetNombre()}, {p.GetEdad()},{p.GetCedula()}");
                }
            }

        //SE AGREGÓ EN EL FOREACH AL TRYGETVALUE PARA MAYOR EFECTIVIDAD IMPRIMIENDO TODOS LOS DATOS DEL PAC
        //EL TRYGETVALUE TRABAJA CON CLAVE Y VALOR (CEDULA Y OBJETO PERSONA) PARA QUE FUNCIONE, SE CREÓ PREVIAMENTE EL DICCIONARIO
            public static void BuscarPorCedula(string cedula)  //USA EL MISMO DIC
            {
        
                if (personasPorCedula.TryGetValue(cedula, out Persona p))
                {
                    Console.WriteLine($"{p.GetNombre()}, {p.GetEdad()}, {p.GetCedula()}");
                }
                else
                {
                    Console.WriteLine("No encontrado");
                }
            }
            //LINQ PARA EL FILTRADO CON EXPRESIÓN DEL WHERE USANDO LAMBDA PARA CAPTURAR LA EDAD DEL LISTADO DE PERSONAS
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

        //METODO PARA GUARDAR EL ARCHIVO EN FORMATO TXT, PREVIAMENTE SE HACE LA LLAMADA DESDE EL MAIN
        static void LeerArchivo()
        {
            string path = "personas.txt.txt";

            if (!File.Exists(path))
            {
                File.Create(path).Close();
                return;
            }

            string[] lineas = File.ReadAllLines(path);

            foreach (string linea in lineas)
            {
                string[] partes = linea.Split(',');  //SE TOMA UNA LINEA DEL ARCHIVO Y SE DIVIDE EN UN ARREGLO DE STRINGS USANDO COMA COMO SEPARADOR

                if (partes.Length >= 4)
                {
                    string nombre = partes[0];  //FORMA DE ACCEDER A LOS ELEMENTOS DE UN ARREGLO [NUMERO]
                    int edad = int.Parse(partes[1]);
                    string cedula = partes[2];
                    string estadoCivil = partes[3];

                    Persona p;

                if (partes.Length == 5) //PACIENTE ASEGURADO
                {
                    string tipoAfiliacion = partes[4];
                    p = new PacienteAsegurado(nombre, edad, cedula, estadoCivil, tipoAfiliacion);
                }
                else //PACIENTE NORMAL
                {
                    p = new Paciente(nombre, edad, cedula, estadoCivil);
                }
        
            //SE GUARDA EL OBJ PERSONA EN VARIAS COLECCIONES. sE RECORREN LOS DATOS, AGREGANDO PEROSNAS A LIST GNRAL (VER TODOS PARA FILTRAR),
            //LUEGO AGREGA LA PERSONA A LA COLA QUEUE, A LA PILA STACK Y AL DICCIONARIO USANDO LA CED COMO CLAVE
            personas.Add(p);
            Cola.Enqueue(p);
            PilaRegistrado.Push(p);
            personasPorCedula[cedula] = p;
        }
    }
}

        //METODO PARA GUARDAR TODOS LOS PAC EN EL ARCHIVO DONDE SE UBICA EL ARCHIVO TXT PREVIAMENTE CREADO DENTRO DEL PROYECTO
        //
        static void GuardarArchivo()
        {
            string path = "personas.txt.txt";
            List<string> lineas = new List<string>();

            foreach (Persona p in personas)
            {
                if (p is PacienteAsegurado pa)
                {
                lineas.Add($"{pa.GetNombre()},{pa.GetEdad()},{pa.GetCedula()},{pa.GetEstadoCivil()}");
                }
                else
                {
                lineas.Add($"{p.GetNombre()},{p.GetEdad()},{p.GetCedula()},{p.GetEstadoCivil()}");
                }
            }

            File.WriteAllLines(path, lineas);  //PARA REESCRIBIR TODO EL ARCHIVO CON LO Q SE TIENE EN MEMORIA
        }

        

    }
}