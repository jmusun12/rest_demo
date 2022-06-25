using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDemo.DTOs
{
    public class DetalleEmpleadoDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string FechaNacimiento { get; set; }
        public decimal Salario { get; set; }

        public DetalleEmpleadoDto()
        {

        }

        public DetalleEmpleadoDto(string nombres, string apellidos, string telefono, string correo,
            string fechaNacimiento, decimal salario = 0)
        {
            this.Nombres = nombres;
            this.Apellidos = apellidos;
            this.Telefono = telefono;
            this.Correo = correo;
            this.FechaNacimiento = fechaNacimiento;
            this.Salario = salario;
        }
    }
}