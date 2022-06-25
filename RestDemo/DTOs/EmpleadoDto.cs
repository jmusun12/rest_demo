using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestDemo.DTOs
{
    public class EmpleadoDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string FechaNacimiento { get; set; }
        public decimal Salario { get; set; }
    }
}