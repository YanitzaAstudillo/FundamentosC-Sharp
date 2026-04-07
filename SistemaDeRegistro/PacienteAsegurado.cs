
using System;

namespace SistemaDeRegistro
{
    public class PacienteAsegurado: Paciente
    {
        private string TipoAfiliacion;

        public PacienteAsegurado(string nombre, int edad, string cedula, string estadocivil, string tipoAfiliacion)
            : base(nombre, edad, cedula, estadocivil)
        {
            this.TipoAfiliacion = tipoAfiliacion;
        }

        public override void MostrarInformacion()
        {
            Console.WriteLine($"Paciente Asegurado: {GetNombre()}, Edad: {Edad},  Cedula: {GetCedula()}, EstadoCivil: {GetEstadoCivil()}");
        }

    }
}